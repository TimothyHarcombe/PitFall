/*using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BarrelPath : MonoBehaviour {

	public Transform[] Destinations;

	public IEnumerator<Transform> GetPathEnumerator(){
		if (Destinations == null || Destinations.Length < 1)
			yield break;
		var direction = 1;
		var index = 0;
		while (true) {
			yield return Destinations [index];
			if (Destinations.Length == 1)
				continue;
			if (index <=0)
				direction = 1;
			else if (index >= Destinations.Length - 1)
				direction = -1;
			index = index + direction;
		}
	}

	public void OnDrawGizmos ()
	{

		if (Destinations == null || Destinations.Length < 2)
			return;

		var Destinations = Destinations.Where (t => t != null).ToList ();
		if (Destinations.Count < 2)
			return;

		for (var i = 1; i < Destinations.Count; i++) {
			Gizmos.DrawLine (Destinations [i - 1].position, Destinations [i].position);
		}
	}
}*/