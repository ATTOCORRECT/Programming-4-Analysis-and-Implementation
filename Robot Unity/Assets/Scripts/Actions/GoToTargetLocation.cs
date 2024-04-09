using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

    public class GoToTargetLocation : ActionTask
    {

        protected override void OnUpdate()
        {
            Vector3 targetLocation = blackboard.GetVariableValue<Vector3>("TargetLocation"); // Grab target location from the blackboard

            Transform targetTransform = blackboard.GetVariableValue<Transform>("MoveTarget"); // Grab the transform of our move target
            Transform robotTransform = blackboard.GetVariableValue<Transform>("RobotRoot"); // Grab the transform of our move target

            Vector3 localTargetLocation = targetLocation - robotTransform.position;
            Vector3 TowardsTargetLocation = Vector3.ClampMagnitude(localTargetLocation, 1);

            targetTransform.position = robotTransform.position + TowardsTargetLocation; // Change it

            blackboard.SetVariableValue("MoveTarget", targetTransform); // Set transform
        }
    }
}