#pragma strict

var MaxHealth = 100;
var Health : int;
var respawnTransform : Transform;

function Start () 
{
	Health = MaxHealth;
}
function Update()
{
	if(Health <= 0)
	{
		RespawnPlayer();
	}
}

function ApplyDamage (TheDamage : int)
{
	Health -= TheDamage;
}

function RespawnPlayer ()
{
	transform.position = respawnTransform.position;
	transform.rotation = respawnTransform.rotation;
	gameObject.SendMessage("RespawnStats");
	Debug.Log("Enemy has respawned");
}
function RespawnStats ()
{
	Health = MaxHealth;
}