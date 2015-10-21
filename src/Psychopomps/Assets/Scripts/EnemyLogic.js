var Distance;
var Target : Transform;
var lookAtDistance = 25.0;
var chaseRange = 15.0;
var attackRange = 1.5;
var moveSpeed = 5.0;
var Damping = 6.0;
var attackRepeatTime = 1;
var anim : Animator;
var TheDamage = 40;

private var attackTime : float;

var controller : CharacterController;
private var MoveDirection : Vector3 = Vector3.zero;

function Start ()
{
	anim = GetComponent ("Animator");
	attackTime = Time.time;
}

function Update ()
{
	Distance = Vector3.Distance(Target.position, transform.position);
	
	if (Distance < lookAtDistance)
	{
		lookAt();
	}
	
	if (Distance > lookAtDistance)
	{
		//renderer.material.color = Color.green;
		idle();
	}
	
	if (Distance < attackRange)
	{
		attack();
	}
	else if (Distance < chaseRange)
	{
		chase ();
	}

}

function idle()
{
	anim.SetBool("enemyIdle", true);
	anim.SetBool("enemyWalk", false);
	anim.SetBool("enemyAttack", false);
}

function lookAt ()
{
	//renderer.material.color = Color.yellow;
	anim.SetBool("enemyIdle", true);
	anim.SetBool("enemyWalk", false);
	anim.SetBool("enemyAttack", false);
	var rotation = Quaternion.LookRotation(Target.position - transform.position);
	transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
}

function chase ()
{
	
	//renderer.material.color = Color.red;
	anim.SetBool("enemyWalk", true);
	moveDirection = transform.forward;
	moveDirection *= moveSpeed;
	controller.Move(moveDirection * Time.deltaTime);
}

function attack ()
{
	if (Time.time > attackTime)
	{
		anim.SetBool("enemyAttack", true);
		Target.SendMessage("ApplyDamage", TheDamage);
		Debug.Log("The Enemy Has Attacked");
		attackTime = Time.time + attackRepeatTime;
	}
}

//function ApplyDamage ()
//{
//	chaseRange += 30;
//	moveSpeed += 2;
//	lookAtDistance += 40;
//}
