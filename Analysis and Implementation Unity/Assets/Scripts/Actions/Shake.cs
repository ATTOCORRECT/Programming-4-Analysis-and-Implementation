using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class Shake : ActionTask {

		Vector3 originalPosition;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
            
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            originalPosition = agent.transform.position;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			agent.transform.position = originalPosition + Random3DUnitVector() / 50;

        }

		//Called when the task is disabled.
		protected override void OnStop() {
			agent.transform.position = originalPosition;

        }

		//Called when the task is paused.
		protected override void OnPause() {
			
		}

		Vector3 Random3DUnitVector()
		{
			float randomHeight = Random.Range(-1, 1);
			float randomAngle = Random.Range(0, 2 * Mathf.PI);
			float ring = Mathf.Sqrt(1 - (randomHeight * randomHeight));

            return new Vector3(ring * Mathf.Cos(randomAngle),
                               ring * Mathf.Sin(randomAngle), 
							   randomHeight)
							   .normalized;
        }
	}
}