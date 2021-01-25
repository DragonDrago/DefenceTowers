using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour {

	[SerializeField] Tower towerPrefab;

	[SerializeField] int towerLimit = 5;

    [SerializeField] Transform towerParentTransform;

	Queue<Tower> towerQueue = new Queue<Tower>();


	public void AddTower(Waypoint baseWaypoint)
	{
		int numTowers = towerQueue.Count;
        /// REMEMBER ALL THE CODE AND METHODS ARE ABOUT RING BUFER ALGORITHM's IMPLEMENTATIONS !!!!!!!!!!!!!!!!
        /// KEEP IN MIND
		if (numTowers < towerLimit)
		{
			InstantiateNewTower(baseWaypoint);
		}
		else
		{
			MoveExistingTower(baseWaypoint);
		}
	}

	private void InstantiateNewTower(Waypoint baseWaypoint)
	{
		var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = towerParentTransform.transform;
		baseWaypoint.isPlaceable = false;

        newTower.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;

        towerQueue.Enqueue(newTower);
	}

	private void MoveExistingTower(Waypoint newBaseWaypoint)
	{
		var oldTower = towerQueue.Dequeue();

        oldTower.baseWaypoint.isPlaceable = true; // free-up the block

        newBaseWaypoint.isPlaceable = false; //new point of the moved Tower;

        oldTower.baseWaypoint = newBaseWaypoint;

        oldTower.transform.position = newBaseWaypoint.transform.position;

		towerQueue.Enqueue(oldTower);
	}

   
}
