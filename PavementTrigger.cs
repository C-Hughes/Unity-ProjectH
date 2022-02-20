using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PavementTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Vehicle"))
        {
            //other.GetComponent<PoliceCar>().Arrived();
        }
    }
}
