using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flockingBehaviour
{
	public class CrowFlock : Flock
	{
		// Start is called before the first frame update
		void Start()
		{
			agents = new List<FlockAgent>();
			startingCount = 100; // total count of agents to be instantiated
			multiplicationNumber = 10.0f; // used for multiplying/making agents faster
			maxSpeed = 12.0f; //5f originally
			neighbourRadius = 1.5f; // radius for neighbours
			avoidanceRadiusMultiplier = 0.5f;
		
			squareMaxSpeed = maxSpeed * maxSpeed;
			squareNeighbourRadius = neighbourRadius * neighbourRadius;
			squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

			for (int i = 0; i < startingCount; i++)
			{
				FlockAgent agent = Instantiate(
					agentPrefab,
					new Vector3(Random.Range(650.0f, 730.0f),
					Random.Range(1080.0f, 1100.0f),
					Random.Range(220.0f, 240.0f)),
					Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f),
					Random.Range(0.0f, 360.0f))
					);
				agent.name = "Agent No. " + i;
				agent.AssignFlock(this);
				agents.Add(agent); 
			}
		}

		// Loop of the flocking movement
		void Update()
		{
			foreach (FlockAgent agent in agents)
			{
				List<Transform> context = GetNearbyObjects(agent); // constantly check agent's environment
				Vector3 move = behaviour.CalculateMove(agent, context, this); // prepare a move for agent
				move *= multiplicationNumber; // multiply as appropriate
				if (move.sqrMagnitude > squareMaxSpeed)
				{
					move = move.normalized * maxSpeed; // normalize speed 
				}
				agent.Move(move); // return the move for the frame
			}
		}
	}
}