using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flockingBehaviour
{
	// Replicates a murmuration behaviour in starling birds 
	// without the use of ScriptableObject architecture
	public class MurmurationBoundary : MonoBehaviour
	{
		public Vector3 center; // origin point of the area with boundaries
		public float radius = 15.0f; // radius of the area with boundaries
		public StarlingFlock flock;
		public List<FlockAgent> agents;
		public float multiplicationNum;
		public float maxSpeed;
		public float squareMaxSpeed;
		
		void Start()
		{
			center = new Vector3(690f, 1090f, 230f);
			flock = gameObject.GetComponent<StarlingFlock>();
			agents = flock.Agents; // scans the list of agents and saves all the agents
			// speed variables from the main Flock script used below to create a behaviour
			maxSpeed = flock.GetMaxSpeed();
			squareMaxSpeed = flock.GetSquareMaxSpeed();
		}
		
			// Loop of the flocking movement - behaviour every frame!
		void Update()
		{
			foreach (FlockAgent agent in agents)
			{
				List<Transform> context = flock.GetNearbyObjects(agent); // constantly check agent's environment
				Vector3 move = CalculateMove(agent, context, flock); // prepare a move for agent

				if (move.sqrMagnitude > squareMaxSpeed)
				{
					move = move.normalized * (maxSpeed / 5); // normalize speed 
				}
				agent.Move(move); // return the move for the frame
				StartCoroutine("ChangeCenterSoon");
			}
		}
		
		public Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
		{	
			Debug.Log(center.x + " " + center.y + " " + center.z);
			Vector3 centerOffset = center - (Vector3) agent.transform.position; 
			// difference between center and agent position, how far away from the center

			float boundsTolerance = centerOffset.magnitude / radius; // measures offset's magnitude (distance) over boundaries radius

			if (boundsTolerance < 0.92f)
			{
				return Vector3.zero; // within bounds, no extra move
			}
			else
			{
				return centerOffset * boundsTolerance * boundsTolerance; // out of bounds = move back in
			}
		}
		
		// Coroutine - changing the center of the murmuration
		IEnumerator ChangeCenterSoon()
		{
			yield return new WaitForSeconds(2);
	 
			center = new Vector3(Random.Range(center.x - 40f, center.x + 40f), 
								 Random.Range(center.y - 20f, center.y + 20f),
								 Random.Range(center.z - 40f, center.z + 40f));
			// Code to execute after the delay
		}
	}
}