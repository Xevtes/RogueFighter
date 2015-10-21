#pragma strict

var MaxHealth = 100;
var Health : int;

function Start ()
{
	Health = MaxHealth;
}

function ApplyDamage (TheDamage : int)
{
	Health -= TheDamage;
	
	if(Health <= 0)
	{
		Dead();
	}
}

function Dead()
{
	RespawnMenuV2.playerIsDead = true;
	Debug.Log("Player Died");
}

function RespawnStats ()
{
	Health = MaxHealth;
}