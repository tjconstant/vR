using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour {

	public GameObject canvas;
	public GameObject point;

	private float[] xCoords = {0,1,2,3,4};
	private float[] yCoords = {0,0,0,0,0};
	private float[] zCoords = {0,1,2,3,4};

	void Start () {

		// Plot Points using Spheres
		for (int i = 0; i < xCoords.Length; i++) {
			var myPoint = Instantiate(point, canvas.transform.position + new Vector3(xCoords[i], yCoords[i], zCoords[i]), Quaternion.identity);
			myPoint.transform.parent = canvas.transform;
		}

		// Plot Line unsing LineRenderer
		LineRenderer myLine = canvas.GetComponent<LineRenderer>();
		var coordArray = MakeCoordArray (xCoords, yCoords, zCoords);
		myLine.positionCount = coordArray.Length;
		myLine.SetPositions (coordArray);
		myLine.enabled = true;

	}

	// Make a Coordinate Arrayc of type Vaector3
	Vector3[] MakeCoordArray (float[] x, float[] y, float[] z){
		
		Vector3[] coords = new Vector3[5];
		for (int i = 0; i < x.Length; i++) {
			coords [i] = new Vector3 (x [i], y [i], z [i]);
		}

		return coords;
	}

}


	
	