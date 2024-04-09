using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{

    public class RemoveNearestMine : ActionTask
    {

        [SerializeField] float radius = 5;

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            Transform rootTransform = blackboard.GetVariableValue<Transform>("RobotRoot"); // Grab the transform of our root

            Collider[] hitColliders = Physics.OverlapSphere(rootTransform.position, radius, 1 << LayerMask.NameToLayer("Mine")); // Get all Mines within radius

            GameObject nearestMine = hitColliders[0].gameObject;
            for (int i = 1; i < hitColliders.Length; i++) // sort through for the nearest mine
            {
                if (Vector3.Distance(rootTransform.position, nearestMine.transform.position) > Vector3.Distance(rootTransform.position, hitColliders[i].transform.position))
                {
                    nearestMine = hitColliders[i].gameObject;
                }
            }

            GameObject.Destroy(nearestMine);
            EndAction(true);
        }
    }
}