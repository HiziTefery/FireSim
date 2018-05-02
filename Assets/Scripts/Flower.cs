using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

	
	// Use this for initialization
	private enum States {OnFire, Idle, Burned};
	private string tagCheck = "flammable";
	private States currentState;
	[SerializeField] int interval = 5;
	[SerializeField] int repetition = 3;
	[SerializeField] int sphereHeight = 10;
	private RaycastHit[] hit; 
	public Material green;
	public Material flame;
	public Material burned;
	// coroutine pointer
	private IEnumerator spreadCoroutine;

	public void SetFire(){
		// Set fire
		currentState = States.OnFire;
		spreadCoroutine = SpreadFire(interval,repetition);
		// Burn after X seconds
	}

	public void extinguishFire(){
		currentState = States.Burned;
	}

	private void changeMaterial(){

	}

	IEnumerator SpreadFire(int interval, int repetition){
		while (repetition > 0)
		{
			 var direction1 = Quaternion.AngleAxis(30, transform.up) * transform.forward;
			 var direction = Quaternion.AngleAxis(30, transform.up) * transform.forward;
			// if (Physics.SphereCast(transform.position, sphereHeight, transform.forward, out hit,20) && hit.transform.tag == tagCheck && attacking)
			// {
			// }
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
		yield break;
	}

	void Start () {
		currentState = States.Idle;
		Renderer rend = GetComponent<Renderer>();
		rend.material = green;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
