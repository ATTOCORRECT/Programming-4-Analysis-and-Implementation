using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeckoAnimationController : MonoBehaviour
{
    // The target we are going to track
    [SerializeField] Transform lookObject;
    // A reference to the gecko's neck
    [SerializeField] Transform headBone;

    // We will put all our animation code in LateUpdate.
    // This allows other systems to update the environment first, 
    // allowing the animation system to adapt to it before the frame is drawn.
    void LateUpdate()
    {
        Vector3 towardObjectFromHead = lookObject.position - headBone.position * 1;
        headBone.rotation = Quaternion.LookRotation(-towardObjectFromHead, transform.up);


    }
}
