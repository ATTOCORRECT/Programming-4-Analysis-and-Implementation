using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class ChangeAnnoyance : ActionTask {

		public int approachValue = 10;
        private Coroutine coroutine;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            coroutine = StartCoroutine(IncrementAnnoyance());
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
            StopCoroutine(coroutine);
        }

		//Called when the task is paused.
		protected override void OnPause() {
			
		}

        IEnumerator IncrementAnnoyance()
		{
			while (true)
			{
                yield return new WaitForSeconds(1f);

				int annoyance = blackboard.GetVariableValue<int>("annoyance");

				if (approachValue != annoyance)
				{
                    annoyance += (int)Mathf.Sign(approachValue - annoyance); // aproach defined value by 1 every second
                }

                blackboard.SetVariableValue("annoyance", annoyance);
            }
		}


    }
}