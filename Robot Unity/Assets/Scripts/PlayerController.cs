using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform moveTarget;
    [SerializeField] Transform Root;
    // Start is called before the first frame update
    float forwardAxis;
    float turnAxis;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getInputs();

        moveTarget.position += moveTarget.forward * forwardAxis * 0.01f;
        moveTarget.eulerAngles += Vector3.up * turnAxis * 1;
    }

    // Inputs used for our game
    void getInputs()
    {
        forwardAxis = Input.GetAxis("Vertical");
        turnAxis = Input.GetAxis("Horizontal");
    }
}
