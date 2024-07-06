using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions {

	public class DistanceLessThan : ConditionTask {

		// Distance to compare
		[SerializeField] public float distance;

		// Objects were checking the distance between
        [SerializeField] public String blackboardTransform1;
        [SerializeField] public String blackboardTransform2;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit(){
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
			Vector3 position1 = blackboard.GetVariableValue<Transform>(blackboardTransform1).position;
			Vector3 position2 = blackboard.GetVariableValue<Transform>(blackboardTransform2).position;

			float distanceBetween = Vector3.Distance(position1, position2);

			return distanceBetween < distance;
        }
	}
}