using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flockingBehaviour
{
	[CreateAssetMenu(menuName = "Flock/Behaviour/SteeredCohesion")]
	public class SteeredCohesionBehaviour : FlockBehaviour
	{
		public float agentSmoothTime = 0.48f;
		Vector3 currentVelocity;
		
		public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
		{
			// if no neighbours, return no adjustments
			if (context.Count == 0)
			{
				return Vector3.zero;
			}

			// add all points together and average
			Vector3 cohesionMove = Vector3.zero;
			foreach (Transform item in context)
			{
				cohesionMove += (Vector3)item.position;
			}
			cohesionMove /= context.Count;

			// create offset from agent position
			cohesionMove -= (Vector3) agent.transform.position;
			cohesionMove = Vector3.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime); // apply smoothing
			return cohesionMove;
		}
	}
}