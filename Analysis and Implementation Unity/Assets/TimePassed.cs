using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions {

	public class TimePassed : ConditionTask {

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		public float Seconds = 10;
		bool timerComplete = false;
        private IEnumerator coroutine;

        protected override string OnInit(){
            return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
            coroutine = WaitAndFinish(Seconds);
            StartCoroutine(coroutine);
        }

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
			return timerComplete;
		}

		IEnumerator WaitAndFinish(float waitTime)
		{
			yield return new WaitForSeconds(waitTime);
			timerComplete = true;
        }
	}
}