using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public List<Transform> spawnPoints;	
	public GameObject flammable;
	public List<GameObject> clones;
	

	public void spawnFlammable(){
		clones.Add(Instantiate(flammable,spawnPoints[0].transform.position, Quaternion.Euler(0, 0 ,0)) as GameObject);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
