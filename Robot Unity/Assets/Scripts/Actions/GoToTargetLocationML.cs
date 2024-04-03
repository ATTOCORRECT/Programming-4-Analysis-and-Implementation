using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

    public class GoToTargetLocationML : ActionTask
    {
        [SerializeField] float radius = 5;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {

        }

        protected override void OnUpdate()
        {
            Vector3 targetLocation = blackboard.GetVariableValue<Vector3>("TargetLocation"); // Grab target location from the blackboard

            Transform targetTransform = blackboard.GetVariableValue<Transform>("MoveTarget"); // Grab the transform of our move target
            Transform robotTransform = blackboard.GetVariableValue<Transform>("RobotRoot"); // Grab the transform of our root

            Vector3 localTargetLocation = targetLocation - robotTransform.position; // Target location local to the root
            Vector3 towardsTargetLocation = Vector3.ClampMagnitude(localTargetLocation, 1); // Clamped ector towards the target

            Vector3 totalMineForce = Vector3.zero;
            Collider[] hitColliders = Physics.OverlapSphere(robotTransform.position, radius, 1 << LayerMask.NameToLayer("Mine")); // Get all Mines within radius

            for (int i = 0; i < hitColliders.Length; i++)
            {
                Vector3 minePosition = hitColliders[i].transform.position; // Get mine position
                Vector3 displacementToMine = minePosition - robotTransform.position; // Get Displacement
                float scale = 0.5f; // Strength of the repulsion force

                Vector3 mineForce;

                if (displacementToMine.sqrMagnitude != 0) // check to make sure its not zero to avoid an error
                { mineForce = -displacementToMine.normalized * scale * 1 / displacementToMine.sqrMagnitude; }  // Calculate the force away from the mine
                else
                { mineForce = Vector3.zero; }

            
               

                totalMineForce += mineForce; // Add to the total force from all mines
            }

            towardsTargetLocation = Vector3.ClampMagnitude(towardsTargetLocation + totalMineForce, 1); // Add mine force to the steering Vector making sure to clamp the magnitude again

            targetTransform.position = robotTransform.position + towardsTargetLocation; // Update target position

            blackboard.SetVariableValue("MoveTarget", targetTransform); // Set transform
        }
    }
}