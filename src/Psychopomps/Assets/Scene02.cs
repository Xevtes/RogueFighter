using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System; // allows use of serializable
using System.IO; //
using System.Runtime.Serialization.Formatters.Binary;

public class Scene02 : MonoBehaviour {

	public Button button;
	bool isDragging = false;

	public void SaveGame(){

		SaveGame game = new SaveGame ();
		game.x = button.transform.position.x;
		game.y = button.transform.position.y;
		game.z = button.transform.position.z;


		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fs = File.Open (Application.persistentDataPath+ "/saveGame.Steve", FileMode.OpenOrCreate);
		bf.Serialize (fs, game);
		fs.Close ();
	}

	public void LoadGame(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fs = File.Open (Application.persistentDataPath+ "/saveGame.Steve", FileMode.Open);

		SaveGame game = (SaveGame)bf.Deserialize (fs);
		button.transform.position = new Vector3 (game.x, game.y, game.z);

	}

	public void Drag(){
		isDragging = !isDragging;

	}


	void Update(){
		if (isDragging) {
			button.transform.position = Input.mousePosition;
		}
	}
}
[Serializable]
public class SaveGame{
	public float x,y,z;


}
