using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IPooledObject
{
    public int maxHealth = 100;
    public int speed = 8;
    public int xpGain = 10;
    int currentHealth;

    Vector3 currentVehicleTarget;
    float currentVehicleTargetDistance = 100f;

    //NavMesh Agent
    NavMeshAgent agent;

    //Friendly Vehicles
    static GameObject friendlyVehicles;
    int numberOfFriendlyVehicles;
    Vector3[] friendlyVehiclesSpawnPositions;

    void Start()
    {
        
    }

    public void OnObjectSpawn()
    {
        //Initialise
        friendlyVehicles = GameObject.Find("/TestScene/FriendlyVehicles");
        //Each vehicle has a different number of spawn points for enemies
        numberOfFriendlyVehicles = friendlyVehicles.transform.childCount;
        friendlyVehiclesSpawnPositions = new Vector3[numberOfFriendlyVehicles];
        currentHealth = maxHealth;


        //On Spawn, Find closest Player friendly vehicle...
        FindClosestVehicle();
        //Go towards closest vehicle...
        agent = transform.GetComponent<NavMeshAgent>();
        agent.destination = currentVehicleTarget;
        Debug.Log("currentVehicleTarget " + currentVehicleTarget);

        //If player is within enemies range, it should attack...
    }

    void Update()
    {
        
    }

    void FindClosestVehicle()
    {
        //Get array of this cars current spawn points
        for (int i = 0; i < numberOfFriendlyVehicles; i++)
        {
            friendlyVehiclesSpawnPositions[i] = friendlyVehicles.transform.GetChild(i).position;
        }

        //For each SpawnPoint
        foreach (Vector3 friendlyVehiclePosition in friendlyVehiclesSpawnPositions)
        {
            //Calculate distance
            Vector3 targetOffset = transform.position - friendlyVehiclePosition;
            float targetSqrLen = targetOffset.sqrMagnitude;

            //If distance is closer than CURRENTCLOSEST, CURRENTCLOSEST = friendlyVehiclePosition-
            if (targetSqrLen < currentVehicleTargetDistance)
            {
                currentVehicleTarget = friendlyVehiclePosition;
                currentVehicleTargetDistance = targetSqrLen;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //Debug.Log("I'm hit!");
        //Hurt Animation
        //Knockback effect...

        if (currentHealth <= 0)
        {
            //this.GetComponent<ClickToMove>().StopNavigation();
            Die();
        }
    }

    void Die()
    {
        //Play Die Animation...
        //animator.SetBool("IsDead", true);
        //Disable The Enemy
        transform.position = new Vector3(1000, 1000, 1000);
        gameObject.tag = "Dead";
        GetComponent<CapsuleCollider>().enabled = false;

        WaveManager.EnemiesAlive--;
        //Disable
        gameObject.SetActive(false);
        this.enabled = false;
    }
}
