//Steve Graphos
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameplayController : MonoBehaviour{
	
	public GameObject PauseMenuCanvas;						//Name a GameObject variable "PauseMenuCanvas".
	public bool isPaused;									//Name a bool "isPaused". 

	// Use this for initialization
	void Start(){
		isPaused = false;									//Set "isPaused" to false.
		//PauseMenuCanvas = GameObject.Find ("PauseMenuCanvas");		//Set variable "PauseMenuCanvas" to the GameObject "PauseMenuCanvas".
		//PauseMenuCanvas = gameObject.SetActive (false);				//Set GameObject "PauseMenuCanvas" to false.
	}
	
	// Update is called once per frame
	void Update(){
		CheckForPause();									//Check if pause menu is active.
	}
	
	//If the P key is pressed, toggle the pause menu UI.
	public void CheckForPause(){								
		if (isPaused){
			PauseGame(true);
		} 
		else{
			PauseGame(false);
		}
		if (Input.GetButtonDown("Pause")) {				
			TogglePause();
		}
	}
	
	//Stop the game's time while the pause menu is active. 
	public void PauseGame(bool state){
		//If the PauseMenuCanvas GameObject is inactive, time stops.
		if (state){
			PauseMenuCanvas.SetActive(true);
			Time.timeScale = 0.0f;
		}
		//If the PauseMenuCanvas game object is active, time continues.
		else{
			PauseMenuCanvas.SetActive(false);
			Time.timeScale = 1f;
		}
	}

	//Toggle "isPaused" between true or false when called.
	public void TogglePause(){
		if (isPaused){
			isPaused = false;
		}
		else{
			isPaused = true;
		}
	}

	public void MainMenu(){
		Application.LoadLevel("MainMenu");
	}
}