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

    //NavMesh Agent
    NavMeshAgent agent;

    public void OnObjectSpawn()
    {
        currentHealth = maxHealth;
        Debug.Log("I have just spawned!!!");

        //On Spawn, Find closest Player car...
        //Go towards car...
        //If player is within enemies range, it should attack...
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
