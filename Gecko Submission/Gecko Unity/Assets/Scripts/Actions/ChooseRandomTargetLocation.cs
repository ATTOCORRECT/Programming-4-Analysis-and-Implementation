using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class ChooseRandomTargetLocation : ActionTask {
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			Vector3 randomLocation = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)); // get a random location (10x10 centered at 0,0)

			// if i wanted to check for validity and/or obstacle avoidance id probably do it here

			blackboard.SetVariableValue("targetLocation", randomLocation); // set the target location in the blackboard
			EndAction(true);
		}
	}
}