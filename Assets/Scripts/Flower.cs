using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

	
	// Use this for initialization
	enum States {OnFire, Idle, Burned};
	private string tagCheck = "flammable";
	[SerializeField] States currentState;
	[SerializeField] int interval = 2;
	[SerializeField] int repetition = 3;
	[SerializeField] int sphereHeight = 10;
	private RaycastHit[] hit; 
	public Material green;
	public Material flame;
	public Material burned;
	
	// coroutine pointer for start and stop of coroutine
	private IEnumerator spreadCoroutine;

	public void SetFire(){
		// Set fire
		Renderer rend = GetComponent<Renderer>();
		rend.material = flame;
		print("fire!");
		spreadCoroutine = SpreadFire(interval,repetition);
		StartCoroutine(SpreadFire(interval, repetition));
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

	IEnumerator SpreadFire(int interval, int repetition){
		while (repetition > 0)
		{
			print("cycle" + repetition.ToString());
			var direction = Quaternion.AngleAxis(30, transform.up) * transform.forward;
			Debug.DrawRay(transform.position, direction, Color.red,15);
			Debug.DrawRay(transform.position, Vector3.forward, Color.red,15);
			RaycastHit[] hits = Physics.SphereCastAll(transform.position, sphereHeight, direction, 10);
			foreach (var hit in hits)
			{
				if (hit.transform.tag == tagCheck)
				{
					hit.transform.gameObject.SendMessage("SetFire");
				}
			}
			yield return new WaitForSeconds(interval);
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
		var forward = transform.TransformDirection(Vector3.forward) * 10;
		Debug.DrawRay (transform.position, Vector3.forward * 100000000, Color.red);
		switch (currentState)
		{
			case States.OnFire:
				SetFire();
				Debug.DrawRay(transform.position, forward, Color.red,15, false);
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
