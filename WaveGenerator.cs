using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
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
        objectPooler.SpawnFromPool("RookieCop", transform.position, Quaternion.identity);
    }
}
