using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flockingBehaviour 
{
	[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
	public class AlignmentBehaviour : FlockBehaviour
	{
		public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
		{
			// if no neighbours, return current direction
			if (context.Count == 0)
			{
				return agent.transform.up;
			}

			// add all points together and average
			Vector3 alignmentMove = Vector3.zero;
			foreach (Transform item in context)
			{
				alignmentMove += (Vector3)item.transform.up; // sums up the rotations
			}
			alignmentMove /= context.Count; // divide alignment by number of neighbours, normalized value returned through .up

			return alignmentMove;
		}
	}
}
