using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveManager : MonoBehaviour
{

    ObjectPooler objectPooler;

    // Start is called before the first frame update
    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }

    public void SpawnOne()
    {
        NavMeshHit closestHit;

        if (NavMesh.SamplePosition(transform.position, out closestHit, 500f, NavMesh.AllAreas))
        {
            //gameObject.transform.position = closestHit.position;
            objectPooler.SpawnFromPool("RookieCop", closestHit.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Could not find position on NavMesh!");
        }
              

        
    }
}
