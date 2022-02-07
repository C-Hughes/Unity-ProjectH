using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public Animator animator;

    public float attackRange = 0.35f;
    public int attackDamage = 40;

    public float attackRate = 1.5f;
    float nextAttackTime = 0f;

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
    }
}
