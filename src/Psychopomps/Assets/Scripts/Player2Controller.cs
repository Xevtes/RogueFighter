using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax;
}


public class Player2Controller : MonoBehaviour {

	CharacterController cc;
	float moveSpeed = 5;
	float rotationSpeed = 180;

	public Boundary boundary;
	
		
	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		float v = Input.GetAxisRaw ("ArrowVertical");
		float h = Input.GetAxisRaw ("ArrowHorizontal");
		
		transform.Rotate (new Vector3 (0, h * rotationSpeed * Time.deltaTime, 0));
		
		Vector3 speed = transform.forward * v * moveSpeed;
		
		cc.SimpleMove (speed);
	}

	void FixedUpate(){

		GetComponent<Rigidbody>().position = new Vector3 
			(
				Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
				0.0f, 
				Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
			);

	}
}
