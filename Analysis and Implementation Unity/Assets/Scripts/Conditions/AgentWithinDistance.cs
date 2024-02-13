using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions {

	public class AgentWithinDistance : ConditionTask {

		public float radius = 1;
		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
            Vector3 myPosition = agent.transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(myPosition, radius); // get all objects within radius
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject != agent.gameObject) // make sure not to count myself
                {
                    return true;
                }
            }
            return false;
		}
	}
}