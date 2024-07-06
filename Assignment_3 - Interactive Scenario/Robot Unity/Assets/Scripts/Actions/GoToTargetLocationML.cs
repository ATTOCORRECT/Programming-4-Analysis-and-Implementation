using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

    public class GoToTargetLocationML : ActionTask
    {
        [SerializeField] float radius = 5;

        protected override void OnUpdate()
        {
            Vector3 targetLocation = blackboard.GetVariableValue<Vector3>("TargetLocation"); // Grab target location from the blackboard

            Transform targetTransform = blackboard.GetVariableValue<Transform>("MoveTarget"); // Grab the transform of our move target
            Transform robotTransform = blackboard.GetVariableValue<Transform>("RobotRoot"); // Grab the transform of our root

            Vector3 localTargetLocation = targetLocation - robotTransform.position; // Target location local to the root
            Vector3 towardsTargetLocation = Vector3.ClampMagnitude(localTargetLocation, 1); // Clamped ector towards the target

            Vector3 totalRepulsionForce = Vector3.zero;
            Collider[] hitColliders = Physics.OverlapSphere(robotTransform.position, radius, 1 << LayerMask.NameToLayer("Mine") | 1 << LayerMask.NameToLayer("Robot")); // Get all Mines and Robots within radius

            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject != agent.gameObject) // Make sure not to count myself
                {
                    Vector3 ObjectPosition = hitColliders[i].transform.position; // Get object position
                    Vector3 displacementToObject = ObjectPosition - robotTransform.position; // Get Displacement
                    float scale = 0.5f; // Strength of the repulsion force

                    Vector3 repulsionForce;

                    if (displacementToObject.sqrMagnitude != 0) // check to make sure its not zero to avoid an error
                    { repulsionForce = -displacementToObject.normalized * scale * 1 / displacementToObject.sqrMagnitude; }  // Calculate the force away from the object
                    else
                    { repulsionForce = Vector3.zero; }

                    totalRepulsionForce += repulsionForce; // Add to the total force from all mines
                }
            }

            towardsTargetLocation = Vector3.ClampMagnitude(towardsTargetLocation + totalRepulsionForce, 1); // Add mine force to the steering Vector making sure to clamp the magnitude again

            targetTransform.position = robotTransform.position + towardsTargetLocation; // Update target position

            blackboard.SetVariableValue("MoveTarget", targetTransform); // Set transform
        }
    }
}