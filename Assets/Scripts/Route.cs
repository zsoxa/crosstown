using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Route : MonoBehaviour {

	public Vector2 startPoint{ get; set;}
	public Vector2 endPoint{ get; set;}
	public int Source{ get; set;}
	public int Destination{ get; set;}
	public Crossroad endCross{ get; set;}
	List<Vector2> routePoints = new List<Vector2>();
	List<BezierCurve> curveList = new List<BezierCurve>();
	public float startWeight{ get; set;}
	public float endWeight{ get; set;}
	public float routeWeight{ get; set;}
	// Use this for initialization
	void Start () {
	
	}
	
	public void addRoutePoint(Vector2 point){
		routePoints.Add(point);
	}
	
	public void addCurve(BezierCurve curve){
		curveList.Add(curve);
	}
	
	
	public List<BezierCurve> getCurveList(){return curveList;}
	
	public List<Vector2> getPoints(){return routePoints;}
	
	// Update is called once per frame
	void Update () {
	
	}
}
