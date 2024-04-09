using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions 
{

	public class LookAtTarget : ActionTask 
	{

		//Called once per frame while the action is active.
		protected override void OnUpdate() 
		{
            Vector3 targetLocation = blackboard.GetVariableValue<Vector3>("TargetLocation"); // Grab target location from the blackboard

            Transform targetTransform = blackboard.GetVariableValue<Transform>("MoveTarget"); // Grab the transform of our move target
            Transform robotTransform = blackboard.GetVariableValue<Transform>("RobotRoot"); // Grab the transform of our root

            Transform lookTransform = targetTransform.Find("LookTarget");

            lookTransform.position = 10 * (targetLocation - robotTransform.position) + targetLocation;
        }
	}
}