using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform moveTarget;
    [SerializeField] Transform Root;

    // Input Axies
    float forwardAxis;
    float turnAxis;
    float sideAxis;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getInputs(); // Grab our inputs

        // Move player        v start --- v   v add forward ----------------------- v   v add left ---------------- v
        moveTarget.position = Root.position + (moveTarget.forward * forwardAxis * 1f) + (moveTarget.right * sideAxis);

        // Rotate player         v start ----------------------- v   v add rotation ----------- v
        moveTarget.eulerAngles = (Root.eulerAngles.y * Vector3.up) + (Vector3.up * turnAxis * 20);
    }

    // Inputs used for our game
    void getInputs()
    {
        forwardAxis = Input.GetAxis("Vertical");
        sideAxis = Input.GetAxis("Horizontal");
        turnAxis = Input.GetAxis("Skew");
    }
}
