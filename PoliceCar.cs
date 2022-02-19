using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : MonoBehaviour
{
    //Movement Vars
    Vector3 targetPosition = new Vector3(60.0f, 1.2f, 203.0f);
    float smoothTime = 1f;
    float speed = 10;
    Vector3 velocity;

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
    }

    void Update()
    {
        if (startMoving)
        {
            //Calculate Distance to Target
            Vector3 targetOffset = transform.position - targetPosition;
            float targetSqrLen = targetOffset.sqrMagnitude;

            //Check if car has arrived at destination
            if (targetSqrLen < 0.2 * 0.2)
            {
                arrived = true;
                Debug.Log("You have reached your destination");
                SpawnEnemies();
                startMoving = false;
            } 
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, speed);
            }
        }
    }

    public void MoveToPosition()
    {
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
