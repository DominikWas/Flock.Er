using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flockingBehaviour
{
	[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
	public class AvoidanceBehaviour : FlockBehaviour
	{
		public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
		{
			// if no neighbours, return no adjustments
			if (context.Count == 0)
			{
				return Vector3.zero;
			}

			// add all points together and average
			Vector3 avoidanceMove = Vector3.zero;
			int nAvoid = 0;
			foreach (Transform item in context)
			{
				if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
				{
					nAvoid++;
					avoidanceMove += (Vector3) (agent.transform.position - item.position); 
					// negation, moving away move
				}
			}

			if (nAvoid > 0)
			{
				avoidanceMove /= nAvoid;
			}

			return avoidanceMove;
		}
	}
}