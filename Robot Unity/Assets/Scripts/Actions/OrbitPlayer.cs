using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class OrbitPlayer : ActionTask {

        [SerializeField] public float radius = 1.5f;

		protected override void OnUpdate() {

            Transform robotTransform = blackboard.GetVariableValue<Transform>("RobotRoot"); // Grab the transform of our root
            Transform playerTransform = blackboard.GetVariableValue<Transform>("PlayerRoot"); // Grab the transform of player root

			Vector3 playerToMe = robotTransform.position - playerTransform.position; // Vector from the player to this actor
            Vector3 targetLocation = playerToMe.normalized * radius + playerTransform.position; // Normalize it and scale so we always stay a fixed distance

            blackboard.SetVariableValue("TargetLocation", targetLocation); // Set the target location in the blackboard
		}
	}
}