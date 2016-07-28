using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour {

	public Transform target; 
 	public Material material; 
 	public float ropeWidth = 0.5f; 
 	public float resolution = 0.5f; 
 	public float ropeDrag = 0.1f; 
 	public float ropeMass = 0.5f; 
 	public int radialSegments = 6; 
 	public bool startRestrained = true; 
 	public bool endRestrained = false; 
 	public bool useMeshCollision = false; 
 
 	// Private Variables (Only change if you know what your doing) 
 	private Vector3[] segmentPos; 
 	private GameObject[] joints; 
 	private GameObject tubeRenderer; 
 	private TubeRenderer line; 
 	private int segments = 4; 
 	private bool rope = false; 
 
	//Joint Settings 
 	public Vector3 swingAxis = Vector3.up; 
 	public float lowTwistLimit = 0.0f; 
 	public float highTwistLimit = 0.0f; 
 	public float swing1Limit  = 20.0f; 
 
 	void OnDrawGizmos() 
 	{ 
		if(target) 
 		{ 
				58 			Gizmos.color = Color.yellow; 
				59 			Gizmos.DrawLine (transform.position, target.position); 
				60 			Gizmos.DrawWireSphere ((transform.position+target.position)/2,ropeWidth); 
				61 			Gizmos.color = Color.green; 
				62 			Gizmos.DrawWireSphere (transform.position, ropeWidth); 
				63 			Gizmos.color = Color.red; 
				64 			Gizmos.DrawWireSphere (target.position, ropeWidth); 
				65 		} 
			66 		else  
				67 		{ 
				68 			Gizmos.color = Color.green; 
				69 			Gizmos.DrawWireSphere (transform.position, ropeWidth);    
				70 		} 
			71 	} 
		72 

		73 	void Awake() 
		74 	{ 
			75 		if(target) 
				76 		{ 
				77 			BuildRope(); 
				78 		} 
			79 		else  
				80 		{ 
				81 			Debug.LogError("You must have a gameobject attached to target: " + this.name,this);    
				82 		} 
			83 	} 
		84 

		85 	void LateUpdate() 
		86 	{ 
			87 		if(target) 
				88 		{ 
				89 			// Does rope exist? If so, update its position 
				90 			if(rope) 
					91 			{ 
					92 				line.SetPoints(segmentPos, ropeWidth, Color.white); 
					93 

					94 				line.enabled = true; 
					95 				segmentPos[0] = transform.position; 
					96 

					97 				for(int s=1;s<segments;s++) 
						98 				{ 
						99 					segmentPos[s] = joints[s].transform.position; 
						100 				} 
					101 			} 
				102 		} 
			103 	} 
		104 

		105 

		106 

		107 	void BuildRope() 
		108 	{ 
			109 		tubeRenderer = new GameObject("TubeRenderer_" + gameObject.name); 
			110 		line = tubeRenderer.AddComponent(typeof(TubeRenderer)) as TubeRenderer; 
			111 		line.useMeshCollision = useMeshCollision; 
			112 

			113 		// Find the amount of segments based on the distance and resolution 
			114 		// Example: [resolution of 1.0 = 1 joint per unit of distance] 
			115 		segments = Mathf.RoundToInt(Vector3.Distance(transform.position,target.position)*resolution); 
			116 		if(material)  
				117 		{ 
				118 			material.SetTextureScale("_MainTex", new Vector2(1,segments+2)); 
				119 			if(material.GetTexture("_BumpMap")) 
					120 				material.SetTextureScale("_BumpMap", new Vector2(1,segments+2)); 
				121 		} 
			122 		line.vertices = new TubeVertex[segments]; 
			123 		line.crossSegments = radialSegments; 
			124 		line.material = material; 
			125 		segmentPos = new Vector3[segments]; 
			126 		joints = new GameObject[segments]; 
			127 		segmentPos[0] = transform.position; 
			128 		segmentPos[segments-1] = target.position; 
			129 

			130 		// Find the distance between each segment 
			131 		int segs = segments-1; 
			132 		Vector3 seperation = ((target.position - transform.position)/segs); 
			133 

			134 		for(int s=0;s < segments;s++) 
				135 		{ 
				136 			// Find the each segments position using the slope from above 
				137 			Vector3 vector = (seperation*s) + transform.position;    
				138 			segmentPos[s] = vector; 
				139 

				140 			//Add Physics to the segments 
				141 			AddJointPhysics(s); 
				142 		} 
			143 

			144 		// Attach the joints to the target object and parent it to this object 
			145 		CharacterJoint end = target.gameObject.AddComponent(typeof(CharacterJoint)) as CharacterJoint; 
			146 		end.connectedBody = joints[joints.Length-1].transform.rigidbody; 
			147 		end.swingAxis = swingAxis; 
			148 		 
			149 		SoftJointLimit sjl; 
			150 		 
			151 		sjl = end.lowTwistLimit; 
			152 		sjl.limit = lowTwistLimit; 
			153 		end.lowTwistLimit = sjl; 
			154 		 
			155 		sjl = end.highTwistLimit; 
			156 		sjl.limit = highTwistLimit; 
			157 		end.highTwistLimit = sjl; 
			158 		 
			159 		sjl = end.swing1Limit; 
			160 		sjl.limit = swing1Limit; 
			161 		end.swing1Limit = sjl; 
			162 		 
			163 		target.parent = transform; 
			164 

			165 		if(endRestrained) 
				166 		{ 
				167 			end.rigidbody.isKinematic = true; 
				168 		} 
			169 

			170 		if(startRestrained) 
				171 		{ 
				172 			transform.rigidbody.isKinematic = true; 
				173 		} 
			174 

			175 		// Rope = true, The rope now exists in the scene! 
			176 		rope = true; 
			177 	} 
		178 

		179 	void AddJointPhysics(int n) 
		180 	{ 
			181 		joints[n] = new GameObject("Joint_" + n); 
			182 		joints[n].transform.parent = transform; 
			183 		Rigidbody rigid = joints[n].AddComponent(typeof(Rigidbody)) as Rigidbody; 
			184 		if(!useMeshCollision) 
				185 		{ 
				186 			SphereCollider col = joints[n].AddComponent(typeof(SphereCollider)) as SphereCollider; 
				187 			col.radius = ropeWidth; 
				188 		} 
			189 		CharacterJoint ph = joints[n].AddComponent(typeof(CharacterJoint)) as CharacterJoint; 
			190 		ph.swingAxis = swingAxis; 
			191 		 
			192 		SoftJointLimit sjl; 
			193 		 
			194 		sjl = ph.lowTwistLimit; 
			195 		sjl.limit = lowTwistLimit; 
			196 		ph.lowTwistLimit = sjl; 
			197 		 
			198 		sjl = ph.highTwistLimit; 
			199 		sjl.limit = highTwistLimit; 
			200 		ph.highTwistLimit = sjl; 
			201 		 
			202 		sjl = ph.swing1Limit; 
			203 		sjl.limit = swing1Limit; 
			204 		ph.swing1Limit = sjl; 
			205 		//ph.breakForce = ropeBreakForce; <--------------- TODO 
			206 

			207 		joints[n].transform.position = segmentPos[n]; 
			208 

			209 		rigid.drag = ropeDrag; 
			210 		rigid.mass = ropeMass; 
			211 

			212 		if(n==0) 
				213 		{      
				214 			ph.connectedBody = transform.rigidbody; 
				215 		}  
			216 		else 
				217 		{ 
				218 			ph.connectedBody = joints[n-1].rigidbody;    
				219 		} 
			220 	} 
		221 } 
	