//Steve Graphos
using UnityEngine;
using System.Collections;
using System.Collections.Generic;       //Lists are now available.
namespace Completed{

	public class GameController : MonoBehaviour{
		
		public static GameController instance = null;			//Static instance of GameController, which allows it to be accessed by other scripts.
		public LevelController levelScript;						//Create reference LevelController, which builds the level.
		private int level = 3;									//Current level number, expressed in game as "Day 1".
		
		//Awake is always called before any Start functions.
		void Awake(){

			if (instance == null)								//Check if "instance" already exists.
				instance = this;								//if not, set "instance" to this.
			
			//If instance already exists and it's not this:
			else if (instance != this)
				
				//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
				Destroy(gameObject);    
			
			//Sets this to not be destroyed when reloading scene
			DontDestroyOnLoad(gameObject);
			
			//Get a component reference to the attached LevelController script
			levelScript = GetComponent<LevelController>();
			
			//Call the InitGame function to initialize the first level 
			InitGame();
		}
		
		//Initialize the game for each level.
		void InitGame(){
			//Call the SetupScene function of the LevelController script, pass it current level number.
			levelScript.SetupScene(level);	
		}

		//Update is called every frame.
		void Update(){
			
		}
	}
}