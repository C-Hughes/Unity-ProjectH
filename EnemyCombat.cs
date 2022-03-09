using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{
    public Transform attackPoint;
    //public Transform detectionPoint;
    public Animator animator;

    //public Vector3 detectionRange = new Vector3(6, 1, 6);
    public float attackRange = 0.7f;
    public int attackDamage = 40;

    public float attackRate = 1.5f;
    float nextAttackTime = 0f;

    bool m_PlayerInRange = false;
    bool m_VehicleInRange = false;

    public static Transform detectedPlayer = null;
    public static Transform detectedVehicle = null;

    //GameObject parentObject;
    NavMeshAgent agent;
    
    void Start()
    {
        //Get reference to Childs Parent
        //parentObject = transform.parent.gameObject.transform.GetChild(0).gameObject;
        //Get NavMeshAgent
        agent = transform.GetComponent<NavMeshAgent>();

        //Debug.Log("ENEMY STARTED");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Child"))
        {
            m_PlayerInRange = true;
            detectedPlayer = other.transform;
            //Debug.Log("PLAYER ENTER");
        }
        if (other.GetComponent<Collider>().CompareTag("Vehicle"))
        {
            //Check if Vehicle is destroyed...
            m_VehicleInRange = true;
            detectedVehicle = other.transform;
            //Debug.Log("VEHICLE ENTER");
        }
        //Debug.Log("ENTER "+ other.tag);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Child"))
        {
            m_PlayerInRange = true;
            detectedPlayer = other.transform;
        }
        if (other.GetComponent<Collider>().CompareTag("Vehicle"))
        {
            m_VehicleInRange = true;
            detectedVehicle = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Child"))
        {
            m_PlayerInRange = false;
            //Go back to targeting player vehicles
            //Debug.Log("PLAYER EXIT");
        }
        if (other.GetComponent<Collider>().CompareTag("Vehicle"))
        {
            m_VehicleInRange = false;
            //Debug.Log("VEHICLE ENTER");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If enemy is in range
        if (m_PlayerInRange)
        {
            //Debug.Log("POLAYER");
            //Calculate Distance to Player
            Vector3 playerOffset = transform.position - detectedPlayer.position;
            float playerSqrLen = playerOffset.sqrMagnitude;

            //If the enemy if Close
            if (playerSqrLen < 2 * 2)
            {
                //Debug.Log("Enemy in Attack Range");
                //Attack if not in cooldown
                if (Time.time >= nextAttackTime)
                {
                    //Debug.Log("Attacking...");
                    Attack();
                    //Debug.Log("ATTACK PLAYER!!!");
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
            else if (playerSqrLen < 4 * 4)
            {
                //Go closer to enemy
                agent.stoppingDistance = 1f;
                agent.destination = detectedPlayer.transform.position;
                //Debug.Log("POSITIONING TO PLAYER"); 
            }
            else
            {
                //GO BACK TO TARGETING PLAYER VEHICLE
                //Debug.Log("NO PLAYERS - RETURN TO TARGETING PLAYER VEHICLE");
                m_PlayerInRange = false;
            }
        } 
        //If Vehicle is in Range
        else if (m_VehicleInRange)
        {
            //Calculate Distance to Player
            Vector3 vehicleOffset = transform.position - detectedVehicle.position;
            float vehicleSqrLen = vehicleOffset.sqrMagnitude;

            if (vehicleSqrLen < 4 * 4)
            {
                //Close - Attack Vehicle
                AttackVehicle();
                
            }
            else
            {
                //GO BACK TO TARGETING PLAYER VEHICLE
                //Debug.Log("NO VEHICLES - TARGET NEXT VEHICLE");
                m_VehicleInRange = false;

            }
        } 
        else
        {
            //If No vehicle or player in range
            //Find Closest Vehicle
            //Find Closest Player
        }
    }

    void Attack()
    {
        //Debug.Log("ATTACK PLAYER()");
        //Play Attack Animation
        animator.SetTrigger("Attack");

        //Detect which enemies were hit
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange);
        //Debug.Log("hitPlayers " + hitPlayers.Length);
        foreach (var player in hitPlayers)
        {
            //Deduct health
            //hitCollider.SendMessage("AddDamage");
            if (player.GetComponent<Collider>().CompareTag("Child"))
            {
                //Debug.Log("hitPlayer " + player);
                player.GetComponent<Child>().TakeDamage(attackDamage);
            }
        }
    }

    void AttackVehicle()
    {
        //Debug.Log("ATTACK PLAYER()");
        //Play Attack Animation
        animator.SetTrigger("Throw");
        Debug.Log("ATTACK VEHICLES - THROW");
        /*
        //Detect which enemies were hit
        Collider[] hitVehicles = Physics.OverlapSphere(attackPoint.position, attackRange);
        //Debug.Log("hitPlayers " + hitPlayers.Length);
        foreach (var player in hitPlayers)
        {
            //Deduct health
            //hitCollider.SendMessage("AddDamage");
            if (player.GetComponent<Collider>().CompareTag("Vehicle"))
            {
                //Debug.Log("hitPlayer " + player);
                //vehicle.GetComponent<Child>().TakeDamage(attackDamage);
            }
        }*/
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        //Gizmos.DrawWireCube(detectionPoint.position, detectionRange);
    }
    
}
