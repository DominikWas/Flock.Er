using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flockingBehaviour
{
	[CreateAssetMenu(menuName = "Flock/Behaviour/BoundaryControl")]
	public class BoundaryControl : FlockBehaviour
	{
		public Vector3 center; // origin point of the area with boundaries
		public float radius = 15.0f; // radius of the area with boundaries
		public float boundsConstant = 0.92f;

		public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
		{
			Vector3 centerOffset = center - (Vector3) agent.transform.position; 
			// difference between center and agent position, how far away from the center

			float boundsTolerance = centerOffset.magnitude / radius; // measures offset's magnitude (distance) over boundaries radius

			if (boundsTolerance < boundsConstant)
			{
				return Vector3.zero; // within bounds, no extra move
			}
			else
			{
				return centerOffset * boundsTolerance * boundsTolerance; // out of bounds = move back in
			}
		}
	}
}