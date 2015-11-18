//Steve Graphos
using UnityEngine;
using System;
using System.Collections.Generic;       //Lists are now available.
using Random = UnityEngine.Random;      //Have Random use Unity's random number generator.
namespace Completed{
	
	public class LevelController: MonoBehaviour{
		// Using Serializable allows us to embed a class with sub properties in the inspector.
		[Serializable]
		public class Count{
			public int minimum;				//Minimum value for "Count" class.
			public int maximum;				//Maximum value for "Count" class.
			
			//Assignment constructor.
			public Count (int min, int max){
				minimum = min;
				maximum = max;
			}
		}
		
		public int columns = 5;											//Number of columns.
		public int rows = 5;											//Number of rows.
		//public Count wallCount = new Count (5, 9);					//Value limits for walls generated. FOR LATER USE
		//public Count foodCount = new Count (1, 5);					//Value limits for food generated. FOR LATER USE
		//public GameObject exit;										//Prefab to spawn for exit. FOR LATER USE
		public GameObject[] floorTiles;									//Array of available floor prefabs.
		//public GameObject[] wallDebrisTiles;							//Array of available wallDebris prefabs. FOR LATER USE
		//public GameObject[] foodTiles;								//Array of available food prefabs. FOR LATER USE
		//public GameObject[] enemyTiles;								//Array of available enemy prefabs. FOR LATER USE
		public GameObject[] wallTiles;									//Array of available wall prefabs.
		
		private Transform levelHolder;									//Stores a reference to the "Level" GameObject's transform.
		private List <Vector3> gridPositions = new List <Vector3>();	//List of locations to generate tile prefabs.
		
		
		//Clears the List gridPositions. Fill List with each of the tile positions as a Vector3
		void InitialiseList (){
			gridPositions.Clear ();										//Clear the List gridPositions.
			for(int x = 1; x < columns-1; x++){							//Loop through the columns (x axis).
				for(int z = 1; z < rows-1; z++){						//In each column, loop through the rows (z axis).
					gridPositions.Add (new Vector3(x, 0f, z));			//Add a new Vector3 to List with the xy coordinates of that grid position.
				}
			}
		}

		//Generate the walls and floor of the level.
		void LevelSetup (){
			levelHolder = new GameObject ("Level").transform;			//Instantiate "Level" and set "levelHolder" to its Transform.
			for(int x = -1; x < columns + 1; x++){						//Loop through the columns (x axis), starting from -1 to fill corners with floor or wall tiles.
				for(int z = -1; z < rows + 1; z++){						//Loop through the rows (z axis), starting from -1 to place floor or wall tiles.
					//Pick randomly from the floor prefabs array and prepare to instantiate it.
					GameObject toInstantiate = floorTiles[Random.Range (0,floorTiles.Length)];
					
					//If current position is at level's edge, generate prefab from the wall prefabs array.
					if(x == -1 || x == columns || z == -1 || z == rows)
						toInstantiate = wallTiles [Random.Range (0, wallTiles.Length)];

					//Instantiate the GameObject "instance" with prefab picked in "toInstantiate" at the Vector3 of the current grid position.
					GameObject instance = Instantiate (toInstantiate, new Vector3 (x, 0f, z), Quaternion.identity) as GameObject;

					instance.transform.SetParent (levelHolder);			//To avoid Hierarchy clutter, set parent of the new GameObject "instance" to "levelHolder".
				}
			}
		}

		//RandomPosition returns a random position from the List gridPositions.
		Vector3 RandomPosition(){
			//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in the List gridPositions.
			int randomIndex = Random.Range(0, gridPositions.Count);
			
			//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from the List gridPositions.
			Vector3 randomPosition = gridPositions[randomIndex];
			gridPositions.RemoveAt(randomIndex);	//Remove the entry at randomIndex from the list so that it can't be re-used.
			return randomPosition;					//Return the randomly selected Vector3 position.
		}
			
		//Uses an array of GameObjects to choose from, with a minimum and maximum number of GameObjects to generate.
		void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum){
			//Choose a random number of objects to instantiate within the minimum and maximum.
			int objectCount = Random.Range (minimum, maximum+1);
			
			//Instantiate objects until the randomly chosen limit objectCount is reached
			for(int i = 0; i < objectCount; i++){
				//Choose a position for randomPosition by getting a random position from the list of available Vector3s stored in gridPosition
				Vector3 randomPosition = RandomPosition();
				
				//Choose a random tile from tileArray and assign it to tileChoice
				GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
				
				//Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
				Instantiate(tileChoice, randomPosition, Quaternion.identity);
			}
		}
			
		//SetupScene initializes the level and calls the previous functions to lay out the game level
		public void SetupScene (int level){
			//Creates the walls and floor.
			LevelSetup ();
			
			//Reset the list of gridpositions.
			InitialiseList ();
			
			//Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
			//LayoutObjectAtRandom (wallDebrisTiles, wallCount.minimum, wallCount.maximum);
			
			//Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
			//LayoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);
			
			//Determine number of enemies based on current level number, based on a logarithmic progression
			//int enemyCount = (int)Mathf.Log(level, 2f);
			
			//Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
			//LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount);
			
			//Instantiate the exit tile in the upper right hand corner of the game level
			//Instantiate (exit, new Vector3 (columns - 1, rows - 1, 0f), Quaternion.identity);
		}
	}
}