using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crossroad : MonoBehaviour {
	public Vector2 Place{ get; set;}
	public FieldDescriber field;
	public List<Route> bRoutes = new List<Route>();
	public List<Route> rRoutes = new List<Route>();
	public List<Route> uRoutes = new List<Route>();
	public List<Route> lRoutes = new List<Route>();
	public int state;
	float timeLeft;
	// Use this for initialization
	void Start () {
		timeLeft = Random.Range(0.0f,4.0f);
		state = Random.Range(0,4);
	}
	
	public void setField(FieldDescriber f){field=f;}
	//public FieldDescriber getField(){return field;}
	
	public void addRoute(int source, Route r){
		switch(source){
			case 0:
				bRoutes.Add(r);
			break;
			case 1:
				rRoutes.Add(r);
			break;
			case 2:
				uRoutes.Add(r);
			break;
			case 3:
				lRoutes.Add(r);
			break;
			default:
			break;
		}
	}
	
	public int getState(){
		return state;
	}
	
	public List<Route> getRoute(int source){
		switch(source){
			case 0:
				return bRoutes;
			break;
			case 1:
				return rRoutes;
			break;
			case 2:
				return uRoutes;
			break;
			case 3:
				return lRoutes;
			break;
			default:
				return null;
			break;
		}
	}
	
	public void updateField(){
		if(field!=null){
			switch(state){
				case 1: field.GetComponent<Renderer>().material.color = Color.red; break;
				case 2: field.GetComponent<Renderer>().material.color = Color.yellow; break;
				case 3: field.GetComponent<Renderer>().material.color = Color.magenta; break;
				default: field.GetComponent<Renderer>().material.color = Color.green; break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft += Time.deltaTime*3;
		if(timeLeft>5){state++;timeLeft=0;}
		if(state>3){state = state - 4;}
		updateField();
	}
}
