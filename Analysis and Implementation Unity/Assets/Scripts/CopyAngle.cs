using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CopyAngle : MonoBehaviour
{
    [SerializeField] Transform Point1;
    [SerializeField] Transform Point2;

    float currentAngle = 0;
    float targetAngle = 0;

    float targetAngle2 = 0; // for smoothing
    float targetAngle3 = 0;
    float targetAngle4 = 0;

    // Update is called once per frame
    void Update()
    {
        targetAngle4 = targetAngle3;
        targetAngle3 = targetAngle2;
        targetAngle2 = targetAngle;

        Vector3 targetAngledVector = Point2.position - Point1.position;
        targetAngle = Vector3.SignedAngle(Vector3.back, targetAngledVector, Vector3.up);
        Debug.Log(targetAngledVector);

        currentAngle = (targetAngle + targetAngle2 + targetAngle3 + targetAngle4) / 4;

        transform.eulerAngles = new Vector3 (0, targetAngle, 0);
    }
}
