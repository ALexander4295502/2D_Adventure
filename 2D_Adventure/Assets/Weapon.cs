using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public float fireRate = 0;
	public float Damage = 10;
	public LayerMask whatToHit;
	
	float timeToFire = 0;

	[SerializeField]
	private GameObject theProjectile;

	[SerializeField]
	private Transform firePoint;

	// Use this for initialization
	void Awake () {
		if (firePoint == null) {
			Debug.LogError ("No firePoint? WHAT?!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0) {
			if (Input.GetButtonDown ("Fire1")) {
				Shoot();
			}
		}
		else {
			if (Input.GetButton ("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time + 1/fireRate;
				Shoot();
			}
		}
	}
	
	void Shoot () {
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		Vector2 direction = (mousePosition - firePointPosition).normalized;
		// RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition-firePointPosition, 100, whatToHit);
		GameObject _go = Instantiate(theProjectile, firePoint.position, Quaternion.identity);
		_go.GetComponent<Rigidbody2D>().AddForce(direction*10f, ForceMode2D.Impulse);
	}
}
