using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.UIElements;

namespace NodeCanvas.Tasks.Actions {

    public class GoToTargetLocation : ActionTask
    {

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
            Transform robotTransform = blackboard.GetVariableValue<Transform>("RobotRoot"); // Grab the transform of our move target

            Vector3 localTargetLocation = targetLocation - robotTransform.position;
            Vector3 TowardsTargetLocation = Vector3.ClampMagnitude(localTargetLocation, 1);

            targetTransform.position = robotTransform.position + TowardsTargetLocation; // Change it

            blackboard.SetVariableValue("MoveTarget", targetTransform); // Set transform
        }
    }
}