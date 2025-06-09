using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flockingBehaviour
{
	[RequireComponent(typeof(Collider))]
	public class FlockAgent : MonoBehaviour
	{
		Flock agentFlock; // the flock the agent is attached to
		public Flock AgentFlock { get { return agentFlock; } }

		Collider agentCollider;

		public Collider AgentCollider { get { return agentCollider; } } 

		// Start is called before the first frame update
		void Start()
		{
			agentCollider = GetComponent<Collider>();
		}
		
		public void AssignFlock(Flock flock)
		{
			agentFlock = flock;
		}

		public void Move(Vector3 velocity)
		{
			transform.up = velocity;
			transform.position += (Vector3) velocity * Time.deltaTime;
			
		}
	}
}