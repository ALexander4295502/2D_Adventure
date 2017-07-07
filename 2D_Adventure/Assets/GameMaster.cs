using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;

	private AudioManager audioManager;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		if (gm == null)
		{
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}
	}

	// Use this for initialization
	void Start () {
		audioManager = AudioManager.instance;
		if(audioManager == null){
			Debug.LogError("No AudioManager found in this scene.");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
