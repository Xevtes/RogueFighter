var Distance;
var Target : Transform;
var lookAtDistance = 25.0;
var attackDistance = 15.0;
var moveSpeed = 5.0;
var Damping = 6.0;
var anim : Animator;

function Start()
{
	anim = GetComponent ("Animator");	
}

function Update ()
{
	Distance = Vector3.Distance(Target.position, transform.position);
	
	if (Distance < lookAtDistance)
	{
		//renderer.material.color = Color.yellow;
		lookAt();
	}
	
	if (Distance > lookAtDistance)
	{
		//renderer.material.color = Color.green;
		idle();
	}
	
	if (Distance < attackDistance)
	{
		//renderer.material.color = Color.red;
		move ();
	}
}

function idle()
{
	anim.SetBool("enemyIdle", true);
	anim.SetBool("enemyWalk", false);
}

function lookAt ()
{
	var rotation = Quaternion.LookRotation(Target.position - transform.position);
	transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
	anim.SetBool("enemyIdle", true);
	anim.SetBool("enemyWalk", false);
}

function move ()
{
	transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	anim.SetBool("enemyWalk", true);
}
