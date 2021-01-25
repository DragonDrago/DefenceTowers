using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	// Use this for initialization

	[SerializeField] ParticleSystem goalParticle;

	float dwellTime = 0.5f;


	void Start () {
		Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
		var path = pathfinder.GetPath();
		StartCoroutine(FollowPath(path));
	}

	IEnumerator FollowPath(List<Waypoint> path)
	{
		print("Starting Patrol");
		foreach (Waypoint waypoint in path)
		{
			transform.position = waypoint.transform.position;
			yield return new WaitForSeconds(dwellTime);
		}
		SelfDestruct();
	}

	private void SelfDestruct()
	{
		var vfx = Instantiate(goalParticle, transform.position, Quaternion.identity);
		vfx.Play();

		Destroy(vfx.gameObject, vfx.main.duration);

		Destroy(gameObject);
	}


}
