using UnityEngine;
using System.Collections;

public class MarbhAI : MonoBehaviour {

	public CharacterController controller;
	public Transform target;
	public Animator anim;
	public Transform respawnLocation;

	public float lookAtRange = 25.0f;
	public float chaseRange = 15.0f;
	public float attackRange = 1.5f;
	public float moveSpeed = 5.0f;
	public float damping = 6.0f;
	public float attackRepeatTime = 1f;
	public float TheDamage = 40f;
	public float maxHealth = 100f;
	public float currentHealth;
	
	private float attackTime;
	private Vector3 moveDirection = Vector3.zero;
	private float distance;

	

	// Use this for initialization
	void Start(){
		anim = gameObject.GetComponentInChildren<Animator>();
		controller = GetComponent <CharacterController>();
		attackTime = Time.time;
		currentHealth = maxHealth;

	}
	
	// Update is called once per frame
	void Update (){
		distance = Vector3.Distance(target.position, transform.position);

		if(currentHealth <= 0){
			RespawnPlayer();
		}

		if (distance < lookAtRange){
			LookAt();
		}
		
		if (distance > lookAtRange){
			Idle();
		}
		
		if (distance < attackRange){
			Attack();
		}

		else if (distance < chaseRange){
			Chase();
		}	
	}

	//Play idle animation
	void Idle(){
		anim.SetBool("enemyIdle", true);
		anim.SetBool("enemyWalk", false);
		anim.SetBool("enemyAttack", false);
	}

	//When in range, turn to look at player
	void LookAt(){
		anim.SetBool("enemyIdle", true);
		anim.SetBool("enemyWalk", false);
		anim.SetBool("enemyAttack", false);
		var rotation = Quaternion.LookRotation(target.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
	}

	//When in range, chase player
	void Chase(){
		anim.SetBool("enemyWalk", true);
		moveDirection = transform.forward;
		moveDirection *= moveSpeed;
		controller.Move(moveDirection * Time.deltaTime);
	}
	
	void Attack(){
		if (Time.time > attackTime){
			anim.SetBool("enemyAttack", true);
			target.SendMessage("ApplyDamage", TheDamage);
			Debug.Log("Marbh Has Attacked");
			attackTime = Time.time + attackRepeatTime;
		}
	}

	void ApplyDamage(){
		currentHealth -= TheDamage;
	}

	void RespawnPlayer(){
		transform.position = respawnLocation.position;
		transform.rotation = respawnLocation.rotation;
		gameObject.SendMessage("RespawnStats");
		Debug.Log("Enemy has respawned");
	}

	void RespawnStats(){
		currentHealth = maxHealth;
	}
}