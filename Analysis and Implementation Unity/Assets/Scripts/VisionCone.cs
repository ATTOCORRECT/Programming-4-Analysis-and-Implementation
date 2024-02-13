using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    // Start is called before the first frame update
    public float viewDistance = 5;
    public float viewAngle = 90;
    bool canSeeAgents = false;
    public List<GameObject> AgentsBeingSeen = new List<GameObject>();
    Collider[] hitColliders;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AgentsBeingSeen.Clear();
        canSeeAgents = false; // reset value
        Vector3 myPosition = transform.position;
        hitColliders = Physics.OverlapSphere(myPosition, viewDistance); // get all objects within view distance
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject != gameObject) // make sure not to count myself
            {
                Vector3 directionToAgent = hitColliders[i].transform.position - myPosition;
                if (Vector3.Angle(transform.forward, directionToAgent) < viewAngle / 2) // check if angle to objects is within view angle
                {
                    // object seen!
                    Debug.DrawRay(myPosition, directionToAgent);
                    canSeeAgents = true;
                    VisionManager.Instance.AgentsBeingSeen.Add(hitColliders[i].gameObject); // add to seen objects list
                    AgentsBeingSeen.Add(hitColliders[i].gameObject);
                }
            }
        }
        Debug.DrawRay(myPosition, transform.forward * viewDistance, Color.red);
        Debug.DrawRay(myPosition, Quaternion.AngleAxis(-viewAngle / 2, transform.up) * transform.forward * viewDistance, Color.red);
        Debug.DrawRay(myPosition, Quaternion.AngleAxis( viewAngle / 2, transform.up) * transform.forward * viewDistance, Color.red);
    }

    public bool getCanSeeAgents()
    {
        return canSeeAgents;
    }

    public List<GameObject> getAgentsBeingSeen()
    {
        return AgentsBeingSeen;
    }
}
