using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class ChooseRandomTargetLocation : ActionTask {

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			float range = 10;
			Vector3 randomLocation = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range)); // get a random location (10x10 centered at 0,0)

			blackboard.SetVariableValue("TargetLocation", randomLocation); // set the target location in the blackboard
			EndAction(true);
		}
	}
}