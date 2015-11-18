//Steve Graphos
using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	public void PlayGame(){
		Application.LoadLevel("TopDownTest");
	}
	
	public void ExitGame(){
		Application.Quit();
	}
}
