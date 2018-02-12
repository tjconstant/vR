using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Plot : MonoBehaviour {

	public GameObject canvas;
	public GameObject point;
	public Transform axisText;

	//private float[] xCoords = {0,1,2,3,4,8};
	//private float[] yCoords = {0,1,2,3,4,5};
	//private float[] zCoords = {0,1,2,2,4,2};

	private float canvasSize = 10F;


	void Start () {
		
		Vector3[] myData = read_csv ("C:\\Users\\Tom\\Documents\\text.csv");

		GeomPoint (myData);
		GeomLine (myData);
		Axis ();

	}


	// Make a Coordinate Array of type Vector3
	Vector3[] MakeCoordArray (float[] x, float[] y, float[] z){
		
		Vector3[] coords = new Vector3[x.Length];
		for (int i = 0; i < x.Length; i++) {
			coords [i] = new Vector3 (x [i], y [i], z [i]);
		}

		return coords;
	}

	// Normalise Vectors to Canvas Size (unfinished)
	float[] NormalizeArray(float[] coords){
		
		float maxCoord = Mathf.Max (coords);
		//float minCoord = Mathf.Min (coords);
		//float centerCoord = (maxCoord - minCoord) / 2;

		for (int i = 0; i < coords.Length; i++) {
			coords [i] *= (canvasSize / (maxCoord));
			coords [i] *= 0.5F;
		}
		return coords;
	}

	// Read CSV Function
	Vector3[] read_csv(string filename){
		
		string str = System.IO.File.ReadAllText (filename);
		string[] values = str.Split('\n');


		// Including an empty 0,0 point that needs to be removed
		float[] xCoords = new float[values.Length-2]; //-1 to remove
		float[] yCoords = new float[values.Length-2]; 
		float[] zCoords = new float[values.Length-2]; 

		for(int i = 0; i<(values.Length-2); i++){
			xCoords[i] = float.Parse(values [i+1].Split (',')[0]);
			yCoords[i] = float.Parse(values [i+1].Split (',')[2]);
			zCoords[i] = float.Parse(values [i+1].Split (',')[1]);
		}

		xCoords = NormalizeArray (xCoords);
		//yCoords = NormalizeArray (yCoords);
		zCoords = NormalizeArray (zCoords);

		return MakeCoordArray (xCoords, yCoords, zCoords);

	}
		
	void GeomPoint (Vector3[] coordArray) {
		// Plot Points using Spheres

		for (int i = 0; i < coordArray.Length; i++) {
			var myPoint = Instantiate(point, canvas.transform.TransformPoint(coordArray[i]), canvas.transform.rotation);
			myPoint.transform.localScale = new Vector3(0.5F,0.5F,0.5F);
			myPoint.transform.SetParent(canvas.transform);
			//myPoint.transform.parent = canvas.transform;
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

	// Prototype Axis Maker
	void Axis (){

		float[] positions = {-5,-4,-3,-2,-1,0,1,2,3,4,5};

		for(int i = 0; i < positions.Length; i++){
			
			Transform txtMeshTransform = (Transform)Instantiate(axisText);
			TextMesh txtMesh = txtMeshTransform.GetComponent<TextMesh>();
			txtMesh.text = positions[i].ToString();
			txtMeshTransform.SetParent(canvas.transform);
			txtMeshTransform.localPosition = new Vector3 (positions[i], 0.0F, -5.5F);
			txtMeshTransform.Rotate(new Vector3(90,0,0));
			txtMesh.color = Color.black; // Set the text's color to red
		}

		for(int i = 0; i < positions.Length; i++){

			Transform txtMeshTransform = (Transform)Instantiate(axisText);
			TextMesh txtMesh = txtMeshTransform.GetComponent<TextMesh>();
			txtMesh.text = positions[i].ToString();
			txtMeshTransform.SetParent(canvas.transform);
			txtMeshTransform.localPosition = new Vector3 (-5.5F, 0.0F, positions[i]);
			txtMeshTransform.Rotate(new Vector3(90,0,0));
			txtMesh.color = Color.black; // Set the text's color to red
		}
	}

}


	
	