using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Child : MonoBehaviour
{
    NavMeshAgent childAgent;
    public Vector3 formationPosition;

    // Start is called before the first frame update
    void Start()
    {
        formationPosition = gameObject.transform.position;
        childAgent = gameObject.GetComponent<NavMeshAgent>();
    }


    public void UpdateFormationPosition(Vector3 newPosition)
    {
        formationPosition = newPosition;
        //Debug.Log("New POS = "+ formationPosition);
    }

    public void GoToFormationPosition()
    {
        //Set destination of all children to same as Parent
        childAgent.destination = formationPosition;
    }
}
