using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : MonoBehaviour
{
    //Movement Vars
    Vector3 targetPosition = new Vector3(60.0f, 1.2f, 203.0f);
    float smoothTime = 2f;
    float speed = 10;
    Vector3 velocity;

    bool arrived = false;
    bool startMoving = false;

    void Update()
    {
        if (startMoving)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, speed);
            //Check if car has arrived at destination

            //Once arrived - startMoving = false;

            //Drop off 1-3 officers, then leave the area.....
        }
    }

    public void MoveToPosition()
    {
        startMoving = true;
    }
}
