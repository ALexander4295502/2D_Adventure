using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShort : MonoBehaviour {

	private float fireRate = 2;

	[SerializeField] private Animator playerAnim;
	[SerializeField] private XftWeapon.XWeaponTrail trailEffect;
	[SerializeField] private GameObject hitParticleEffect;
	[SerializeField] private GameObject hitBlood;

	public bool damageSettle = true;

	float timeToFire = 0;

	// Use this for initialization
	void Start () {
		trailEffect.MyColor.a = 0;
		damageSettle = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > timeToFire && playerAnim.transform.GetComponent<Rigidbody2D>().velocity.y == 0) {
			timeToFire = Time.time + 1/fireRate;
			damageSettle = true;
			Attack();
		}
		if(playerAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
			trailEffect.MyColor.a = 255;
			if(damageSettle == false){
				LaunchAttack(GetComponent<PolygonCollider2D>());
				damageSettle = true;
			}
		}else{
			trailEffect.MyColor.a = 0;
			damageSettle = true;
			// hitParticleEffect.SetActive(false);
		}
	}

	private void Attack(){
		playerAnim.SetTrigger("SDWAttack");
	}

	// /// <summary>
	// /// Sent when another object enters a trigger collider attached to this
	// /// object (2D physics only).
	// /// </summary>
	// /// <param name="other">The other Collider2D involved in this collision.</param>
	// void OnTriggerEnter2D(Collider2D other)
	// {
	// 	if(other.CompareTag("Item")){
	// 		hitParticleEffect.SetActive(true);
	// 	}
	// }

	private void LaunchAttack(Collider2D col){

		Debug.Log("Launch attack!!!!");

		Vector2 _center = new Vector2(col.bounds.center.x, col.bounds.center.y);
		Vector2 _size = new Vector2(col.bounds.extents.x, col.bounds.extents.y);
		Collider2D _col = Physics2D.OverlapBox(_center, _size, col.transform.rotation.z, LayerMask.GetMask("Item"));

		if(_col != null && _col.CompareTag("Item")){
			Instantiate(hitParticleEffect, col.bounds.center, (playerAnim.transform.position.x > _col.transform.position.x)? Quaternion.Euler(0f,-90f,0f) : Quaternion.Euler(0f,90f,0f));
			Instantiate(hitBlood, col.bounds.center, (playerAnim.transform.position.x > _col.transform.position.x)? Quaternion.Euler(0f,-90f,0f) : Quaternion.Euler(0f,90f,0f));		
		}
		
	}
}
