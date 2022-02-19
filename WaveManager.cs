using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{

    ObjectPooler objectPooler;
    public float timeBetweenWaves = 10f;
    private float countdown = 5f;
    private int waveNumber = 0;
    public static int EnemiesAlive = 0;


    //Police Car info
    public GameObject policeCar;
    public Wave[] waves;

    //Text
    public TMPro.TMP_Text waveIncomingText;




    // Start is called before the first frame update
    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemiesAlive > 0)
        {
            return;
        }
        
        if(countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            SpawnCar();
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveNumber];

        waveIncomingText.text = "Wave Incoming!";
        waveNumber++;

        yield return new WaitForSeconds(2.0f);

        for (int i = 0; i < wave.count; i++)
        {
            //SpawnEnemy(wave.enemyName);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIncomingText.text = " ";

        if (waveNumber == waves.Length)
        {
            Debug.Log("Level Completed!");
            this.enabled = false;
        }
    }

    void SpawnEnemy(string enemyToSpawn)
    {
        objectPooler.SpawnFromPool(enemyToSpawn, transform.position, Quaternion.identity);
        EnemiesAlive++;
    }

    void SpawnCar()
    {
        //policeCar.transform.position = Vector3.SmoothDamp(policeCar.transform.position, targetPosition, ref velocity, smoothTime, speed);
        policeCar.GetComponent<PoliceCar>().MoveToPosition();
    }

    /*
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
    */
}
