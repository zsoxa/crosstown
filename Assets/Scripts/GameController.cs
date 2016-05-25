using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	public int spaceSize;
	public int entrySize;
	public bool strictEntrySize;
	public float fieldSize;
	public GameObject Field;
	public ViewModifier view;
	public Route Route;
	public Crossroad Crossroad;
	public CameraHandler camera;
	public int carsNumber = 1;
	List<Crossroad> crosses = new List<Crossroad>();
	public CarHandler car1;
	List<CarHandler> carList = new List<CarHandler>();
	List<FieldDescriber> world;
	// Use this for initialization
	void Start () {
		world = new List<FieldDescriber>();
		GenerateWorld(world);
	}
	
	void GenerateWorld(List<FieldDescriber> world) {
		Debug.logger.Log("Hey");
		Vector3 objectSize = new Vector3(fieldSize, fieldSize, 1.0f);
		Field.transform.localScale = objectSize;
		spawnFields(world);
		addEntries(world);
		fixFieldTypes(world);
		view.Initialize(world,spaceSize);
		determineCrossroads(world);
		if(crosses.Count > 1){
		addCars();
		}
	}
	
	int GetArrayByIndex(Vector2 index){
		return ((int)index.x*spaceSize+(int)index.y);
	}
	
	void spawnFields(List<FieldDescriber> world) {
		for(int i=0; i<spaceSize; i++){
			for(int j=0; j<spaceSize; j++){
				world.Add(CreateField(i,j));
			}
		}
	}
	
	List<int> initRandomList(){
		List<int> randomValues = new List<int>();
		for(int i=0;i<spaceSize;i++){
			randomValues.Add(i);
		}
		return randomValues;
	}
	
	void addEntries(List<FieldDescriber> world){
		int avgOnSide = (int) entrySize/4;
		int leftOver = entrySize;
		for(int i=0;i<avgOnSide+1;i++){
			for(int j=0;j<4;j++){
				bool asd = true;
				List<int> randValues = initRandomList();
				while(asd && randValues.Count!=0 && leftOver>0){
					int randNum = Random.Range(0,randValues.Count);
					int entryValue = 0;
					FieldDescriber item = null;
					switch(j){
						case 0:
							item = world[randValues[randNum]];
							if((int)item.getIndex().y==0){entryValue = 3;item.setGeneric(0,2);item.setType(getRandomType(3));item.setFinal(true);} //bottom left
							else if((int)item.getIndex().y==(spaceSize-1)){entryValue = 1;item.setGeneric(0,2);item.setType(getRandomType(1));item.setFinal(true);} //top left
							else{entryValue = 2;item.setGeneric(0,1);}	// left
							item.setGeneric(4,1);
						break;
						case 1:
							item = world[spaceSize*randValues[randNum]];
							if((int)item.getIndex().x==0){entryValue = 3;item.setGeneric(0,2);item.setType(getRandomType(3));item.setFinal(true);} //bottom left
							else if((int)item.getIndex().x==(spaceSize-1)){entryValue = 5;item.setGeneric(0,2);item.setType(getRandomType(5));item.setFinal(true);} //bottom right
							else{entryValue = 4;item.setGeneric(0,1);} // bottom
							item.setGeneric(2,1);
						break;
						case 2:
							item = world[(spaceSize-1)+(spaceSize*randValues[randNum])];
							if((int)item.getIndex().x==(spaceSize-1)){entryValue = 7;item.setGeneric(0,2);item.setType(getRandomType(7));item.setFinal(true);} // top right
							else if((int)item.getIndex().x==0){entryValue = 1;item.setGeneric(0,2);item.setType(getRandomType(1));item.setFinal(true);} // top left
							else{entryValue = 8;item.setGeneric(0,1);} // top
							item.setGeneric(1,1);
						break;
						case 3:
							item = world[(spaceSize-1)*spaceSize+randValues[randNum]];
							if((int)item.getIndex().y==0){entryValue = 5;item.setGeneric(0,2);item.setType(getRandomType(5));item.setFinal(true);} // bottom right
							else if((int)item.getIndex().y==spaceSize-1){entryValue = 7;item.setGeneric(0,2);item.setType(getRandomType(7));item.setFinal(true);} // top right
							else{entryValue = 6;item.setGeneric(0,1);} // right
							item.setGeneric(3,1);
						break;
					}
					randValues.Remove(randValues[randNum]);
					if(item.getEntryPoint()>0){
						asd = true;
					}else{
						asd = false;
						item.setEntryPoint(entryValue);
						//item.GetComponent<Renderer>().material.color = Color.grey;
						leftOver--;
					}
				}
			}
			
		}
	}
	
	public int getRandomType(int input){
		List<int> randValues = new List<int>();
		switch(input){
			case 1:
				randValues.Add(3);randValues.Add(7);randValues.Add(8);
				return randValues[Random.Range(0,randValues.Count)];		
			case 2:
				randValues.Add(1);randValues.Add(3);randValues.Add(4);
				randValues.Add(5);randValues.Add(7);randValues.Add(8);
				randValues.Add(9);
				//FieldDescriber higherItem = world[GetArrayByIndex(new Vector2(index.x,index.y+1))];
				//FieldDescriber lowerItem = world[GetArrayByIndex(new Vector2(index.x,index.y-1))];
				return randValues[Random.Range(0,randValues.Count)];			
			case 3:
				randValues.Add(3);randValues.Add(9);randValues.Add(5);
				return randValues[Random.Range(0,randValues.Count)];			
			case 4:
				randValues.Add(1);randValues.Add(2);randValues.Add(4);
				randValues.Add(6);randValues.Add(7);randValues.Add(8);
				randValues.Add(11);
				return randValues[Random.Range(0,randValues.Count)];			
			case 5:
				randValues.Add(3);randValues.Add(5);randValues.Add(10);
				return randValues[Random.Range(0,randValues.Count)];	
			case 6:
				randValues.Add(1);randValues.Add(3);randValues.Add(5);
				randValues.Add(6);randValues.Add(7);randValues.Add(10);
				randValues.Add(11);
				return randValues[Random.Range(0,randValues.Count)];	
			case 7:
				randValues.Add(3);randValues.Add(7);randValues.Add(11);
				return randValues[Random.Range(0,randValues.Count)];	
			case 8:
				randValues.Add(1);randValues.Add(2);randValues.Add(4);
				randValues.Add(5);randValues.Add(6);randValues.Add(9);
				randValues.Add(10);
				return randValues[Random.Range(0,randValues.Count)];
			default:
				return 0;
		}
	}
	
	public void fixFieldTypes(List<FieldDescriber> world){
		// spawnFieldek
		//for(int o=0;o<2;o++){
			foreach(FieldDescriber f in world){
				if(f.getIndex().x == 0 || f.getIndex().y == 0 || f.getIndex().x == spaceSize-1 || f.getIndex().y == spaceSize-1/*f.getGen(0)==1 /*&& f.getEntryPoint()>0*/){
					int notPossible = 0;
					if(f.getEntryPoint()==0){
						if(f.getIndex().x == 0){f.setGeneric(4,2);notPossible++;}
						if(f.getIndex().x == spaceSize-1){f.setGeneric(3,2);notPossible++;}
						if(f.getIndex().y == 0){f.setGeneric(2,2);notPossible++;}
						if(f.getIndex().y == spaceSize-1){f.setGeneric(1,2);notPossible++;}
					}
					if(f.getIndex().y<spaceSize-1){
						FieldDescriber higherItem = world[GetArrayByIndex(new Vector2(f.getIndex().x,f.getIndex().y+1))];
						int num = higherItem.getType();
						if(higherItem.isFinal()){
							if( num==1 || num==2 || num==4 || num==6 || num==7 || num==8 || num==11){f.setGeneric(1,1);}
							else{f.setGeneric(1,2);notPossible++;}
							//f.setFinal(true);
						}
					}
					if(f.getIndex().y> 0){
						FieldDescriber lowerItem = world[GetArrayByIndex(new Vector2(f.getIndex().x,f.getIndex().y-1))];
						int num = lowerItem.getType();
						if(lowerItem.isFinal()){
							if( num==1 || num==2 || num==4 || num==5 || num==6 || num==9 || num==10){f.setGeneric(2,1);}
							else{f.setGeneric(2,2);notPossible++;}
							//f.setFinal(true);
						}
					}
					if(f.getIndex().x<spaceSize-1){
						FieldDescriber higherItem = world[GetArrayByIndex(new Vector2(f.getIndex().x+1,f.getIndex().y))];
						int num = higherItem.getType();
						if(higherItem.isFinal()){
							if( num==1 || num==3 || num==4 || num==5 || num==7 || num==8 || num==9){f.setGeneric(3,1);}
							else{f.setGeneric(3,2);notPossible++;}
							//f.setFinal(true);
						}
					}
					if(f.getIndex().x> 0){
						FieldDescriber lowerItem = world[GetArrayByIndex(new Vector2(f.getIndex().x-1,f.getIndex().y))];
						int num = lowerItem.getType();
						if(lowerItem.isFinal()){
							if( num==1 || num==3 || num==5 || num==6 || num==7 || num==10 || num==11){f.setGeneric(4,1);}
							else{f.setGeneric(4,2);notPossible++;}
							//f.setFinal(true);
						}
					}
					//if(f.getEntryPoint()==0 && notPossible>2){f.setType(12);f.setFinal(true);}
					if(f.isFinal() == false){
					f.CalculateFirstType();
					f.setFinal(true);
					}
				}
			}	
		//}
			//többi		
			int NotFinal = 1;
			while(NotFinal>0){
				NotFinal = 0;
				foreach(FieldDescriber f in world){
							int notPossible = 0;
							List<int> randDir = new List<int>();
						/*if(f.getIndex().y> 0 && f.getIndex().y<spaceSize-1){
							FieldDescriber higherItem = world[GetArrayByIndex(new Vector2(f.getIndex().x,f.getIndex().y+1))];
							int num = higherItem.getType();
							if(higherItem.isFinal()){
								if( num==1 || num==2 || num==4 || num==6 || num==7 || num==8 || num==11){f.setGeneric(1,1);}
								else{f.setGeneric(1,2);randDir.Add(1);}
								f.setFinal(true);
							}
							FieldDescriber lowerItem = world[GetArrayByIndex(new Vector2(f.getIndex().x,f.getIndex().y-1))];
							num = lowerItem.getType();
							if(lowerItem.isFinal()){
								if( num==1 || num==2 || num==4 || num==5 || num==6 || num==9 || num==10){f.setGeneric(2,1);}
								else{f.setGeneric(2,2);randDir.Add(2);}
								f.setFinal(true);
							}
						}*/
						if(f.getIndex().y<spaceSize-1){
							FieldDescriber higherItem = world[GetArrayByIndex(new Vector2(f.getIndex().x,f.getIndex().y+1))];
							int num = higherItem.getType();
							if(higherItem.isFinal()){
								if( num==1 || num==2 || num==4 || num==6 || num==7 || num==8 || num==11){f.setGeneric(1,1);}
								else{f.setGeneric(1,2);if(num!=12){randDir.Add(1);}notPossible++;}
								//f.setFinal(true);
							}
						}
						if(f.getIndex().y> 0){
							FieldDescriber lowerItem = world[GetArrayByIndex(new Vector2(f.getIndex().x,f.getIndex().y-1))];
							int num = lowerItem.getType();
							if(lowerItem.isFinal()){
								if( num==1 || num==2 || num==4 || num==5 || num==6 || num==9 || num==10){f.setGeneric(2,1);}
								else{f.setGeneric(2,2);if(num!=12){randDir.Add(2);};notPossible++;}
								//f.setFinal(true);
							}
						}
						/*if(f.getIndex().x> 0 && f.getIndex().x<spaceSize-1){
							FieldDescriber higherItem2 = world[GetArrayByIndex(new Vector2(f.getIndex().x+1,f.getIndex().y))];
							int num = higherItem2.getType();
							if(higherItem2.isFinal()){
								if( num==1 || num==3 || num==4 || num==5 || num==7 || num==8 || num==9){f.setGeneric(3,1);}
								else{f.setGeneric(3,2);randDir.Add(3);}
								f.setFinal(true);
							}
							FieldDescriber lowerItem2 = world[GetArrayByIndex(new Vector2(f.getIndex().x-1,f.getIndex().y))];
							num = lowerItem2.getType();
							if(lowerItem2.isFinal()){
								if( num==1 || num==3 || num==5 || num==6 || num==7 || num==10 || num==11){f.setGeneric(4,1);}
								else{f.setGeneric(4,2);randDir.Add(4);}
								f.setFinal(true);
							}
						}*/
							if(f.getIndex().x<spaceSize-1){
								FieldDescriber higherItem = world[GetArrayByIndex(new Vector2(f.getIndex().x+1,f.getIndex().y))];
								int num = higherItem.getType();
								if(higherItem.isFinal()){
									if( num==1 || num==3 || num==4 || num==5 || num==7 || num==8 || num==9){f.setGeneric(3,1);}
									else{f.setGeneric(3,2);if(num!=12){randDir.Add(3);}notPossible++;}
									//f.setFinal(true);
								}
							}
							if(f.getIndex().x> 0){
								FieldDescriber lowerItem = world[GetArrayByIndex(new Vector2(f.getIndex().x-1,f.getIndex().y))];
								int num = lowerItem.getType();
								if(lowerItem.isFinal()){
									if( num==1 || num==3 || num==5 || num==6 || num==7 || num==10 || num==11){f.setGeneric(4,1);}
									else{f.setGeneric(4,2);if(num!=12){randDir.Add(4);}notPossible++;}
									//f.setFinal(true);
								}
							}
							//if(notPossible>2){f.setFinal(false);}
							while(notPossible>2 && f.getType()!=12/* && f.isFinal() == false*/){
								NotFinal = 1;
								if(randDir.Count<2){break;}
								int dir = randDir[Random.Range(1,randDir.Count)];
								foreach(int v in randDir){if(dir==v){randDir.Remove(v);break;}}
								FieldDescriber item;
								switch(dir){
									case 1: item = world[GetArrayByIndex(new Vector2(f.getIndex().x,f.getIndex().y+1))];
									break;
									case 2: item = world[GetArrayByIndex(new Vector2(f.getIndex().x,f.getIndex().y-1))];
									break;
									case 3: item = world[GetArrayByIndex(new Vector2(f.getIndex().x+1,f.getIndex().y))];
									break;
									default: item = world[GetArrayByIndex(new Vector2(f.getIndex().x-1,f.getIndex().y))];
									break;
								}
								item.setFinal(false);
								f.setGeneric(dir,1);
								notPossible--;
							}
						if(f.isFinal() == false){
							f.CalculateFirstType();
							f.setFinal(true);
						}
				}			
			}
	}
	
	public void determineCrossroads(List<FieldDescriber> world){
		int crossNum = 0;
		foreach(FieldDescriber f in world){
			if(f.getType() == 1 || f.getType() == 4 || f.getType() == 5 || f.getType() == 6 || f.getType() == 7){
				crossNum++;
				//Crossroad c = new Crossroad();
				Crossroad c = Crossroad.GetComponent<Crossroad>();
				//c.GetComponent<Renderer>().material.mainTexture = texture;
				Vector3 spawnPosition = new Vector3((float)fieldSize*f.getIndex().x,0.01f,(float)fieldSize*f.getIndex().y);
				Crossroad cross = (Crossroad) Instantiate(c, spawnPosition,Quaternion.AngleAxis(90.0f, Vector3.right));
				cross.name = "Crossroad "+ crossNum;
				cross.Place = f.getIndex();
				cross.setField(f);
				
				//Debug.logger.Log("Ho");
				//Debug.logger.Log("Indulas: "+f.getIndex());
				int routeNum = 0;
				foreach(Vector2 key in f.getCurves().Keys){
					int source = (int) key.x/1000;
					//Debug.logger.Log("Index"+f.getIndex());
					//Debug.logger.Log(key);
					Route route = Route.GetComponent<Route>();
					Route dRoute = (Route) Instantiate(route, new Vector3(),Quaternion.AngleAxis(0.0f, Vector3.up));
					dRoute.name = "Route " + routeNum;
					routeNum++;
					dRoute.startPoint = f.getIndex();
					dRoute.addRoutePoint(dRoute.startPoint);
					dRoute.Source = source;
					dRoute.addCurve(f.getCurveByS(key));
					dRoute.startWeight = 1; // egyelőre
					dRoute.endWeight = 1; // egyelőre
					dRoute.routeWeight = 1; // egyelőre
					Vector2 lastCoord = f.getIndex();
					Vector2 nextCoord = new Vector2(key.x - (source*1000), key.y - (source*1000));
					bool innerpoint = true;
					int count = 0;
					int newSource = source;
					while(innerpoint){
						count++;
						if(nextCoord.x >= 0 && nextCoord.y >= 0 && nextCoord.x < spaceSize && nextCoord.y < spaceSize){
							dRoute.addRoutePoint(nextCoord);
							//Debug.logger.Log("ellenorzes: "+nextCoord);
							FieldDescriber nextfield = world[GetArrayByIndex(nextCoord)];
							//Debug.logger.Log("Route: "+dRoute.name+". Source: "+source+". Destination: "+newSource+". Lepes:"+count+". Elozo: "+lastCoord+". Kov: "+nextCoord+" NextType: "+nextfield.getType());
							if(nextfield.getType() == 1 || nextfield.getType() == 4 || nextfield.getType() == 5 || nextfield.getType() == 6 || nextfield.getType() == 7){
								innerpoint = false;
								dRoute.endPoint = nextCoord;
								dRoute.Source = source;
								newSource = 0;
								if(nextCoord.x > lastCoord.x){newSource=3;}
								else if(nextCoord.x < lastCoord.x){newSource=1;}
								else if(nextCoord.y < lastCoord.y){newSource=2;}
								else if(nextCoord.y > lastCoord.y){newSource=0;}
								else{newSource = 4;}
								dRoute.Destination = newSource;
								//Debug.logger.Log("Endpoint: "+dRoute.endPoint);
								cross.addRoute(source,dRoute);
							}
							else if(nextfield.getType() == 12){innerpoint = false;}
							else{
								//route.addRoutePoint(nextfield.getIndex());
								newSource = 0;
								if(nextCoord.x > lastCoord.x){newSource=3;}
								else if(nextCoord.x < lastCoord.x){newSource=1;}
								else if(nextCoord.y < lastCoord.y){newSource=2;}
								else if(nextCoord.y > lastCoord.y){newSource=0;}
								else{newSource = 4;}
								//if(count>4){Debug.logger.Log("Voltunk: "+lastCoord);}
								//if(count>4){Debug.logger.Log("Hely: "+nextfield.getIndex());}
								//if(count>4){Debug.logger.Log("Kene: "+newSource);}
								foreach(Vector2 nextkey in nextfield.getCurves().Keys){
									int match = (int) nextkey.x/1000;
									//if(count>4){Debug.logger.Log("Lista: "+nextkey);Debug.logger.Log("Match: "+match);}
									if(match==newSource){
										lastCoord = nextCoord;
										nextCoord = new Vector2(nextkey.x - (match*1000), nextkey.y - (match*1000));
										//route.addRoutePoint(nextCoord);
										//BezierCurve cur = nextfield.getCurveByS(nextkey);
										//cur.drawColor = Color.grey;
										dRoute.addCurve(nextfield.getCurveByS(nextkey));
										//Debug.Log (nextfield.getIndex());
										//break;
									}
									else if(newSource == 4){
										//Debug.logger.Log("Route: "+dRoute.name+". Source: "+newSource+". Lepes:"+count+". Elozo: "+lastCoord+". Kov: "+nextCoord+" NextType: "+nextfield.getType());
										innerpoint = false;
										dRoute.endPoint = nextCoord;
										//c.addRoute(source,route);
										//c.addRoute(source,route);
									}
								}	
								
								//route.addCurve(nextfield.getCurve(newSource,));
							}
							//c.addRoute(source,route);
						}
						else{
							innerpoint = false;
						}
						if(count>10){/*Debug.Log("FIVE");*/innerpoint = false;}
						//if(count>20){innerpoint = false;}
					}
					
				}
				//asd.name = "yay";
				crosses.Add(cross);
			}
		}
		foreach(Crossroad cross in crosses){
			for(int j=0;j<4;j++){
				List<Route> rts = cross.getRoute(j);
				foreach(Route route in rts){
					foreach(Crossroad crs in crosses){
						if(route.endPoint == crs.Place){
							route.endCross = crs;
							break;
						}
					}
				}
			}
		}
	}
	
	protected void addNewCar(int i){
		CarHandler car = (CarHandler) Instantiate(car1,new Vector3(3*i,0,-10),Quaternion.AngleAxis(0.0f, Vector3.up));
		car.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
		car.name = "MyCar "+ (i+1);
		carList.Add(car);
	}
	
	public bool checkIsAlone(CarHandler car){
		int temp = 0;
		foreach(CarHandler c in carList){
			if((car.getOrientation()==c.getOrientation()) && (car.getStartPlace()==c.getStartPlace())){
				temp++;
			}
		}
		if(temp>1){return false;}
		return true;
	}
	
	public void addCars(){
		for(int i=0; i<carsNumber; i++){addNewCar(i);}
		camera.addCarList(carList);
		//int j=0;
		foreach(CarHandler c in carList){
			setCarCross(world,c);
			if(checkIsAlone(c)){
				rideCar(c);
			}
			//else{carList.Remove(c);}
			//j++;
		}
		
	}
	
	public void setCarCross(List<FieldDescriber> world, CarHandler car){
		bool search = false;
		while(!search){
		int chosenOne = Random.Range(0,crosses.Count);
		int place = Random.Range(0,4);
		int nonAvailable = world[GetArrayByIndex(crosses[chosenOne].Place)].getType()-3;
		//Debug.Log(nonAvailable);
		if(nonAvailable%2==0){nonAvailable=nonAvailable-2;}
		if(place == nonAvailable){place++;}
		if(place>3){place = place - 4;}
		//Debug.Log(nonAvailable);
		search = car.setTrip(crosses[chosenOne],place,spaceSize);
		}
	}
	
	public void rideCar(CarHandler car){
		/*Route longest = Route;
		int length = 0;
		foreach(Crossroad c in crosses){
			foreach(Route route in c.bRoutes){if(route.getCurveList().Count>length){length = route.getCurveList().Count; longest = route;}}
			foreach(Route route in c.rRoutes){if(route.getCurveList().Count>length){length = route.getCurveList().Count; longest = route;}}
			foreach(Route route in c.uRoutes){if(route.getCurveList().Count>length){length = route.getCurveList().Count; longest = route;}}
			foreach(Route route in c.lRoutes){if(route.getCurveList().Count>length){length = route.getCurveList().Count; longest = route;}}
		}
		car1.setStart(longest.getCurveList()[0]);*/
		car.Iterate();
	}
	
	FieldDescriber CreateField(int posx, int posy){
		Vector3 spawnPosition = new Vector3((float)fieldSize*posx,0.0f,(float)fieldSize*posy);
		Vector2 index = new Vector2(posx,posy);
		Quaternion spawnRotation = Quaternion.AngleAxis(90, Vector3.right);
		FieldDescriber desc = Field.GetComponent<FieldDescriber>();
		desc.setType(Random.Range(1,13));
		desc.setIndex(index);
		desc.setEntryPoint(0);
		desc.setAllGen(0);
		desc.setFinal(false);
		desc.setPos(spawnPosition);
		//desc.CalculateType();
		//desc.GetComponent<Renderer>().material.color = Color.black;
		GameObject exactField = (GameObject) Instantiate (Field, spawnPosition,spawnRotation);
		FieldDescriber describedField = exactField.GetComponent<FieldDescriber>();
		return describedField;
	}

}