using UnityEngine;
using System.Collections;

public class CurveCreatorScript : MonoBehaviour {
	//private BezierCurve curve : BezierCurve;
	// Use this for initialization
	void Start () {
		//curve = GetComponent(BezierCurve);
		float posx = 0.0f;
		float posy = 10.0f;
		Vector3 position = new Vector3((float)posx,0.0f,(float)posy);
		BezierPoint point0 = this.GetComponent<BezierCurve>()[0];
		point0.transform.position = position;
		//point0.position = position;
		//uaternion spawnRotation = Quaternion.AngleAxis(0, Vector3.right);
		//GameObject newObject = (GameObject) Instantiate (point, spawnPosition,spawnRotation);
		//BezierPoint exactPoint = newObject.GetComponent<BezierPoint>();
		//this.GetComponent<BezierCurve>().AddPoint(newObject.GetComponent<BezierPoint>());
		//point1.
		//curve.AddPoint(point1);
	}

	
}