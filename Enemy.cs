using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Hurt Animation
        //Knockback effect...

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Play Die Animation...
        Debug.Log("DIE!");
        //animator.SetBool("IsDead", true);
        //Disable The Enemy
        GetComponent<CapsuleCollider>().enabled = false;
        this.enabled = false;
    }
}
