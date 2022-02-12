using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    //public Transform detectionPoint;
    public Animator animator;

    public Vector3 detectionRange = new Vector3(6, 1, 6);
    public float attackRange = 0.35f;
    public int attackDamage = 40;

    public float attackRate = 1.5f;
    float nextAttackTime = 0f;
    bool m_IsEnemyInRange;


    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Enemy")
        {
            m_IsEnemyInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Enemy")
        {
            m_IsEnemyInRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            //Check Distance To Enemy

            //If distance is less than X

            //Attack()
            nextAttackTime = Time.time + 1f / attackRate;
        }

        if (m_IsEnemyInRange)
        {
            /*
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;


            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
            */
            Debug.Log("Enemy Detected");
        }
    }

    void Attack()
    {
        //Play Attack Animation
        //animator.SetTrigger("Attack");

        //Detect which enemies were hit
        Collider[] hitEnimies = Physics.OverlapSphere(attackPoint.position, attackRange);
        foreach (var enemy in hitEnimies)
        {
            //Deduct health
            //hitCollider.SendMessage("AddDamage");
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
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
