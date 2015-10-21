/// <summary>
/// UnityTutorials - A Unity Game Design Prototyping Sandbox
/// <copyright>(c) John McElmurray and Julian Adams 2013</copyright>
/// 
/// UnityTutorials homepage: https://github.com/jm991/UnityTutorials
/// 
/// This software is provided 'as-is', without any express or implied
/// warranty.  In no event will the authors be held liable for any damages
/// arising from the use of this software.
///
/// Permission is granted to anyone to use this software for any purpose,
/// and to alter it and redistribute it freely, subject to the followXFormXForming restrictions:
///
/// 1. The origin of this software must not be misrepresented; you must not
/// claim that you wrote the original software. If you use this software
/// in a product, an acknowledgment in the product documentation would be
/// appreciated but is not required.
/// 2. Altered source versions must be plainly marked as such, and must not be
/// misrepresented as being the original software.
/// 3. This notice may not be removed or altered from any source distribution.
/// </summary>

using UnityEngine;
using System.Collections;

// DESCRIPTION OF CLASS
public class CharacterControllerLogic : MonoBehaviour 
{



	#region Variables (private)
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private float directionDampTime = .25f;
	[SerializeField]
	private ThirdPersonCamera gamecam;
	[SerializeField]
	private float directionSpeed = 3.0f;
	[SerializeField]
	private float rotationDegreePerSecond = 120f;

	// Private global only
	private float speed = 0.0f;
	private float direction = 0f;
	private float horizontal = 0.0f;
	private float vertical = 0.0f;
	private AnimatorStateInfo stateInfo;

	// Hashes
	private int m_MovementId = 0;
	#endregion

	#region Properties (public)
	#endregion

	#region Unity event functions
	// Use this for initiailization
	void Start() 
	{
		animator = GetComponent<Animator> ();
		if (animator.layerCount >= 2)
		{
			animator.SetLayerWeight (1, 1);
		}

		// Hash all animation names for performance
		m_MovementId = Animator.StringToHash ("Base Layer.Movement");
	}

	// Update called once per fream
	void Update() 
	{
		if (animator) 
		{
			stateInfo = animator.GetCurrentAnimatorStateInfo(0);
			// Pulls values from controller/keyboard
			horizontal = Input.GetAxis ("Horizontal");
			vertical = Input.GetAxis ("Vertical");

//			// Translate controls stick coords into world/cam/character space
//			StickToWorldspace(this.transform, gamecam.transform, ref direction, ref speed);

			animator.SetFloat("Speed", speed);
			animator.SetFloat("Direction", direction, directionDampTime, Time.deltaTime);
		}
	}

	// Code that moves the character needs to be checked against physics
	void FixedUpdate()
	{
		// Rotate character if stick is tilted left or right, but only if moving in that direction
		if (IsInMovement () && ((direction >= 0 && horizontal >= 0) || (direction < 0 && horizontal < 0))) 
		{
			Vector3 rotationAmount = Vector3.Lerp (Vector3.zero, new Vector3 (0f, rotationDegreePerSecond * (horizontal < 0f ? -1f : 1f), 0f), Mathf.Abs (horizontal));
			Quaternion deltaRotation = Quaternion.Euler (rotationAmount * Time.deltaTime); 
			this.transform.rotation = (this.transform.rotation * deltaRotation);
		}
	}

//	void OnAnimatorMove()
//	{
//		Animator animator = GetComponent<Animator>(); 
//		
//		if (animator)
//		{
//			Vector3 newPosition = transform.position;
//			newPosition.z += animator.GetFloat("move") * Time.deltaTime; 
//			transform.position = newPosition;
//		}
//	}

	#endregion


	#region Methods	
	// Debugging information should be put here
	public void StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut)
	{	
		Vector3 rootDirection = root.forward;
		Vector3 stickDirection = new Vector3(horizontal, 0, vertical);
		speedOut = stickDirection.sqrMagnitude;

		//Get camera rotation
		Vector3 CameraDirection = camera.forward;
		CameraDirection.y = 0.0f; // No Y values
		Quaternion referentialShift = Quaternion.FromToRotation (Vector3.forward, CameraDirection);

		//Convert jostick input in Worldspace coordinates
		Vector3 moveDirection = referentialShift * stickDirection;
		Vector3 axisSign = Vector3.Cross (moveDirection, rootDirection);

		Debug.DrawRay (new Vector3 (root.position.y + 2f, root.position.z), moveDirection, Color.green);
		Debug.DrawRay (new Vector3 (root.position.y + 2f, root.position.z), axisSign, Color.red);
		Debug.DrawRay (new Vector3 (root.position.y + 2f, root.position.z), rootDirection, Color.magenta);
		Debug.DrawRay (new Vector3 (root.position.y + 2f, root.position.z), stickDirection, Color.blue);

		float angleRootToMove = Vector3.Angle (rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

		angleRootToMove /= 180f;

		directionOut = angleRootToMove * directionSpeed;
	}
	
	public bool IsInMovement()
			{
				return stateInfo.nameHash == m_MovementId;
			}
	#endregion Methods
}
	
