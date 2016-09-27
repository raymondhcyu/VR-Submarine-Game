using UnityEngine;
using System.Collections;

public class SubmarineController : MonoBehaviour {

	public float moveSpeed = 10; // initial default values; Unity will override these
	public float rotateSpeed = 10; // initial default values; Unity will override these
	public int numTorpedoes = 4; // limit number of torpedoes that can be fired

	// Public: other scripts can access submarine, vs private 
	public GameObject submarine; // declare submarine
	public Camera gvrCamera; // declare VR camera
	public GameObject torpedoPrefab; // declare torp

	public Transform leftFirePosition; // type transform = spot in space/world
	public Transform rightFirePosition;  
	public Transform convergencePoint; 

	// private bool fireLeftTorpedo = true; // bool is true/false value, torpedo fire sequence

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	// Update: Called once per frame; VR is 90fps ideal, but 60fps ok.
	// "Every frame update location/movement/direction"
	void Update () {
		UpdateRotation ();
		UpdateMovement ();
	}
		
	void UpdateRotation () {
		// Move camera
		Quaternion cameraLookRotation = Quaternion.LookRotation (
			gvrCamera.transform.forward, gvrCamera.transform.up); 
		// Quaternion is a type, like Camera and GameObject.
		// "dot" = access from this property

		// For submarine to follow camera
		submarine.transform.localRotation = Quaternion.RotateTowards (
			submarine.transform.rotation, cameraLookRotation, rotateSpeed * Time.deltaTime); 
		// Time multiply by amount of time passed since last update, so not dependent on game framerate
	}

	void UpdateMovement () {
		// "Get me component that is of CharacterController from GameObject"
		GetComponent<CharacterController> ().Move(submarine.transform.forward * moveSpeed * Time.deltaTime); 
		// 
	}

	/*public void FireTorpedo () {
		Vector3 torpedoFirePosition; // Vector3 is x, y, z position in space

		if (numTorpedoes > 0) { // check if there's enough torpedos remaining to fire

			if (fireLeftTorpedo) { // blank if implies if it is true
				torpedoFirePosition = leftFirePosition.position;
				fireLeftTorpedo = false;
			} else {
				torpedoFirePosition = rightFirePosition.position;
				fireLeftTorpedo = true;
			}

			// GameObject torpedoObject = Instantiate (torpedoPrefab, torpedoFirePosition, Quaternion.identity) as GameObject; 
			// Instantiate creates object, like game object but not, on the fly, need "as GameObject" to make GameObject
			// Instantiate = duplicate

			// torpedoObject.transform.forward = (convergencePoint.position - torpedoObject.transform.position).normalized;
			// "Our forward direction is.	.. and shift torp to forward direction"
			// aka unit vector in correct direction

			numTorpedoes -= 1; // subtract one torpedo each time fired
		} else {
			Debug.Log ("Out of torpedoes.");
		}
	}
	*/
}