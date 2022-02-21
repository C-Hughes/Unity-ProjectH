using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceCar : MonoBehaviour
{
    //Movement Vars
    Vector3 targetPosition;// = new Vector3(60.0f, 1.2f, 203.0f);
    //float smoothTime = 1f;
    //float speed = 10;
    //Vector3 velocity;
    //public GameObject carArrivalPoint;
    public GameObject centreOfMap;

    Vector3[] spawnPositions;
    int numberOfSpawns;

    bool arrived = false;
    bool startMoving = false;

    ObjectPooler objectPooler;
    NavMeshAgent agent;

    void Start()
    {
        //Each vehicle has a different number of spawn points for enemies
        numberOfSpawns = transform.GetChild(1).childCount;
        spawnPositions = new Vector3[numberOfSpawns];

        objectPooler = ObjectPooler.Instance;


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
            /*
            else
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, speed);
                transform.rotation = Quaternion.LookRotation(velocity);
            }
            */
        }
    }

    public void Arrived()
    {
        arrived = true;
        Debug.Log("Arrived()");
        StartCoroutine(SpawnEnemies());
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
            NavMeshHit closestHit;
            if (NavMesh.SamplePosition(hit.transform.position, out closestHit, 500f, NavMesh.AllAreas))
            {
                targetPosition = closestHit.position;
            } 
            else
            {
                targetPosition = hit.transform.position;
            }
        }
        startMoving = true;

        //Move Agent to Center of Square
        agent = transform.GetComponent<NavMeshAgent>();
        agent.destination = targetPosition;
    }

    IEnumerator SpawnEnemies()
    {
        //Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        //Get array of this cars current spawn points
        for (int i = 0; i < numberOfSpawns; i++)
        {
            spawnPositions[i] = transform.GetChild(1).transform.GetChild(i).position;
        }

        //For each SpawnPoint
        foreach (Vector3 spawnPoint in spawnPositions)
        {
            Debug.Log("SpawnPoint "+ spawnPoint);
            objectPooler.SpawnFromPool("RookieCop", spawnPoint, Quaternion.identity);
        }


        //for spawn points in array
        //Spawn cop
    }
}
