using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    //public Transform detectionPoint;
    public Animator animator;

    public Vector3 detectionRange = new Vector3(6, 1, 6);
    public float attackRange = 0.7f;
    public int attackDamage = 40;

    public float attackRate = 1.5f;
    float nextAttackTime = 0f;
    bool m_EnemyInRange;

    UnityEngine.AI.NavMeshAgent agent;
    public static Transform detectedEnemy = null;


    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Enemy")
        {
            m_EnemyInRange = true;
            detectedEnemy = other.transform;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Enemy")
        {
            m_EnemyInRange = true;
            detectedEnemy = other.transform;
        } 
        else
        {
            //returnToPosition();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Enemy")
        {
            m_EnemyInRange = false;
            returnToPosition();
            Debug.Log("ENEMY EXIT");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If enemy is in range
        if (m_EnemyInRange)
        {
            //Debug.Log("Enemy in Detection Range");
            //Calculate Distance
            Vector3 offset = transform.position - detectedEnemy.position;
            float sqrLen = offset.sqrMagnitude;

            //If the enemy if Close
            if (sqrLen < 2 * 2)
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
            else if (sqrLen < 4 * 4)
            {
                //Go closer to enemy
                //Debug.Log("Moving To Enemy Location");
                agent = transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
                agent.stoppingDistance = 1f;
                agent.destination = detectedEnemy.transform.position;
                Debug.Log("POSITIONING TO ENMEMY");
            } 
            else if (transform.position != transform.GetComponent<Child>().formationPosition)
            {
                returnToPosition();
            }
        } 
        else
        {
            //If no Enemy is in range, move player back to formationPosition
            //returnToPosition();
            //Debug.Log("NO ENEMY");
        }
    }

    void Attack()
    {
        //Play Attack Animation
        //animator.SetTrigger("Attack");

        //Detect which enemies were hit
        Collider[] hitEnimies = Physics.OverlapSphere(attackPoint.position, attackRange);
        Debug.Log("hitEnimies " + hitEnimies.Length);
        foreach (var enemy in hitEnimies)
        {
            //Deduct health
            //hitCollider.SendMessage("AddDamage");
            if (enemy.GetComponent<Collider>().tag == "Enemy")
            {
                Debug.Log("hitenemy " + enemy);
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
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

    void returnToPosition()
    {
        Debug.Log("returnToPosition 2222222222");
        //If no Enemy is in range, move player back to formationPosition
        agent.stoppingDistance = 0.1f;
        //agent = transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
        //agent.destination = transform.GetComponent<Child>().formationPosition;
        transform.GetComponent<Child>().GoToFormationPosition();
        Debug.Log("returnToPosition = " + agent.destination);
    }
}
