//This class is used to define the path of the platforms. Got this from Udemy

using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathDefinition : MonoBehaviour 
{
	public Transform[] Points;

	public IEnumerator<Transform> GetPathEnumerator()//Tells the game where to start of the platforms and where it ends
    {
		if (Points == null || Points.Length < 1)
			yield break;
		var direction = 1;
		var index = 0;
		while (true) {
			yield return Points [index];
			if (Points.Length == 1)
				continue;
			if (index <=0)
				direction = 1;
			else if (index >= Points.Length - 1)
				direction = -1;
			index = index + direction;
		}
	}

	public void OnDrawGizmos() //This is used to draw a line for the platforms to make it easier to see in the Editor
    {

		if (Points == null || Points.Length < 2)
			return;

		var points = Points.Where (t => t != null).ToList ();
		if (points.Count < 2)
			return;

		for (var i = 1; i < points.Count; i++) {
			Gizmos.DrawLine(points[i-1].position, points[i].position);
		}
	}
}
