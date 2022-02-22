using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCombat : MonoBehaviour
{
    public Transform attackPoint;
    //public Transform detectionPoint;
    //public Animator animator;

    public Vector3 detectionRange = new Vector3(6, 1, 6);
    public float attackRange = 0.7f;
    public int attackDamage = 40;

    public float attackRate = 1.5f;
    float nextAttackTime = 0f;
    bool m_PlayerInRange = false;

    //GameObject parentObject;
    NavMeshAgent agent;
    public static Transform detectedPlayer = null;

    
    void Start()
    {
        //Get reference to Childs Parent
        //parentObject = transform.parent.gameObject.transform.GetChild(0).gameObject;
        //Get NavMeshAgent
        agent = transform.GetComponent<NavMeshAgent>();

        Debug.Log("PLAYER STARTED");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Child"))
        {
            m_PlayerInRange = true;
            detectedPlayer = other.transform;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Child"))
        {
            m_PlayerInRange = true;
            detectedPlayer = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Child"))
        {
            m_PlayerInRange = false;
            //Go back to targeting player vehicles
            Debug.Log("PLAYER EXIT");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If enemy is in range
        if (m_PlayerInRange)
        {
            Debug.Log("POLAYER");
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
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
            else if (playerSqrLen < 4 * 4)
            {
                //Go closer to enemy
                agent.stoppingDistance = 1f;
                agent.destination = detectedPlayer.transform.position;
                Debug.Log("POSITIONING TO PLAYER"); 
            }
            else if (transform.position != transform.GetComponent<Child>().formationPosition)
            {
                //GO BACK TO TARGETING PLAYER VEHICLE
                Debug.Log("NO PLAYERS - RETURN TO TARGETING PLAYER VEHICLE");
                m_PlayerInRange = false;
            }
        }
    }

    void Attack()
    {
        Debug.Log("ATTACK");
        //Play Attack Animation
        //animator.SetTrigger("Attack");

        //Detect which enemies were hit
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange);
        Debug.Log("hitPlayers " + hitPlayers.Length);
        foreach (var player in hitPlayers)
        {
            //Deduct health
            //hitCollider.SendMessage("AddDamage");
            if (player.GetComponent<Collider>().CompareTag("Enemy"))
            {
                Debug.Log("hitPlayer " + player);
                player.GetComponent<Child>().TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        //Gizmos.DrawWireCube(detectionPoint.position, detectionRange);
    }
    
}
