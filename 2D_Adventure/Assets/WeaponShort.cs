using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShort : MonoBehaviour {

	private float fireRate = 2;

	[SerializeField] private float comboDelay = 1f;
	private float comboTimer = 0;

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
		if( comboTimer > 0 ){
             comboTimer += Time.deltaTime ;
		}
		// Do not allow combo if the user waited to long
        if( comboTimer > comboDelay ){
             comboTimer = 0 ;
		}
		if (Input.GetButton ("Fire1") && Time.time > timeToFire ) {
			timeToFire = Time.time + 1/fireRate;
			damageSettle = true;
			AttackAnim();
		}

		if(playerAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
			trailEffect.MyColor.a = 255;
			if(damageSettle == false){
				LaunchAttack(GetComponent<Collider2D>());
				damageSettle = true;
			}
		}else{
			trailEffect.MyColor.a = 0;
			damageSettle = true;
			// hitParticleEffect.SetActive(false);
		}
	}

	private void AttackAnim(){
		// Simple attack
		if( comboTimer == 0 )
		{
			playerAnim.SetTrigger("SDWAttack1");
			comboTimer += Time.deltaTime ;
		} else if( comboTimer <= comboDelay )
		{
			playerAnim.SetTrigger("SDWAttack2");
			// reset timer to come back to simple attack
			comboTimer = 0 ;
		}
	}

	private void LaunchAttack(Collider2D col){

		Vector2 _center = new Vector2(col.bounds.center.x, col.bounds.center.y);
		Vector2 _size = new Vector2(col.bounds.extents.x, col.bounds.extents.y);
		Collider2D _col = Physics2D.OverlapBox(_center, _size, col.transform.rotation.z, LayerMask.GetMask("Item"));
		
		if(_col != null && _col.CompareTag("Item")){
			HitEnemy(col, _col);
		}
		
	}

	private void HitEnemy(Collider2D col, Collider2D _col){
		Debug.Log("Hit "+_col.name);
		Instantiate(hitParticleEffect, col.bounds.center, (playerAnim.transform.position.x > _col.transform.position.x)? Quaternion.Euler(0f,-90f,0f) : Quaternion.Euler(0f,90f,0f));
		Instantiate(hitBlood, col.bounds.center, (playerAnim.transform.position.x > _col.transform.position.x)? Quaternion.Euler(0f,-90f,0f) : Quaternion.Euler(0f,90f,0f));		
	}
}
