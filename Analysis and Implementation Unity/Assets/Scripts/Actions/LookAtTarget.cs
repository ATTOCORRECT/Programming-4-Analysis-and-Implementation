using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace NodeCanvas.Tasks.Actions
{

    public class LookAtTarget : ActionTask
    {

        GameObject target;
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
            target = blackboard.GetVariableValue<GameObject>("target");
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            agent.transform.eulerAngles = new Vector3(agent.transform.eulerAngles.x, 
                                                      Mathf.LerpAngle(agent.transform.eulerAngles.y, angleDirectionToTarget(), 0.05f), 
                                                      agent.transform.eulerAngles.z);
        }

        //Called when the task is disabled.
        protected override void OnStop()
        {

        }

        //Called when the task is paused.
        protected override void OnPause()
        {

        }

        float angleDirectionToTarget()
        {
            Vector3 vectorToTarget = target.transform.position - agent.transform.position;
            return Vector3.SignedAngle(Vector3.forward, vectorToTarget, Vector3.up);
        }
    }
}