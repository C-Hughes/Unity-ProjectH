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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startMoving)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, speed);
        }
    }

    public void MoveToPosition()
    {
        startMoving = true;
    }
}
