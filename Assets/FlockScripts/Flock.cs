using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flockingBehaviour
{
	public abstract class Flock : MonoBehaviour
	{
		public FlockAgent agentPrefab;
		public List<FlockAgent> agents;
		public FlockBehaviour behaviour;
		[Range(10, 500)]
		public int startingCount;  // total count of agents to be instantiated
		[Range(1.0f, 100.0f)]
		public float multiplicationNumber; // used for multiplying/making agents faster
		[Range(1.0f, 200.0f)]
		public float maxSpeed; //5f originally
		[Range(1.0f, 10.0f)]
		public float neighbourRadius; // radius for neighbours
		[Range(0.0f, 1.0f)] 
		public float avoidanceRadiusMultiplier;

		protected float squareMaxSpeed;
		protected float squareNeighbourRadius;
		protected float squareAvoidanceRadius;
		public float SquareAvoidanceRadius;

		public List<Transform> GetNearbyObjects(FlockAgent agent)
		{
			List<Transform> context = new List<Transform>();
			Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighbourRadius);
			foreach (Collider c in contextColliders)
			{
				if (c != agent.AgentCollider) // checking all environment colliders except agent's own collider
				{
					context.Add(c.transform);
				}
			}
			return context;
		}
		
		public List<FlockAgent> Agents
		{	
			get { return agents; }
			set { agents = value; }
		}
		
		public float GetMultiplicationNumber()
		{
			return multiplicationNumber;
		}
		
		public float GetMaxSpeed()
		{
			return maxSpeed;
		}
		
		public float GetSquareMaxSpeed()
		{
			return squareMaxSpeed;
		}
		
		public float GetSquareNeighbourRadius()
		{
			return squareMaxSpeed;
		}
		
			public float GetSquareAvoidanceRadius()
		{
			return squareAvoidanceRadius;
		}
	}
}