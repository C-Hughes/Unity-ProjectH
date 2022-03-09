using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    //public Transform detectionPoint;
    public Animator animator;

    //public Vector3 detectionRange = new Vector3(6, 1, 6);
    public float attackRange = 0.7f;
    public int attackDamage = 40;

    public float attackRate = 1.5f;
    float nextAttackTime = 0f;
    bool m_EnemyInRange;

    GameObject parentObject;
    NavMeshAgent agent;
    public static Transform detectedEnemy = null;

    void Start()
    {
        //Get reference to Childs Parent
        parentObject = transform.parent.gameObject.transform.GetChild(0).gameObject;
        //Get NavMeshAgent
        agent = transform.GetComponent<NavMeshAgent>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Enemy"))
        {
            //m_EnemyInRange = true;
            detectedEnemy = other.transform;
            //Debug.Log("ENEMY ENTER");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Enemy"))
        {
            //m_EnemyInRange = true;
            detectedEnemy = other.transform;
        } 
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Enemy"))
        {
            m_EnemyInRange = false;
            returnToPosition();
            //Debug.Log("ENEMY EXIT");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If enemy is in range
        if (m_EnemyInRange)
        {
            //Calculate Distance to Enemy
            Vector3 enemyOffset = transform.position - detectedEnemy.position;
            float enemySqrLen = enemyOffset.sqrMagnitude;
            //Calculate Distance to Parent
            Vector3 parentOffset = transform.position - parentObject.transform.position;
            float parentSqrLen = parentOffset.sqrMagnitude;

            //If the enemy if Close
            if (enemySqrLen < 2 * 2)
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
            else if (parentSqrLen > 6 * 6)
            {
                //Too far, go back to parent
                returnToPosition();
                //Debug.Log("TOO FAR - RETURN TO POSITION");
            }
            else if (enemySqrLen < 4 * 4)
            {
                //Go closer to enemy
                agent.stoppingDistance = 1f;
                agent.destination = detectedEnemy.transform.position;
                //Debug.Log("POSITIONING TO ENMEMY");
            } 
            else if (transform.position != transform.GetComponent<Child>().formationPosition)
            {
                returnToPosition();
                //Debug.Log("NO ENEMIES - RETURN TO POSITION");
                m_EnemyInRange = false;
            }
        } 
    }

    void Attack()
    {
        //Play Attack Animation
        animator.SetTrigger("Attack");

        //Detect which enemies were hit
        Collider[] hitEnimies = Physics.OverlapSphere(attackPoint.position, attackRange);
        //Debug.Log("hitEnimies " + hitEnimies.Length);
        foreach (var enemy in hitEnimies)
        {
            //Deduct health
            //hitCollider.SendMessage("AddDamage");
            if (enemy.GetComponent<Collider>().CompareTag("Enemy"))
            {
                //Debug.Log("hitenemy " + enemy);
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
        //Debug.Log("returnToPosition()!!!!!");
        //If no Enemy is in range, move player back to formationPosition
        agent.stoppingDistance = 0.1f;
        //agent = transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
        //agent.destination = transform.GetComponent<Child>().formationPosition;
        transform.GetComponent<Child>().GoToFormationPosition();
        //Debug.Log("returnToPosition = " + agent.destination);
    }
}
