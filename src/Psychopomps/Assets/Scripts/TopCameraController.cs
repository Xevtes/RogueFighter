using UnityEngine;
using System.Collections;

public class TopCameraController : MonoBehaviour {

	public GameObject follow;//Set which gameObject to follow
	
	private Vector3 offset; //The distance to be found between the camera and player's Transform position
	
	void Start(){
		offset = transform.position - follow.transform.position; //Find offset to difference between the camera and player's Transform position
	}
	
	void LateUpdate(){
		transform.position = follow.transform.position + offset;// Make camera's position the player's + the offset value
	}
}