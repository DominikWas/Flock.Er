using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flockingBehaviour
{
	[CreateAssetMenu(menuName = "Flock/Behaviour/Obstacle Avoid")]
	public class ObstacleAvoidBehaviour : FlockBehaviour
	{
		public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
		{
			// if no neighbours, return no adjustments
			if (context.Count == 0)
			{
				return Vector3.zero;
			}
			
			Vector3 avoidanceMove = Vector3.zero;
			int nAvoid = 0;
			foreach (Transform item in context)
			{
				if (item.gameObject.layer == LayerMask.NameToLayer("Environment"))
				// check if any environment items found (MUST BE IN ENVIRONMENT LAYER)
				{
					nAvoid++;
					avoidanceMove += (Vector3) (agent.transform.position - item.position);
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