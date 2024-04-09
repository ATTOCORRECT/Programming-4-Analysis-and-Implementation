using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

    public class GoToTargetLocationMD : ActionTask
    {
        [SerializeField] float radius = 5;

        protected override void OnUpdate()
        {
            Vector3 targetLocation = blackboard.GetVariableValue<Vector3>("TargetLocation"); // Grab target location from the blackboard

            Transform targetTransform = blackboard.GetVariableValue<Transform>("MoveTarget"); // Grab the transform of our move target
            Transform robotTransform = blackboard.GetVariableValue<Transform>("RobotRoot"); // Grab the transform of our root

            Vector3 localTargetLocation = targetLocation - robotTransform.position; // Target location local to the root
            Vector3 towardsTargetLocation = Vector3.ClampMagnitude(localTargetLocation, 1); // Clamped ector towards the target

            Collider[] hitColliders = Physics.OverlapSphere(robotTransform.position, radius, 1 << LayerMask.NameToLayer("PlayerRobot")); // Get all Mines within radius
            
            Vector3 playerForce = Vector3.zero;

            if (hitColliders.Length > 0 )
            {
                Vector3 playerPosition = hitColliders[0].transform.position; // Get player position (there should only be one)
                Vector3 displacementToPlayer = playerPosition - robotTransform.position; // Get Displacement
                float scale = 1f; // Strength of the repulsion force

                if (displacementToPlayer.sqrMagnitude != 0) // check to make sure its not zero to avoid an error
                { playerForce = -displacementToPlayer.normalized * scale * 1 / displacementToPlayer.sqrMagnitude; }  // Calculate the force away from the player
                else
                { playerForce = Vector3.zero; }
            }

            towardsTargetLocation = Vector3.ClampMagnitude(towardsTargetLocation + playerForce, 1); // Add mine force to the steering Vector making sure to clamp the magnitude again

            targetTransform.position = robotTransform.position + towardsTargetLocation; // Update target position

            blackboard.SetVariableValue("MoveTarget", targetTransform); // Set transform
        }
    }
}