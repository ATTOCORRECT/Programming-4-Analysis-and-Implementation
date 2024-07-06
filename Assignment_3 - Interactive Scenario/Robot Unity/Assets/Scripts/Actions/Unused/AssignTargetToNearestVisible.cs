using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class AssignTargetToNearestVisible : ActionTask {
        
        
        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            List<GameObject> AgentsBeingSeen = agent.GetComponent<VisionCone>().getAgentsBeingSeen(); // get visible agents

            //sort list for nearest
            GameObject Nearest = AgentsBeingSeen[0];
			for (int i = 1; i < AgentsBeingSeen.Count; i++)
			{
				float distance1 = (Nearest.transform.position - agent.transform.position).sqrMagnitude;
                float distance2 = (AgentsBeingSeen[i].transform.position - agent.transform.position).sqrMagnitude;

                if (distance1 > distance2)
				{
					Nearest = AgentsBeingSeen[i];
                }
            }

			//set nearest
			Debug.Log(Nearest);
            blackboard.SetVariableValue("target", Nearest);
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}