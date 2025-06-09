using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using flockingBehaviour;

namespace flockingBehaviour
{
	// Responsible for behaviour slider interaction- changing of weights across behaviours
	public class BehaviourSlider : MonoBehaviour
	{
		public CompositeBehaviour behaviour; // Access to behaviours and composite of behaviours

		public void ChangeBehaviourWeight()
		{
			if (gameObject.name.Equals("slider_alignment")) 
			{
				float weight = GameObject.Find("slider_alignment").GetComponent<Slider>().value;
				float[] weights = behaviour.Weights;
				behaviour.setWeight(0, weight); // pointing to alignment
			}

			else if (gameObject.name.Equals("slider_avoidance")) 
			{
				float weight = GameObject.Find("slider_avoidance").GetComponent<Slider>().value;
				float[] weights = behaviour.Weights;
				behaviour.setWeight(1, weight); // pointing to avoidance
			}

			else if (gameObject.name.Equals("slider_cohesion"))
			{
				float weight = GameObject.Find("slider_cohesion").GetComponent<Slider>().value;
				float[] weights = behaviour.Weights;
				behaviour.setWeight(2, weight); // pointing to cohesion
			}

		}
	}
}