using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Plot : MonoBehaviour {

	public GameObject canvas;
	public GameObject point;

	//private float[] xCoords = {0,1,2,3,4,8};
	//private float[] yCoords = {0,1,2,3,4,5};
	//private float[] zCoords = {0,1,2,2,4,2};

	private int canvasSize = 10;

	void Start () {

		Vector3[] myData = read_csv ("C:\\Users\\Tom\\Documents\\text.csv");

		// Coordinate Normalization - Doesn't Work Yet for Negative Numbers
		//for (int i = 0; i < xCoords.Length; i++) {
		//	xCoords [i] = (canvasSize * xCoords [i] / Mathf.Max (xCoords)) - canvasSize/2;
		//	yCoords [i] = canvasSize * yCoords [i] / (Mathf.Max (yCoords) + 0.1F); // +0.1 to avoid problems if max is zero (need better solution)
		//	zCoords [i] = (canvasSize * zCoords [i] / Mathf.Max (zCoords)) - canvasSize/2;
		//}

		GeomPoint (myData);
		GeomLine (myData); 
	}

	// Make a Coordinate Array of type Vector3
	Vector3[] MakeCoordArray (float[] x, float[] y, float[] z){
		
		Vector3[] coords = new Vector3[x.Length];
		for (int i = 0; i < x.Length; i++) {
			coords [i] = new Vector3 (x [i], y [i], z [i]);
		}

		return coords;
	}

	// Read CSV Function
	Vector3[] read_csv(string filename){
		
		string str = System.IO.File.ReadAllText (filename);
		string[] values = str.Split('\n');


		// Including an empty 0,0 point that needs to be removed
		float[] xCoords = new float[values.Length-1]; //-1 to remove
		float[] yCoords = new float[values.Length-1]; 
		float[] zCoords = new float[values.Length-1]; 

		for(int i = 1; i<values.Length-1; i++){
			xCoords[i] = float.Parse(values [i].Split (',')[0]);
			yCoords[i] = float.Parse(values [i].Split (',')[2]);
			zCoords[i] = float.Parse(values [i].Split (',')[1]);
		}

		return MakeCoordArray (xCoords, yCoords, zCoords);

	}
		
	void GeomPoint (Vector3[] coordArray) {
		// Plot Points using Spheres
		for (int i = 0; i < coordArray.Length; i++) {
			var myPoint = Instantiate(point, canvas.transform.position + coordArray[i], Quaternion.identity);
			//myPoint.transform.localScale = new Vector3(0.1F,0.1F,0.1F);
			myPoint.transform.parent = canvas.transform;
		}
	}

	void GeomLine (Vector3[] coordArray) {
		// Plot Line unsing LineRenderer

		LineRenderer myLine = canvas.GetComponent<LineRenderer>();
		//LineRenderer myLine = canvas.AddComponent<LineRenderer>();
		myLine.useWorldSpace = false;
		myLine.positionCount = coordArray.Length;
		myLine.SetPositions (coordArray);
		myLine.widthMultiplier = 0.3F;
		myLine.enabled = true;
	}
	


}


	
	