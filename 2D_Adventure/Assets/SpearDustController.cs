using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearDustController : MonoBehaviour {

	private float startTime = 0f;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time-startTime>1f){
			Destroy(gameObject);
		}
	}
}
