using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveManager : MonoBehaviour
{

    ObjectPooler objectPooler;
    public float timeBetweenWaves = 10f;
    private float countdown = 3f;
    private int waveNumber = 1;


    //Police Car info
    public GameObject policeCar;

    // Start is called before the first frame update
    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        
    }

    // Update is called once per frame
    void Update()
    {
       if(countdown <= 0)
        {
            SpawnWave();
            SpawnCar();
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    void SpawnWave()
    {
        Debug.Log("Wave Incomming!");

        for (int i = 0; i < waveNumber; i++)
        {
            objectPooler.SpawnFromPool("RookieCop", transform.position, Quaternion.identity);
        }


        waveNumber++;
    }

    void SpawnCar()
    {
        //policeCar.transform.position = Vector3.SmoothDamp(policeCar.transform.position, targetPosition, ref velocity, smoothTime, speed);
        policeCar.GetComponent<PoliceCar>().MoveToPosition();
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
