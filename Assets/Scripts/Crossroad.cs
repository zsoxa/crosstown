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
	public Texture2D tx0;
	public Texture2D tx1;
	public Texture2D tx2;
	public Texture2D tx3;
	public int state;
	float timeLeft;
	// Use this for initialization
	void Start () {
		this.GetComponent<Renderer>().material.color = Color.white;		
		this.GetComponent<Renderer>().material.SetFloat("_Mode", 3);
		this.GetComponent<Renderer>().material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
		this.GetComponent<Renderer>().material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		this.GetComponent<Renderer>().material.SetInt("_ZWrite", 0);
		this.GetComponent<Renderer>().material.DisableKeyword("_ALPHATEST_ON");
		this.GetComponent<Renderer>().material.DisableKeyword("_ALPHABLEND_ON");
		this.GetComponent<Renderer>().material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
		this.GetComponent<Renderer>().material.renderQueue = 3000;
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
				case 1: this.GetComponent<Renderer>().material.mainTexture = tx1;break;
				case 2: this.GetComponent<Renderer>().material.mainTexture = tx2; break;
				case 3: this.GetComponent<Renderer>().material.mainTexture = tx3; break;
				default: this.GetComponent<Renderer>().material.mainTexture = tx0; break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft += Time.deltaTime*2;
		if(timeLeft>5){state++;timeLeft=0;}
		if(state>3){state = state - 4;}
		updateField();
	}
}
