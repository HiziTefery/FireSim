using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour {

	
	// Use this for initialization
	enum States {OnFire, Idle, Burned};
	private string tagCheck = "flammable";
	[SerializeField] States currentState;
	[SerializeField] int interval = 2;
	[SerializeField] int repetition = 3;
	[SerializeField] int sphereHeight = 10;
	[SerializeField] int angle = 0;
	private bool coroutineStarted = false;
	private RaycastHit[] hit; 
	public Material green;
	public Material flame;
	public Material burned;
	
	// coroutine pointer for start and stop of coroutine
	private IEnumerator spreadCoroutine;

	public void SetFire(){
		// Set fire
		currentState = States.OnFire;
		// Burn after X seconds
	}

	public void ExtinguishFire(){
		Renderer rend = GetComponent<Renderer>();
		rend.material = burned;
		print("burned!");		
		if(spreadCoroutine != null){
			StopCoroutine(spreadCoroutine);
		}
	}

	IEnumerator SpreadFire(int angle, int interval, int repetition){
		coroutineStarted = true;
		while (repetition > 0)
		{	
			yield return new WaitForSeconds(interval);
			print("cycle" + repetition.ToString());
			var direction = Quaternion.AngleAxis(-angle + 90, transform.up) * transform.forward;
			RaycastHit[] hits = Physics.SphereCastAll(transform.position, sphereHeight, direction, 1000);
			print("Number of hits: " + hits.Length.ToString());
			foreach (var hit in hits)
			{
				print("distance to target: " + hit.distance.ToString());
				if (hit.transform.tag == tagCheck)
				{
					print("distance to target: " + hit.distance.ToString());
					hit.transform.gameObject.SendMessage("SetFire");
				}
			}
			repetition--;
		}
		currentState = States.Burned;
		print("burned");
		Renderer rend = GetComponent<Renderer>();
		rend.material = burned;
		yield break;
	}

	void Start () {
		currentState = States.Idle;
		Renderer rend = GetComponent<Renderer>();
		rend.material = green;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 noAngle = transform.forward;
		var direction = Quaternion.AngleAxis(-angle + 90 , transform.up) * transform.forward;
		Debug.DrawRay(transform.position, direction * 100, Color.red,1000000);
		switch (currentState)
		{
			case States.OnFire:
				if (!coroutineStarted){
					Renderer rend = GetComponent<Renderer>();
					rend.material = flame;
					print("fire!");
					spreadCoroutine = SpreadFire(angle, interval, repetition);
					StartCoroutine(SpreadFire(angle, interval, repetition));
				}
				// Debug.DrawRay(transform.position, forward, Color.red,15, false);
				break;
			case States.Burned:
				ExtinguishFire();
				break;
			case States.Idle:
				print("idle!");
				break;
			

		}
	}
}
