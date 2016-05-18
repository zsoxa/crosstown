using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarHandler : MonoBehaviour {
	public BezierCurve bezier;
	public static float moveplace = 0.0f;
	float timeLeft = 0.0f;
	Route route;
	int spaceSize;
	int iterator = 0;
	int lastiter = -1;
	int routeCounter = 0;
	Vector3 lastPoint = new Vector3(0,0,0);
	public Crossroad startPlace;
	public Crossroad endPlace;
	public int orientation;
	public float minWeight = 100000.0f;
	//Dictionary<int,Crossroad> tripList = new Dictionary<int, Crossroad>();
	List<Crossroad> tripList = new List<Crossroad>();
	public List<Route> trip;
	public List<BezierCurve> curveTrip = new List<BezierCurve>();
	List<Route> endCrossTrip = new List<Route>();
	// Use this for initialization
	void Start () {
		Vector3 startPoint = bezier.GetComponent<BezierCurve>().GetPointAt(0.0f);
		this.transform.position = startPoint;
		//GetPointAt(float t)
	}
	
	int GetArrayByIndex(Vector2 index){
		return ((int)index.x*spaceSize+(int)index.y);
	}
	
	public void setStart(BezierCurve bez){
		Vector3 startPoint = bez.GetComponent<BezierCurve>().GetPointAt(0.0f);
		this.transform.position = startPoint;
	}
	
	public void Iterate(){
		iterator = 0;
		List<Route> available = startPlace.getRoute(orientation);
		List<Vector2> beenThere = new List<Vector2>();
		List<Route> waylist = new List<Route>();
		float weight = 0.0f;
		beenThere.Add(startPlace.Place);
		foreach(Route route in available){
			List<Route> result = recursiveSearch(route, beenThere, weight, waylist);
			if(result != null){
				trip = result;
			}
		}
		foreach(Route t in trip){
			foreach(BezierCurve bz in t.getCurveList()){
				bz.name = "Bezier" + curveTrip.Count;
				curveTrip.Add(bz);
				endCrossTrip.Add(t);
			}
		}
	}
	
	public List<Route> recursiveSearch(Route route, List<Vector2> beenThere, float weight, List<Route> waylist){
			weight += route.routeWeight;
			List<Route> mywaylist = new List<Route>(waylist);
			mywaylist.Add(route);
			if(weight>minWeight){ return null; }
			List<Route> newRoutes = route.endCross.getRoute(route.Destination);
			if(route.endCross == endPlace){
				if(weight < minWeight){ minWeight = weight;  return mywaylist;}
				else{return null;}//trip=mywaylist;
			}
			foreach(Vector2 was in beenThere){
				if(route.endPoint == was){return null;}
			}
			beenThere.Add(route.endPoint);
			foreach(Route newR in newRoutes){
				List<Route> result = recursiveSearch(newR, beenThere, weight, mywaylist);
				if(result != null){return result;}
			}
			return null;
			//if(resultWeight<minWeight){minWeight=resultWeight;}
	}
	
	public bool setTrip(Crossroad Cross, int Place, int spaceSize){
		List<Route> rt = Cross.getRoute(Place);
		orientation = Place;
		startPlace = Cross;
		//tripList.Add(startPlace);
		recurse(rt);
		int randomKey = Random.Range(0,tripList.Count-1);
		foreach(Crossroad c in tripList){Debug.Log(c);}
		if(tripList.Count==0){return false;}
		endPlace = tripList[randomKey];
		return true;
	}
	
	public bool checkPlace(Crossroad cross){
		foreach(Crossroad crs in tripList){
			if(cross == crs || cross == startPlace){
				return false;
			}
		}
		return true;
	}
	
	public void recurse(List<Route> rt){
		foreach(Route route in rt){
			Vector2 place = route.endPoint;
			bool wasntHere = checkPlace(route.endCross);
			if(wasntHere){
				tripList.Add(route.endCross);
				int newPlace = route.Destination;
				List<Route> rts = route.endCross.getRoute(newPlace);
				if(rts != null){
					recurse(rts);
				}
			}
		}
	}
	
	
	
	// Update is called once per frame
	void Update () {
		BezierCurve bz = bezier;

		if(curveTrip != null && curveTrip.Count>0){
			bz = curveTrip[iterator];
		}
		timeLeft += Time.deltaTime * 10; /// 20;
		Vector3 nextPoint = bz.GetComponent<BezierCurve>().GetPointAtDistance(timeLeft);
		if(lastPoint != nextPoint){
			Vector3 nowPoint = this.transform.position;
			this.transform.LookAt(nextPoint,Vector3.up);
			this.transform.position=nextPoint;
			
		}
		else{
			if(iterator+1 < curveTrip.Count){
				if(endCrossTrip[iterator].endCross!=endCrossTrip[iterator+1].endCross){
					//Debug.Log(endCrossTrip[iterator].endCross.getState());
					List<Route> freeRoutes = endCrossTrip[iterator].endCross.getRoute(endCrossTrip[iterator].endCross.getState());
					foreach(Route route in freeRoutes){
						if(endCrossTrip[iterator+1]==route){
							Debug.Log("GREENWAY");
							lastiter = iterator;
							iterator++;
							timeLeft = 0.0f;
							break;
						}
					}
				}
				else{
					lastiter = iterator;
					iterator++;
					timeLeft = 0.0f;
				}
			}
		}
		lastPoint = nextPoint;
	}
}
