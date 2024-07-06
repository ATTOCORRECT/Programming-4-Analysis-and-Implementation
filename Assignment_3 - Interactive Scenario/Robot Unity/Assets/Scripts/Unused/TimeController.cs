using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] GameObject Sun;
    Boolean DayTime = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DayTime = !DayTime;
        }
    }

    void FixedUpdate()
    {
        Debug.Log(DayTime);
        if (DayTime)
        {
            Sun.transform.rotation = Quaternion.Lerp(Sun.transform.rotation, Quaternion.Euler(100, -30, 0), 0.01f);
        }
        else
        {
            Sun.transform.rotation = Quaternion.Lerp(Sun.transform.rotation, Quaternion.Euler(5, -30, 0), 0.01f);
        }
    }
}
