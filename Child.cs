using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Child : MonoBehaviour
{
    NavMeshAgent childAgent;
    public Animator animator;
    public Vector3 formationPosition;

    public int maxHealth = 100;
    int currentHealth;
    bool walking = false;


    // Start is called before the first frame update
    void Start()
    {
        formationPosition = gameObject.transform.position;
        childAgent = gameObject.GetComponent<NavMeshAgent>();
        //animator = gameObject.GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (childAgent.remainingDistance <= childAgent.stoppingDistance)
        {
            walking = false;
        }
        else
        {
            walking = true;
        }
        animator.SetBool("IsMoving", walking);
    }

    public void UpdateFormationPosition(Vector3 newPosition)
    {
        formationPosition = newPosition;
        //Debug.Log("New POS = "+ formationPosition);
    }

    public void GoToFormationPosition()
    {
        animator.SetBool("IsMoving", true);
        //Set destination of all children to same as Parent
        childAgent.destination = formationPosition;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("PLAYER HAS TAKEN DAMAGE!!!!");
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
        animator.SetBool("IsDead", true);
        //Disable The Enemy
        transform.position = new Vector3(1000, 1000, 1000);
        gameObject.tag = "Dead";
        GetComponent<CapsuleCollider>().enabled = false;

        //Disable
        gameObject.SetActive(false);
        this.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.blue;
    }
}
