using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScController : MonoBehaviour {

	Animator animator;
	private float getAttackedTime = 0f;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - getAttackedTime > 0.4f){
			animator.SetBool("leftRotation", false);
			animator.SetBool("rightRotation", false);
		}
	}


	public void getHit(Vector3 hitPosition){

		Debug.Log("Get hit at "+hitPosition.ToString()+ " i am at: "+transform.position.ToString());
		getAttackedTime = Time.time;
		if(transform.position.x < hitPosition.x){
			animator.SetBool("leftRotation", true);
			animator.SetBool("rightRotation", false);
		} else {
			animator.SetBool("leftRotation", false);
			animator.SetBool("rightRotation", true);
		}
		
	}
}
