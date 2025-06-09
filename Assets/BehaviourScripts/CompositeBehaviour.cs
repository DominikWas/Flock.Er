using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flockingBehaviour
{
	[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
	public class CompositeBehaviour : FlockBehaviour
	{
		[SerializeField] FlockBehaviour[] behaviours;
		[SerializeField] float[] weights;

		public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
		{
			// handle data mismatches
			
			if (weights.Length != behaviours.Length)
			{
				Debug.LogError("Data mismatch in " + name, this);
				return Vector2.zero;
			}
			
			// set move to zero initially
			Vector3 move = Vector3.zero;

			// iterate through behaviours
			for (int i = 0; i < behaviours.Length; i++) 
			{
				Vector3 partialMove = behaviours[i].CalculateMove(agent, context, flock) * weights[i];

				if (partialMove.sqrMagnitude > weights[i] * weights[i])
				{
					partialMove.Normalize();
					partialMove *= weights[i];
				}

				move += partialMove; // add the partial move to the move
			}

			return move; // return the final move once partial behaviour moves are combined
		}

		public float[] Weights 
		{  
			get { return weights; }  
		} 

		public void setWeight(int position, float weight) 
		{
			weights[position] = weight;
		}
	}
}