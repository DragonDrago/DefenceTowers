using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

  //  [Range(1f,120f)] it will create dragable object for secondsBetweenSpawns and you can change it between 1f and 120f;
	[SerializeField] float secondsBetweenSpawns = 2.5f;

	[SerializeField] EnemyMovement enemyPrefab;

	[SerializeField] Transform enemyParentTransform;

	[SerializeField] Text spawnedEnemies;

    [SerializeField] AudioClip spawnedEnemySFX;

	int score;

	// Use this for initialization
	void Start () {
		StartCoroutine(RepeteadlySpawnEnemies());
		spawnedEnemies.text = score.ToString();
	}
	
	IEnumerator RepeteadlySpawnEnemies()
	{
		while (true)
		{
			AddScore();
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);//if it is repeated one time in the game
			var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
			newEnemy.transform.parent = enemyParentTransform.transform;
			yield return new WaitForSeconds(secondsBetweenSpawns);
		}
	}

	private void AddScore()
	{
		score++;
		spawnedEnemies.text = score.ToString();
	}
}
