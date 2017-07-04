using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

	private float beginTime;

	// Use this for initialization
	void Start () {
		beginTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - beginTime > 3){
			Destroy(gameObject);
		}
	}

}
