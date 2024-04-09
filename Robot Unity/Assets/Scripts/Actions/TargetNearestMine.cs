using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class TargetNearestMine : ActionTask {

        [SerializeField] float radius = 5;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            Transform robotTransform = blackboard.GetVariableValue<Transform>("RobotRoot"); // Grab the transform of our root

            Collider[] hitColliders = Physics.OverlapSphere(robotTransform.position, radius, 1 << LayerMask.NameToLayer("Mine")); // Get all Mines within radius

			Vector3 targetLocation = hitColliders[0].transform.position; // set target to a mine within the sphere, (it doesnt actually need to be the nearest because this only runs if a mine is detected at all, meaning its probably the only and nearest)

            blackboard.SetVariableValue("TargetLocation", targetLocation); // Set the target location in the blackboard
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}