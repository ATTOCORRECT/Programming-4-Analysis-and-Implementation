using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions {

	public class MinesNearPlayer : ConditionTask {

		[SerializeField] public float radius;

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {

            Transform playerTransform = blackboard.GetVariableValue<Transform>("PlayerRoot"); // Grab the transform of player root

            Collider[] hitColliders = Physics.OverlapSphere(playerTransform.position, radius, 1 << LayerMask.NameToLayer("Mine")); // Check for mines near the player

            return hitColliders.Length > 0; // Return true if there are any mines
		}
	}
}