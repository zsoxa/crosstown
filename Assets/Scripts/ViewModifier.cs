using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewModifier : MonoBehaviour {
	// Use this for initialization
	public Texture2D type1;
	public Texture2D type2;
	public Texture2D type3;
	public Texture2D type4;
	public BezierCurve bc;
	public BezierCurve bl;
	public BezierCurve blc;
	public Building building;
	public Building building2;
	protected float fieldSize;
	
	public void Initialize (List<FieldDescriber> world, float fieldSize) {
		fieldTextures(world);
		this.fieldSize = fieldSize;
	}
	
	void drawTurn(int i,FieldDescriber w, List<Vector2> neighbors){
		int num = i - 1;
		if(num < 0){num = 3;}
		Quaternion spawnRotation = Quaternion.AngleAxis(0.0f - i * 90.0f, Vector3.up);
		BezierCurve leftCurve = (BezierCurve) Instantiate (bc, w.getPos(),spawnRotation);
		w.fillCurveList(i,neighbors[num],leftCurve);
					
		spawnRotation = Quaternion.AngleAxis(90.0f - i * 90.0f, Vector3.up);
		BezierCurve rightCurve = (BezierCurve) Instantiate (blc, w.getPos(),spawnRotation);
		w.fillCurveList(num,neighbors[i],rightCurve);
	}
	
	void fieldTextures(List<FieldDescriber> world){
		foreach(FieldDescriber w in world){
			List<Vector2> neighbors = new List<Vector2>();
			Vector2 left = new Vector2(w.getIndex().x-1,w.getIndex().y);
			Vector2 bottom = new Vector2(w.getIndex().x,w.getIndex().y-1);
			Vector2 right = new Vector2(w.getIndex().x+1,w.getIndex().y);
			Vector2 top = new Vector2(w.getIndex().x,w.getIndex().y+1);
			neighbors.Add(bottom);neighbors.Add(right);neighbors.Add(top);neighbors.Add(left);
			//w.GetComponent<Renderer>().material.color = Color.black;
			switch(w.getType()){
				case 1:
					//w.GetComponent<Renderer>().material.color = Color.red;
					w.GetComponent<Renderer>().material.mainTexture = type1;
					for(int i=0; i<4;i++){
						/* bal kanyarok */
						Quaternion spawnRotation = Quaternion.AngleAxis(i * -90, Vector3.up);
						BezierCurve leftCurve = (BezierCurve) Instantiate (bc, w.getPos(),spawnRotation);
						int bcIndex = i + 3;
						if(bcIndex>3){bcIndex = bcIndex - 4;}
						w.fillCurveList(i,neighbors[bcIndex],leftCurve);
						/* egyenes utak */
						BezierCurve straight = (BezierCurve) Instantiate (bl, w.getPos(),spawnRotation);
						int blIndex = i + 2;
						if(blIndex>3){blIndex = blIndex - 4;}
						w.fillCurveList(i,neighbors[blIndex],straight);
						/* jobb kanyarok */
						BezierCurve rightCurve = (BezierCurve) Instantiate (blc, w.getPos(),spawnRotation);
						int blcIndex = i + 1;
						if(blcIndex>3){blcIndex = blcIndex - 4;}
						w.fillCurveList(i,neighbors[blcIndex],rightCurve);
					}
					break;
				case 2:
					//w.GetComponent<Renderer>().material.color = Color.blue;
					w.GetComponent<Renderer>().material.mainTexture = type2;
					/* egyenes utak fel-le */
					for(int i=0; i<2;i++){
					Quaternion spawnRotation = Quaternion.AngleAxis(i * 180, Vector3.up);
					BezierCurve straight = (BezierCurve) Instantiate (bl, w.getPos(),spawnRotation);
					if(i==1){i++;}
					int blIndex = i + 2;
					if(blIndex>3){blIndex = blIndex - 4;}
					w.fillCurveList(i,neighbors[blIndex],straight);
					}
					break;
				case 3:
					w.transform.Rotate(0.0f,90.0f,0.0f,0);
					w.GetComponent<Renderer>().material.mainTexture = type2;
					/* egyenes utak jobb-bal */
					for(int i=1; i<3;i++){
					int j = i-1;
					Quaternion spawnRotation = Quaternion.AngleAxis(-90 + j * 180, Vector3.up);
					BezierCurve straight = (BezierCurve) Instantiate (bl, w.getPos(),spawnRotation);
					if(i==2){i++;}
					int blIndex = i + 2;
					if(blIndex>3){blIndex = blIndex - 4;}
					w.fillCurveList(i,neighbors[blIndex],straight);
					}
					break;
				case 4:
					w.GetComponent<Renderer>().material.mainTexture = type4;
					
					for(int i=0; i<2;i++){
					Quaternion spawnRotation = Quaternion.AngleAxis(i * 180, Vector3.up);
					BezierCurve straight = (BezierCurve) Instantiate (bl, w.getPos(),spawnRotation);
					if(i==1){i++;}
					int blIndex = i + 2;
					if(blIndex>3){blIndex = blIndex - 4;}
					w.fillCurveList(i,neighbors[blIndex],straight);
					}
					
					drawTurn(0,w,neighbors);
					drawTurn(3,w,neighbors);
					break;
				case 5:
					w.transform.Rotate(0.0f,90.0f,0.0f,0);
					w.GetComponent<Renderer>().material.mainTexture = type4;
					
					for(int i=1; i<3;i++){
					int j = i-1;
					Quaternion spawnRotation = Quaternion.AngleAxis(-90 + j * 180, Vector3.up);
					BezierCurve straight = (BezierCurve) Instantiate (bl, w.getPos(),spawnRotation);
					if(i==2){i++;}
					int blIndex = i + 2;
					if(blIndex>3){blIndex = blIndex - 4;}
					w.fillCurveList(i,neighbors[blIndex],straight);
					}
					
					drawTurn(2,w,neighbors);
					drawTurn(3,w,neighbors);
					
					break;
				case 6:
					w.transform.Rotate(0.0f,180.0f,0.0f,0);
					w.GetComponent<Renderer>().material.mainTexture = type4;
					
					for(int i=0; i<2;i++){
					Quaternion spawnRotation = Quaternion.AngleAxis(i * 180, Vector3.up);
					BezierCurve straight = (BezierCurve) Instantiate (bl, w.getPos(),spawnRotation);
					if(i==1){i++;}
					int blIndex = i + 2;
					if(blIndex>3){blIndex = blIndex - 4;}
					w.fillCurveList(i,neighbors[blIndex],straight);
					}
					
					drawTurn(1,w,neighbors);
					drawTurn(2,w,neighbors);
					
					break;
				case 7:
					w.transform.Rotate(0.0f,270.0f,0.0f,0);
					w.GetComponent<Renderer>().material.mainTexture = type4;
					
					for(int i=1; i<3;i++){
					int j = i-1;
					Quaternion spawnRotation = Quaternion.AngleAxis(-90 + j * 180, Vector3.up);
					BezierCurve straight = (BezierCurve) Instantiate (bl, w.getPos(),spawnRotation);
					if(i==2){i++;}
					int blIndex = i + 2;
					if(blIndex>3){blIndex = blIndex - 4;}
					w.fillCurveList(i,neighbors[blIndex],straight);
					}
					
					drawTurn(0,w,neighbors);
					drawTurn(1,w,neighbors);
					
					break;
				case 8:
					w.GetComponent<Renderer>().material.mainTexture = type3;
					
					drawTurn(0,w,neighbors);
				break;
				case 9:
					w.transform.Rotate(0.0f,90.0f,0.0f,0);
					w.GetComponent<Renderer>().material.mainTexture = type3;
					
					drawTurn(3,w,neighbors);
				break;
				case 10:
					w.transform.Rotate(0.0f,180.0f,0.0f,0);
					w.GetComponent<Renderer>().material.mainTexture = type3;
					
					drawTurn(2,w,neighbors);
				break;
				case 11:
					w.transform.Rotate(0.0f,270.0f,0.0f,0);
					w.GetComponent<Renderer>().material.mainTexture = type3;
					
					drawTurn(1,w,neighbors);
				break;
				default:
					w.GetComponent<Renderer>().material.color = Color.black;
					int view = Random.Range(0,4);
					int btype = Random.Range(0,2);
					if(btype==0){
					Building build = (Building) Instantiate(building,w.getPos(),Quaternion.AngleAxis(view*90.0f, Vector3.up));
					}
					else{
					Building build = (Building) Instantiate(building2,w.getPos(),Quaternion.AngleAxis(view*90.0f, Vector3.up));
					}
					break;
			}
			//w.renderer.material.mainTexture = ColorWhite;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
