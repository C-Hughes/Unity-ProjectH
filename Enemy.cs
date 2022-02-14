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
        //Debug.Log("I'm hit!");
        //Hurt Animation
        //Knockback effect...

        if (currentHealth <= 0)
        {
            this.GetComponent<ClickToMove>().StopNavigation();
            Die();
        }
    }

    void Die()
    {
        //Play Die Animation...
        //animator.SetBool("IsDead", true);
        //Disable The Enemy
        GetComponent<CapsuleCollider>().enabled = false;
        gameObject.SetActive(false);
        gameObject.tag = "Dead";
        transform.position = new Vector3(1000, 1000, 1000);
        this.enabled = false;
    }
}
