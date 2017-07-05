using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorToPrefab {
	public Color32 color;
	public GameObject[] prefabs;
}

public class LevelLoader : MonoBehaviour {

	public Texture2D LevelMap;

	public ColorToPrefab[] colorToPrefab;

	

	// Use this for initialization
	void Start () {
		LoadMap();
	}

	void LoadMap(){
		EmptyMap();
		Color32[] allPixels = LevelMap.GetPixels32();
		int width = LevelMap.width;
		int height = LevelMap.height;

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				SpawnTileAt(allPixels[ x + (y*width) ], x, y);
			}
		}
	}

	void EmptyMap() {
		while(transform.childCount > 0){
			Transform c = transform.GetChild(0);
			c.SetParent(null);
			Destroy(c.gameObject);
		}
	}

	void SpawnTileAt(Color32 c, int x, int y){

		if(c.a <= 0){
			return;
		}

		foreach(ColorToPrefab ctp in colorToPrefab){
			if(ctp.color.Equals(c)){
				GameObject go = Instantiate(ctp.prefabs[Random.Range(0, ctp.prefabs.Length - 1)], new Vector3(x-LevelMap.width/2, y-LevelMap.height/2.5f, 0), Quaternion.identity);
				go.transform.parent = transform;
				go.layer = 11; // platform layer
				return;
			}
		}

		Debug.LogError("No color to prefab found for: "+ c.ToString());
		return;
	}
	
}
