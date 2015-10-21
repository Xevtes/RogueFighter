#pragma strict

var TheDamage : int = 50;
var Distance : float;
var MaxDistance : float = 1.5;
var TheSystem : Transform;

function Update ()
{
	//Attack function
	var hit : RaycastHit;
	if (Physics.Raycast (TheSystem.transform.position, TheSystem.transform.TransformDirection(Vector3.forward), hit))
	{
		Distance = hit.distance;
		if (Distance < MaxDistance)
		{
			hit.transform.SendMessage("ApplyDamage", TheDamage, SendMessageOptions.DontRequireReceiver);
		}
	}
}