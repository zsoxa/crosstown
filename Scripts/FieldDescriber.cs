using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldDescriber : MonoBehaviour {
	
	public float fieldSize;
	public Vector3 position;
	public int type;
	public bool finalType;
	public Vector2 index;
	public int entryPoint;
	public int[] gen;
	public Dictionary<Vector2,BezierCurve> curves = new Dictionary<Vector2, BezierCurve>();

	// Use this for initialization
	void Start () {

	}
	
	public float getScale(){return fieldSize;}
	
	public void setFinal(bool var){finalType=var;}
	public bool isFinal(){return finalType;}
	
	public Dictionary<Vector2,BezierCurve> getCurves(){return curves;}
	
	
	public void setAllGen(int g){for(int i=0; i<gen.Length; i++){gen[i]=g;}}
	
	public void setGeneric(int place, int value){gen[place]=value;}
	public int getGen(int place){return gen[place];}
	
	public void setIndex(Vector2 index){this.index = index;}	
	public Vector2 getIndex(){return this.index;}
	
	public int getType(){return this.type;}
	public void setType(int type){this.type = type;}
	
	public void setEntryPoint(int value){entryPoint=value;}
	
	public int getEntryPoint(){return entryPoint;}
	
	void clearValue(List<int> list, int value){
		foreach(int v in list){if(value==v){list.Remove(v);break;}}
	}
	
	public void fillCurveList(int source, Vector2 toIndex,BezierCurve curve){
		if(toIndex.x < 0){toIndex.x = 0;}
		if(toIndex.y < 0){toIndex.y = 0;}
		Vector2 dictIndex = new Vector2(1000*source + toIndex.x, 1000*source + toIndex.y);
		try{
		curves.Add(dictIndex,curve);
		}
		catch(System.Exception e){}
	}
	
	public BezierCurve getCurve(int source, Vector2 index){
		Vector2 dictIndex = new Vector2(1000*source + index.x, 1000*source + index.y);
		return curves[dictIndex];
	}
	
	public BezierCurve getCurveByS(Vector2 dictIndex){
		return curves[dictIndex];
	}
	
	public void CalculateFirstType(){
		List<int> randValues = new List<int>();
		randValues.Add(1);randValues.Add(2);randValues.Add(3);randValues.Add(4);randValues.Add(5);randValues.Add(6);randValues.Add(7);randValues.Add(8);randValues.Add(9);randValues.Add(10);randValues.Add(11);randValues.Add(12);
		if(gen[1]==1){clearValue(randValues,3);clearValue(randValues,7);clearValue(randValues,8);clearValue(randValues,11);clearValue(randValues,12);}
		if(gen[1]==2){clearValue(randValues,1);clearValue(randValues,2);clearValue(randValues,4);clearValue(randValues,5);clearValue(randValues,6);clearValue(randValues,9);clearValue(randValues,10);}
		if(gen[2]==1){clearValue(randValues,3);clearValue(randValues,5);clearValue(randValues,9);clearValue(randValues,10);clearValue(randValues,12);}
		if(gen[2]==2){clearValue(randValues,1);clearValue(randValues,2);clearValue(randValues,4);clearValue(randValues,6);clearValue(randValues,7);clearValue(randValues,8);clearValue(randValues,11);}
		if(gen[3]==1){clearValue(randValues,2);clearValue(randValues,4);clearValue(randValues,8);clearValue(randValues,9);clearValue(randValues,12);}
		if(gen[3]==2){clearValue(randValues,1);clearValue(randValues,3);clearValue(randValues,5);clearValue(randValues,6);clearValue(randValues,7);clearValue(randValues,10);clearValue(randValues,11);}
		if(gen[4]==1){clearValue(randValues,2);clearValue(randValues,6);clearValue(randValues,10);clearValue(randValues,11);clearValue(randValues,12);}
		if(gen[4]==2){clearValue(randValues,1);clearValue(randValues,3);clearValue(randValues,4);clearValue(randValues,5);clearValue(randValues,7);clearValue(randValues,8);clearValue(randValues,9);}
		if(randValues.Count>0){
			this.type = randValues[Random.Range(0,randValues.Count)];
			setFinal(true);
		}
	}
	
	public Vector3 getPos(){
		return position;
	}
	
	public void setPos(Vector3 pos){
		position = pos;
	}
}