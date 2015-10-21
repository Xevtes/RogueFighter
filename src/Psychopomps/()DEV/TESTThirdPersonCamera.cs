using UnityEngine;
using System.Collections;

// Struct to hold data for aligning camera
struct CameraPosition
{
	// Position to align camera behind character
	private  Vector3 position;
	//Transform used for rotation
	private Transform xForm;

	public Vector3 Position { get { return position; } set { position = value; } }
	public Transform XForm { get { return xForm; } set { xForm = value; } }

	public void Init(string camName, Vector3 pos, Transform transform, Transform parent)
	{
		position = pos;
		xForm = transform;
		xForm.name = camName;
		xForm.parent = parent;
		xForm.localPosition = Vector3.zero;
		xForm.localPosition = position;
	}
}
public class ThirdPersonCamera : MonoBehaviour 
{
	#region Variables (private)
	[SerializeField]
	private float distanceAway;
	[SerializeField]
	private float distanceUp;
	[SerializeField]
	private Transform followXForm;

	//smoothing and damping
	private Vector3 velocityCamSmooth = Vector3.zero;
	[SerializeField]
	private float camSmoothDampTime = 0.1f;

	// Private global only
	private Vector3 lookDir;
	private Vector3 targetPosition;
	private CamStates camState = CamStates.Behind;
	private CameraPosition firstPersonCamPos;
	#endregion

	#region Properties (public)
	public enum CamStates
	{
		Behind,
		FirstPerson,
		Target,
		Free
	}
	#endregion

	#region Unity event functions
	// Use this for initialization
	void Start() 
	{
		followXForm = GameObject.FindWithTag("Player").transform;
		lookDir = followXForm.forward;

		//Place and parent a GameObject where first person view should be
		firstPersonCamPos = new CameraPosition ();
		firstPersonCamPos.Init
			(
				"First Person Camera",
				new Vector3(0.0f, 1.6f, 0.2f),
				new GameObject().transform,
				followXForm
			);
	}
	
	// Update is called once per frame
	void Update() 
	{
	
	}

	void OnDrawGizmos()
	{
	
	}

	void LateUpdate()
	{

		Vector3 characterOffset = followXForm.position + new Vector3 (0f, distanceUp, 0f);
		// Determine camera state
		if (Input.GetAxis ("XBoxTriggers") > 0.01f)
		{
			camState = CamStates.Target;
		}
		else
		{
			camState = CamStates.Behind;
		}

		// Execute camera state
		switch (camState)
		{
			case CamStates.Behind:
				// Calculate direction from camera to player, kill Y, and normalize to give a valid direction with unit magnitude
				lookDir = characterOffset - this.transform.position;
				lookDir.y = 0;
				lookDir.Normalize ();
				Debug.DrawRay (this.transform.position, lookDir, Color.green);

				//setting the target position to be the correct offset from the player
				targetPosition = characterOffset + followXForm.up * distanceUp - lookDir * distanceAway;
				//Debug.DrawRay(follow.position, Vector3.up * distanceUp, Color.red);
				//Debug.DrawRay(follow.position, -1f * follow.forward * distanceAway, Color.blue);
				Debug.DrawLine(followXForm.position, targetPosition, Color.magenta);
				break;
			case CamStates.Target:
				lookDir = followXForm.forward;
				break;
		
		}
		targetPosition = characterOffset + followXForm.up * distanceUp - lookDir * distanceAway;

		CompensateForWalls (characterOffset, ref targetPosition);

		//make smooth transitions between current position and upcoming positions
		smoothPosition (this.transform.position, targetPosition);

		//make the camera face the player
		transform.LookAt(followXForm);
	}
	#endregion

	#region Methods
	private void smoothPosition(Vector3 fromPos, Vector3 toPos)
	{
		// Make a smooth transition between camera's current and upcoming positions
		this.transform.position = Vector3.SmoothDamp (fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
	}

	private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
	{
		Debug.DrawLine (fromObject, toTarget, Color.cyan);
		// Compensate for walls between camera
		RaycastHit wallHit = new RaycastHit ();
		if(Physics.Linecast (fromObject, toTarget, out wallHit))
		{
			Debug.DrawRay (wallHit.point, Vector3.left, Color.red);
			toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z);
		}
	}

	#endregion Methods
}


