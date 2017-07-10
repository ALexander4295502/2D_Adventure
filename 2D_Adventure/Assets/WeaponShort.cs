using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShort : MonoBehaviour {

	private float fireRate = 2;

	[SerializeField] private float comboDelay = 1f;
	private float comboTimer = 0;

	private AudioManager audioManager;

	[SerializeField] private Animator playerAnim;
	[SerializeField] private XftWeapon.XWeaponTrail trailEffect;
	[SerializeField] private GameObject hitParticleEffect;
	[SerializeField] private GameObject hitBlood;

	public float camShakeAmt = 0.05f;
	public float camShakeLength = 0.1f;
	CameraShake camShake;

	public bool damageSettle = true;

	private string bloodHittingSoundName = "blood_hitting_1";
	private string spearSlideSoundName = "Spear_slide";

	float timeToFire = 0;

	private bool attackFinished;

	// Use this for initialization
	void Start () {
		audioManager = AudioManager.instance;
		trailEffect.MyColor.a = 0;
		damageSettle = true;
		camShake = GameMaster.gm.GetComponent<CameraShake>();
		if (camShake == null)
		{
			Debug.LogError("No CameraShake script found on GM object");
		}
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
			AttackAnim();
		}

		if(playerAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
			trailEffect.MyColor.a = 255;
			if (damageSettle == false  && !attackFinished) {
				Debug.Log("damage settle!!");
				camShake.Shake(camShakeAmt, camShakeLength);
				LaunchAttack(GetComponent<Collider2D>());
				attackFinished = true;
			}
		}else{
			attackFinished = false;
			trailEffect.MyColor.a = 0;
			// hitParticleEffect.SetActive(false);
		}
	}

	private void AttackAnim(){
		RandomAudioSourceProp(GetComponent<AudioSource>());
		// Simple attack
		if(playerAnim.gameObject.GetComponent<Rigidbody2D>().velocity.y == 0){
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
		} else {
			// Debug.Log("Jump attack!!");
			// playerAnim.SetTrigger("SDWAttackJump");
			comboTimer = 0 ;
		}

	}

	private void LaunchAttack(Collider2D col){

		Vector2 _center = new Vector2(col.bounds.center.x, col.bounds.center.y);
		Vector2 _size = new Vector2(col.bounds.extents.x, col.bounds.extents.y);
		Collider2D _col = Physics2D.OverlapBox(_center, _size, col.transform.rotation.z, LayerMask.GetMask("Item"));
		Debug.Log(Time.time.ToString()+(_col == null).ToString());
		if(_col != null && _col.CompareTag("Item")){
			HitEnemy(col, _col);
		}
		
	}

	private void HitEnemy(Collider2D col, Collider2D _col){
		audioManager.PlaySound(bloodHittingSoundName);
		// Debug.Log("Hit "+_col.name);
		camShake.Shake(camShakeAmt*2, camShakeLength);
		Instantiate(hitParticleEffect, col.bounds.center, (playerAnim.transform.position.x > _col.transform.position.x)? Quaternion.Euler(0f,-90f,0f) : Quaternion.Euler(0f,90f,0f));
		Instantiate(hitBlood, col.bounds.center, (playerAnim.transform.position.x > _col.transform.position.x)? Quaternion.Euler(0f,-90f,0f) : Quaternion.Euler(0f,90f,0f));		
	}

	private void RandomAudioSourceProp(AudioSource _as){
		_as.volume = 0.7f * (1 + Random.Range(-0.1f / 2f, 0.1f / 2f));
		_as.pitch = 1f * (1 + Random.Range(-0.1f / 2f, 0.1f / 2f));
	}

}
