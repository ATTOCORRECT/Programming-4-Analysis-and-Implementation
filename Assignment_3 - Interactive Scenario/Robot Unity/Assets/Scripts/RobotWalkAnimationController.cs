using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RobotWalkAnimationController : MonoBehaviour
{
    // How fast we can turn and move full throttle
    [SerializeField] float turnSpeed;
    // How fast we will reach the above speed
    [SerializeField] float turnAcceleration;
    // If we are above this angle from the target, start turning
    [SerializeField] float maxAngToTarget;

    // We are only doing a rotation around the up axis so a float works fine here
    float currentAngularVelocity;

    // Body
    [SerializeField] Transform moveTarget;
    [SerializeField] Transform lookTarget;

    //[SerializeField] Transform headBone;
    //[SerializeField] float headMaxTurnAngle;
    //[SerializeField] float headTrackingSpeed;

    // Legs
    [SerializeField] LegStepper frontLeftLegStepper;
    [SerializeField] LegStepper frontRightLegStepper;
    [SerializeField] LegStepper backLeftLegStepper;
    [SerializeField] LegStepper backRightLegStepper;

    // Second Order Dynamics
    [SerializeField] SecondOrderDynamics positionDynamics;

    private void Awake()
    {
        StartCoroutine(LegUpdateCoroutine());
        positionDynamics.SetInitialDynamicVector(moveTarget.position);
    }

    void LateUpdate()
    {
        RootMotionUpdate();
        //HeadTracking();
    }

    private void FixedUpdate()
    {
        positionDynamics.IterateDynamics();
    }

/*    void HeadTracking()
    {
        // Store the current head rotation since we will be resetting it
        Quaternion currentLocalRotation = headBone.localRotation;
        // Reset the head rotation so our world to local space transformation will use the head's zero rotation. 
        headBone.localRotation = Quaternion.identity;

        Vector3 targetWorldLookDir = lookTarget.position - headBone.position;
        Vector3 targetLocalLookDir = headBone.InverseTransformDirection(targetWorldLookDir);

        // Apply angle limit
        targetLocalLookDir = Vector3.RotateTowards(
          Vector3.forward,
          targetLocalLookDir,
          Mathf.Deg2Rad * headMaxTurnAngle, // Note we multiply by Mathf.Deg2Rad here to convert degrees to radians
          0); // We don't care about the length here, so we leave it at zero

        // Get the local rotation by using LookRotation on a local directional vector
        Quaternion targetLocalRotation = Quaternion.LookRotation(targetLocalLookDir, Vector3.up);

        // Apply smoothing
        headBone.localRotation = Quaternion.Slerp(
          currentLocalRotation,
          targetLocalRotation,
          1 - Mathf.Exp(-headTrackingSpeed * Time.deltaTime));
    }*/

    void RootMotionUpdate()
    {
        // Get the direction toward our target
        Vector3 towardTarget = lookTarget.position - transform.position;

        if (towardTarget.sqrMagnitude > 0.1f) // Dont rotate if were too close
        {
            // Vector toward target on the local XZ plane
            Vector3 towardTargetProjected = Vector3.ProjectOnPlane(towardTarget, transform.up);
            // Get the angle from the gecko's forward direction to the direction toward toward our target
            // Here we get the signed angle around the up vector so we know which direction to turn in
            float angToTarget = Vector3.SignedAngle(transform.forward, towardTargetProjected, transform.up);

            float targetAngularVelocity = 0;

            // If we are within the max angle (i.e. approximately facing the target)
            // leave the target angular velocity at zero
            if (Mathf.Abs(angToTarget) > maxAngToTarget)
            {
                // Angles in Unity are clockwise, so a positive angle here means to our right
                if (angToTarget > 0)
                {
                    targetAngularVelocity = turnSpeed;
                }
                // Invert angular speed if target is to our left
                else
                {
                    targetAngularVelocity = -turnSpeed;
                }
            }

            // Use our smoothing function to gradually change the velocity
            currentAngularVelocity = Mathf.Lerp(
              currentAngularVelocity,
              targetAngularVelocity,
              1 - Mathf.Exp(-turnAcceleration * Time.deltaTime)
            );

            // Rotate the transform around the Y axis in world space, 
            // making sure to multiply by delta time to get a consistent angular velocity
            transform.Rotate(0, Time.deltaTime * currentAngularVelocity, 0, Space.World);
        }




        positionDynamics.SetTargetVector(moveTarget.position);
        transform.position = positionDynamics.GetDynamicVector();
        
    }

    // Only allow diagonal leg pairs to step together
    IEnumerator LegUpdateCoroutine()
    {
        // Run continuously
        while (true)
        {
            do
            {
                frontLeftLegStepper.TryMove();
                backRightLegStepper.TryMove();
                // Wait a frame
                yield return null;

                // Stay in this loop while either leg is moving.
                // If only one leg in the pair is moving, the calls to TryMove() will let
                // the other leg move if it wants to.
            } while (backRightLegStepper.Moving || frontLeftLegStepper.Moving);

            // Do the same thing for the other diagonal pair
            do
            {
                frontRightLegStepper.TryMove();
                backLeftLegStepper.TryMove();

                yield return null;
            } while (backLeftLegStepper.Moving || frontRightLegStepper.Moving);
        }
    }
}
