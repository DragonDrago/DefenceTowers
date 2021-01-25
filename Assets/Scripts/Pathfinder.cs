using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinder : MonoBehaviour {

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    

	Queue<Waypoint> queue = new Queue<Waypoint>();

	 [SerializeField] Waypoint startWaypoint,endWaypoint;

	bool isRunning = true;

	Waypoint searchCenter; // the current searchCenter

	List<Waypoint> path = new List<Waypoint> (); //todo private later

	Vector2Int[] directions = {
		Vector2Int.up,
		Vector2Int.right,
		Vector2Int.down,
		Vector2Int.left
	};

	public List<Waypoint> GetPath()
	{
		if (path.Count == 0)
		{
			CalculatePath();
		}

		return path;
		
	}
	 
	private void CalculatePath()
	{
		LoadBlocks();
		//ColorStartEnd();
		BreathFirstSearch();
		CreatePath();
	}


	private void CreatePath(){
		// we can use such type of methods to simplify our code.
		setAsPath(endWaypoint);
		
		Waypoint previous = endWaypoint.exploredFrom;
		while (previous != startWaypoint) 
		{
            // Dont forget that trick

            path.Add(previous);
            previous.isPlaceable = false;
            previous = previous.exploredFrom;
			
		}
		path.Add(startWaypoint);
		startWaypoint.isPlaceable = false;
		path.Reverse();

	}

	private void setAsPath(Waypoint waypoint)
	{
		path.Add(waypoint);
		waypoint.isPlaceable = false;
	}

	private void BreathFirstSearch()
	{
		queue.Enqueue (startWaypoint);
		while (queue.Count > 0  && isRunning) 
		{
			searchCenter = queue.Dequeue ();
			searchCenter.isExplored = true;
			HaltIfEndFound();
			ExploreNeighbours ();
		}
	}

	private void HaltIfEndFound()
	{
		if (searchCenter == endWaypoint) 
		{
			print ("Searching From endNode therefore stopping");// todo remove log
			isRunning=false;
		}
	}

	private void ExploreNeighbours()
	{
		if (!isRunning) { return;}
		foreach (Vector2Int direction in directions) {
			Vector2Int neighbourCoordinates = searchCenter.GetGridPos () + direction;
			if (grid.ContainsKey(neighbourCoordinates))
			{
				QueueNewNeighbours(neighbourCoordinates);
			}
			
			
		}
	}

	private void QueueNewNeighbours (Vector2Int neighbourCoordinates)
	{
		Waypoint neighbour = grid [neighbourCoordinates];
		if (neighbour.isExplored || queue.Contains(neighbour)) 
		{
			//todo nothing
		} 
		else {
			queue.Enqueue (neighbour);
			neighbour.exploredFrom = searchCenter;
		}
	}

	//private void ColorStartEnd()
	//{
	//	startWaypoint.SetTopColor (Color.green);
	//	endWaypoint.SetTopColor (Color.red);
	//}

	private void LoadBlocks()
	{
		var waypoints = FindObjectsOfType<Waypoint> ();
		foreach (Waypoint waypoint in waypoints) 
		{
			var gridPos = waypoint.GetGridPos ();
			if ( grid.ContainsKey (gridPos)) {
				Debug.LogWarning ("Skipping overlapping block" + waypoint);
			} else
			{
				grid.Add (gridPos, waypoint);
			}
		}
	}
	
	
	
}
