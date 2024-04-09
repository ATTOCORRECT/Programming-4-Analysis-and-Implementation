using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class TargetNearestMine : ActionTask {

        [SerializeField] float radius = 5;

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            Transform playerTransform = blackboard.GetVariableValue<Transform>("PlayerRoot"); // Grab the transform of the player root

            Collider[] hitColliders = Physics.OverlapSphere(playerTransform.position, radius, 1 << LayerMask.NameToLayer("Mine")); // Get all Mines within radius

            Vector3 nearestMine = hitColliders[0].transform.position;
            for (int i = 1; i < hitColliders.Length; i++) // sort through for the nearest mine
			{
				if (Vector3.Distance(playerTransform.position, nearestMine) > Vector3.Distance(playerTransform.position, hitColliders[i].transform.position))
				{
                    nearestMine = hitColliders[i].transform.position;
                }
			}

			Vector3 targetLocation = nearestMine; // set target
            blackboard.SetVariableValue("TargetLocation", targetLocation); // Set the target location in the blackboard

			EndAction(true);
		}
	}
}
