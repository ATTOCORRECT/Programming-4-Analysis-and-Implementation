using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class GoToSpecificLocation : ActionTask {

		public string locationName;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			Transform targetLocation = GameObject.Find(locationName).transform;
			agent.transform.position = targetLocation.position; // Set position for GeckoAnimationController to follow
            EndAction(true);
		}
	}
}