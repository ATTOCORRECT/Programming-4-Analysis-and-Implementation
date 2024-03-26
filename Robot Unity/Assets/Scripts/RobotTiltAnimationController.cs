using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTiltAnimationController : MonoBehaviour
{
    [SerializeField] float offset;

    [SerializeField] Transform robotBody;
    [SerializeField] Transform tiltTarget;
    [SerializeField] SecondOrderDynamics tiltDynamics;
    // Start is called before the first frame update
    void Awake()
    {
        tiltDynamics.SetInitialDynamicVector(tiltTarget.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        tiltDynamics.SetTargetVector(transform.position + Vector3.up * offset);
        tiltTarget.position = tiltDynamics.GetDynamicVector();

        Vector3 localTiltTarget = transform.InverseTransformPoint(tiltTarget.position);

        float tiltZ = Vector3.SignedAngle(Vector3.up, localTiltTarget.x * Vector3.right + Vector3.up * offset, Vector3.forward);
        float tiltX = Vector3.SignedAngle(Vector3.up, localTiltTarget.z * Vector3.forward + Vector3.up * offset, Vector3.right);

        robotBody.eulerAngles = new Vector3(tiltX, transform.eulerAngles.y, tiltZ);
    }
    private void FixedUpdate()
    {
        tiltDynamics.IterateDynamics();
    }
}
