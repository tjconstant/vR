using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour {

	public GameObject canvas;
	public GameObject point;

	private float[] xCoords = {0,1,2,3,4,8};
	private float[] yCoords = {0,1,2,3,4,5};
	private float[] zCoords = {0,1,2,2,4,2};

	private int canvasSize = 10;

	void Start () {

		// Coordinate Normalization - Doesn't Work Yet for Negative Numbers
		for (int i = 0; i < xCoords.Length; i++) {
			xCoords [i] = (canvasSize * xCoords [i] / Mathf.Max (xCoords)) - canvasSize/2;
			yCoords [i] = canvasSize * yCoords [i] / (Mathf.Max (yCoords) + 0.1F); // +0.1 to avoid problems if max is zero (need better solution)
			zCoords [i] = (canvasSize * zCoords [i] / Mathf.Max (zCoords)) - canvasSize/2;
		}

		GeomPoint ();
		GeomLine (); 
	}

	// Make a Coordinate Array of type Vector3
	Vector3[] MakeCoordArray (float[] x, float[] y, float[] z){
		
		Vector3[] coords = new Vector3[xCoords.Length];
		for (int i = 0; i < x.Length; i++) {
			coords [i] = new Vector3 (x [i], y [i], z [i]);
		}

		return coords;
	}

	//public class Geom {

	void GeomPoint () {
		// Plot Points using Spheres
		for (int i = 0; i < xCoords.Length; i++) {
			var myPoint = Instantiate(point, canvas.transform.position + new Vector3(xCoords[i], yCoords[i], zCoords[i]), Quaternion.identity);
			myPoint.transform.parent = canvas.transform;
		}
	}

	void GeomLine () {
		// Plot Line unsing LineRenderer

		LineRenderer myLine = canvas.GetComponent<LineRenderer>();
		//LineRenderer myLine = canvas.AddComponent<LineRenderer>();
		myLine.useWorldSpace = false;
		var coordArray = MakeCoordArray (xCoords, yCoords, zCoords);
		myLine.positionCount = coordArray.Length;
		myLine.SetPositions (coordArray);
		myLine.widthMultiplier = 0.3F;
		myLine.enabled = true;
	}
	
	//}

}


	
	