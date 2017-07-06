using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour {
	
	public float rotationOffset = 0f;

	[SerializeField] private Transform bodyTransform;

	[SerializeField] private GameObject weaponInHand;


	// Update is called once per frame
	void Update () {

		if(weaponInHand.CompareTag("LongDistanceWeapon")){
			transform.parent.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().rotationEnable = true;
			Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			difference.Normalize();
			float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f,0f,rotZ+rotationOffset);
		} else if(weaponInHand.CompareTag("ShortDistanceWeapon")){
			transform.parent.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D>().rotationEnable = false;
		}

		

	}
}
