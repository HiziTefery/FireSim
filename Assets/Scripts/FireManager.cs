using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour {

	public List<GameObject> flammables = new List<GameObject>();

	private void Start() {
		Object flowerPrefab = Resources.Load("Prefabs/flower", typeof(GameObject));
	}

	public void removeFlamable(Flammable obj){
		
	}

	public void addFlammable(Flammable obj){

	}

	public void toggleFire(Flammable obj){
		
	}

	public void modifyWindSpeed(){

	}

	public void modifyWindDirection(){

	}

	public void clearFlammables(){

	}

	public void generateFlammables(){

	}
}
