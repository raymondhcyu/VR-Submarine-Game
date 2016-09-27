using UnityEngine;
using System.Collections;

public class MissileLauncher : MonoBehaviour {

	public Rigidbody Missile; // change name to temporary private variable?
	public float fireRate = 0.5f;
	private float nextFire = 0.0f;
	public float numTorpedoes;

	public Transform firePosition1; // fire position on left wing
	public Transform firePosition2; // fire position on right wing
	private Vector3 firePosition; // input into instantiate
	private bool firePos1 = true; // set initial fire condition

	// Use this for initialization
	void Start () {
		Rigidbody Missile = GetComponent <Rigidbody> (); // what is the point of this? for gravity/rigidbody effects to be present?
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Fire1") && Time.time >= nextFire && numTorpedoes > 0 && firePos1) { // if click, within fire rate, and have torps

			firePosition = firePosition2.position;
			Instantiate (Missile, firePosition, transform.rotation); // duplicate missile system prefab (built in code for guidance if applicable)
			nextFire = Time.time + fireRate; 
			numTorpedoes -= 1;
			firePos1 = false; // torps fire from position 2 now
			Debug.Log("Left");

		} 

		else if (Input.GetButtonDown ("Fire1") && Time.time >= nextFire && numTorpedoes > 0 && firePos1 == false) { // if click, within fire rate, and have torps

			firePosition = firePosition1.position;
			Instantiate (Missile, firePosition, transform.rotation); // duplicate missile system prefab (built in code for guidance if applicable)
			nextFire = Time.time + fireRate; 
			numTorpedoes -= 1;
			firePos1 = true; // restart loop so torps fire from fire position 1 again
			Debug.Log ("Right");

		} 

		else if (Input.GetButtonDown ("Fire1") && Time.time < nextFire) // fire rate limiter
			Debug.Log ("Too early!");
		
		else if (numTorpedoes <= 0) // inform user out of ammo
			Debug.Log ("Out of Torpedoes!");
	}
}
