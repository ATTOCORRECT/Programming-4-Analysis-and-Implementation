using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class PlayMineInteractAnim : ActionTask 
	{
		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() 
		{
            Transform robotTransform = blackboard.GetVariableValue<Transform>("RobotRoot"); // Grab the transform of our root

			Animator animator = robotTransform.GetComponentInChildren<Animator>();

			animator.Play("InteractMine", -1, 0f);

            EndAction(true);
		}
	}
}