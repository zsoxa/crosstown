using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraHandler : MonoBehaviour {
	public CarHandler followedObject;
	public int cameraHeight = 15;
	List<CarHandler> carList;
	int index = 0;
	// Use this for initialization
	void Start () {
		Vector3 place = followedObject.transform.position;
		this.transform.position = new Vector3 (place.x,cameraHeight,place.z);
	}
	
	public void addCarList(List<CarHandler> list){
		carList = list;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow)){cameraHeight++;}
		else if (Input.GetKeyDown(KeyCode.DownArrow)){cameraHeight--;}
		if(carList!=null){
			if (Input.GetKeyDown(KeyCode.RightArrow)){index++;}
			else if (Input.GetKeyDown(KeyCode.LeftArrow)){index--;}
			if(index >= carList.Count){index--;}
			else if(index < 0){index++;}
			followedObject = carList[index];
		}
		
		if(cameraHeight<3){cameraHeight++;}
		Vector3 place = followedObject.transform.position;
		this.transform.position = new Vector3 (place.x,cameraHeight,place.z);
	}
}
