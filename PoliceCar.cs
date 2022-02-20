using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : MonoBehaviour
{
    //Movement Vars
    Vector3 targetPosition;// = new Vector3(60.0f, 1.2f, 203.0f);
    float smoothTime = 1f;
    float speed = 10;
    Vector3 velocity;
    public GameObject carArrivalPoint;
    public GameObject centreOfMap;

    Vector3[] spawnPositions;
    int numberOfSpawns;

    bool arrived = false;
    bool startMoving = false;

    ObjectPooler objectPooler;

    void Start()
    {
        numberOfSpawns = transform.GetChild(1).childCount;
        spawnPositions = new Vector3[numberOfSpawns];

        objectPooler = ObjectPooler.Instance;

        //targetPosition = carArrivalPoint.transform.position;
    }

    void Update()
    {
        if (startMoving && !arrived)
        {
            //Calculate Distance to Target
            Vector3 targetOffset = transform.position - targetPosition;
            float targetSqrLen = targetOffset.sqrMagnitude;

            //Check if car has arrived at destination
            if (targetSqrLen < 2 * 2)
            {
                arrived = true;
                Debug.Log("You have reached your destination");
                Arrived();
                startMoving = false;
            } 
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, speed);
                transform.rotation = Quaternion.LookRotation(velocity);
            }
        }
        //Debug.Log("UPDATE targetPosition " + targetPosition);
    }

    public void Arrived()
    {
        arrived = true;
        Debug.Log("Arrived()");
        SpawnEnemies();
    }


    public void MoveToPosition()
    {
        //Raycast towards Center of Map to find arrival target
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Vector3 fromPosition = transform.position;
        Vector3 toPosition = centreOfMap.transform.position;
        Vector3 direction = toPosition - fromPosition;

        if (Physics.Raycast(transform.position, direction, out hit, 100))
        {
            Debug.Log("Did Hit"+ hit.transform.position);
            //Set new targetPosition to hit
            targetPosition = hit.transform.position;

            Debug.Log("targetPosition " + targetPosition);
            Debug.Log("carArrivalPoint " + carArrivalPoint.transform.position);
        }
        else
        {
            Debug.Log("DID NOT HIT");
        }
        startMoving = true;
    }

    public void SpawnEnemies()
    {
        //Get array of this cars spawn points
        for (int i = 0; i < numberOfSpawns; i++)
        {
            spawnPositions[i] = transform.GetChild(1).transform.GetChild(i).position;
        }

        //For each SpawnPoint
        int j = 0;
        foreach (Vector3 spawnPoint in spawnPositions)
        {
            Debug.Log("SpawnPoint "+ spawnPoint);
            objectPooler.SpawnFromPool("RookieCop", spawnPoint, Quaternion.identity);
            j++;
        }


        //for spawn points in array
        //Spawn cop
    }
}
