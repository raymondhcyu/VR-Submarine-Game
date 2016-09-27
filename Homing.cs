using UnityEngine;
using System.Collections;

public class Homing : MonoBehaviour {

	public Rigidbody missilePrefab; 
	public GameObject missileMod; // 3D model of missile
	public float missileVelocity = 1000f;
	public float turnSpeed = 20f; 
	public float fuseDelay = 1.0f;
	public ParticleSystem WhiteSmoke; // missile smoke trail, ***NOTE: is already attached to and will start with missile prefab***
	public AudioClip missileClip; // missile launch audio
	private Transform target; // target with "target" tag
	private Quaternion aimDirection;

	public ParticleSystem explosion; // explosion particle system animation
	public AudioClip explosionSound;

	// Use this for initialization
	void Start () {
		
		Rigidbody missilePrefab = GetComponent <Rigidbody> ();
		Fire ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float step = turnSpeed * Time.deltaTime; // time.deltatime makes speed indepednent of frame rate. 10m/s instead of 10m/frame

		if (missilePrefab == null || target == null) { // check to see if missile system or target is present
			return;
		}

		missilePrefab.velocity = transform.forward * missileVelocity * Time.deltaTime; // propel missile system in forward direction * Time.deltatime 
		aimDirection = Quaternion.LookRotation(target.position - transform.position); // define rotation towards target
		missilePrefab.MoveRotation(Quaternion.RotateTowards(transform.rotation, aimDirection, step));  // move missile system to target

		// missilePrefab.transform.forward = (target.position - transform.position).normalized; 
		// missilePrefab.AddForce(missilePrefab.transform.forward * missileVelocity * Time.deltaTime, ForceMode.VelocityChange);

	}

	public void Fire () { 
		
		AudioSource.PlayClipAtPoint (missileClip, transform.position); 
		float distance = float.MaxValue; // assign distance to be equal to positive INF

		foreach (GameObject go in GameObject.FindGameObjectsWithTag("target")) { // search for label "target"
			
			float diff = (go.transform.position - transform.position).sqrMagnitude; // find distance & direction of travel?

			if (diff < distance) { // check if diff is positive INF
				distance = diff;
				target = go.transform; // assign final target value
			}
		}
	}

	void OnCollisionEnter (Collision thecollision){ // behaviour on collision with another gameobject
		
		if (thecollision.gameObject.name == "Cube") {

			WhiteSmoke.Stop (); // stops particle systems so it gradually fades out, but still "runs" in background
			// Destroy (this.GetComponent<ParticleSystem>()); // destroys particle system without fadeout
			Destroy (missileMod); // destroys 3D missile model
			Instantiate(explosion, transform.position, transform.rotation); // create explosion effect
			AudioSource.PlayClipAtPoint (explosionSound, transform.position); // play missile explosion sound
			return;
		
		}
	}
}
