using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : MonoBehaviour
{
    float timepassed;
    public float speed = -11f;
    float timepasseduntilnow = 30f;

    void Update()
    {
        timepassed += Time.deltaTime;
        if (timepasseduntilnow < timepassed)
        {
            timepasseduntilnow += 20f;
            speed -= 2f;
        }
    }
}
