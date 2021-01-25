using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour {

   
	[SerializeField] int hitPoints = 10;
	// Use this for initialization

	[SerializeField] ParticleSystem hitParticlePrefab;

	[SerializeField] ParticleSystem deathParticlePrefab;

    [SerializeField] AudioClip enemyHitSFX;

    [SerializeField] AudioClip enemyDeathSFX;


    AudioSource myAudioSource;
	
	void Start () {
        myAudioSource = GetComponent<AudioSource>();
	}

	private void OnParticleCollision(GameObject other)
	{
       
		ProcessHit();
		if (hitPoints <= 0)
		{
			KillEnemy();
		}
	}

	private void KillEnemy()
	{
	   var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
		vfx.Play();
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);
  // in the game world the listener is the camera so we need to put position of camera in here above.
		Destroy(gameObject);
	}

	private void ProcessHit()
	{
        hitPoints = hitPoints - 1;
		hitParticlePrefab.Play();
        myAudioSource.PlayOneShot(enemyHitSFX);
	}

}
