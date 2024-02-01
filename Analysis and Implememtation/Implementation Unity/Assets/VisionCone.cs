using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    // Start is called before the first frame update
    public float viewDistance = 5;
    public float viewAngle = 90;
    bool canSeeAgents = false;
    Collider[] hitColliders;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        canSeeAgents = false;
        Vector3 myPosition = transform.position;
        hitColliders = Physics.OverlapSphere(myPosition, viewDistance);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject != gameObject)
            {
                Vector3 directionToAgent = hitColliders[i].transform.position - myPosition;
                if (Vector3.Angle(transform.forward, directionToAgent) < viewAngle / 2)
                {

                    Debug.DrawRay(myPosition, directionToAgent);
                    canSeeAgents = true;
                    VisionManager.Instance.AgentsBeingSeen.Add(hitColliders[i].gameObject);
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
}
