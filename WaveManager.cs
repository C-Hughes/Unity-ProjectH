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
    Vector3 targetPosition = new Vector3(60, 1, 203);
    public float smoothTime = 4f;
    public float speed = 8;
    Vector3 velocity;

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

        policeCar.transform.position = Vector3.SmoothDamp(policeCar.transform.position, targetPosition, ref velocity, smoothTime, speed);
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
        Debug.Log("Moving Car "+ targetPosition);
        policeCar.transform.position = Vector3.SmoothDamp(policeCar.transform.position, targetPosition, ref velocity, smoothTime, speed);
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
