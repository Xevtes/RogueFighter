using UnityEngine;
using System.Collections;

public class Scene01 : MonoBehaviour {

	public void PlayGame(){

		GameObject obj = GameObject.Find ("StayCube");
		DontDestroyOnLoad (obj);

		Application.LoadLevel ("Scene02");

	}
}
