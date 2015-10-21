using UnityEngine;
using System.Collections;

public class PlayerLogic : MonoBehaviour 
{
	private CharacterController controller;
	public float speed = 6.0f;
	public float turnSpeed = 60.0f;

	private Vector3 moveDirection = Vector3.zero;
	private Animator anim;

	void Start(){
		controller = GetComponent <CharacterController>();
		anim = gameObject.GetComponentInChildren<Animator>();
	}

	void Update(){
		float turn = Input.GetAxis("Horizontal");
		transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
		moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
		anim.SetFloat ("speed", controller.velocity.magnitude);
		controller.Move(moveDirection * Time.deltaTime);

		if (Input.GetButton ("Attack")){
			Attack();
		}
	}

	void Attack(){
		anim.SetBool("playerAttack", false);
	 
		if(Input.GetButtonDown ("Attack")){
			anim.SetBool("playerAttack", true);
		}
	}
}
