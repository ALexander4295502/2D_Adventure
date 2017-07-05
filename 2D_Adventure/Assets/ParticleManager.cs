using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {

	public GameObject projectile;
	public GameObject trail;
	public GameObject hit;

	public LayerMask whatToHit;

	private float beginTime;

	// Use this for initialization
	void Start () {
		beginTime = Time.time;
		projectile.SetActive(true);
		trail.SetActive(true);
		hit.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - beginTime > 3){
			Destroy(gameObject);
			gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right*10f,ForceMode2D.Impulse);
		}
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		// Debug.Log("projectile hit ! collider name : "+other.gameObject.name);

		if ( whatToHit == (whatToHit | (1 << other.gameObject.layer))) {
			projectile.SetActive(false);
			trail.SetActive(false);
			hit.SetActive(true);
			Destroy(gameObject, 1f);
			gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			gameObject.GetComponent<CircleCollider2D>().enabled = false;
			if(other.CompareTag("Item")){
				other.gameObject.GetComponent<ScController>().getHit(transform.position);
				hit.transform.Find("Blood_Particle").gameObject.SetActive(true);
			}
		}
	}




}
