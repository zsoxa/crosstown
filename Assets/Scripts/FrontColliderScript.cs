using UnityEngine;
using System.Collections;

public class FrontColliderScript : MonoBehaviour {
	int state;
	// Use this for initialization
	void Start () {
		state = 0;
	}
	
	void OnTriggerEnter (Collider other){
		state = 1;
	}
	
	void OnTriggerExit (Collider other){
		state = 0;
	}
	
	public int getState(){return state;}
	
	// Update is called once per frame
	void Update () {
	
	}
}
