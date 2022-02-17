using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{

    public Vector3 formationPosition;

    // Start is called before the first frame update
    void Start()
    {
        formationPosition = gameObject.transform.position;
    }


    public void UpdateFormationPosition(Vector3 newPosition)
    {
        formationPosition = newPosition;
        Debug.Log("New POS = "+ formationPosition);
    }
}
