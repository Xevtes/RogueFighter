//Steve Graphos
using UnityEngine;
using System.Collections;

public class Player1Controller : MonoBehaviour {

	CharacterController cc;
	float moveSpeed = 5;
	float rotationSpeed = 180;

	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		float v = Input.GetAxisRaw ("WASDVertical");
		float h = Input.GetAxisRaw ("WASDHorizontal");
		
		transform.Rotate (new Vector3 (0, h * rotationSpeed * Time.deltaTime, 0));
		
		Vector3 speed = transform.forward * v * moveSpeed;
		
		cc.SimpleMove (speed);
	}
}
