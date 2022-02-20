using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPositionFinder : MonoBehaviour
{

    Vector3 targetPosition;
    bool changed = false;
    public GameObject Cylinder;
    public GameObject Target;
    Renderer rend;

    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////
    /// GET RANDOM POINT
    /// 
    /// Put prefabs into N, E, S, W
    /// 
    /// When finding a target detination point, get a random prefab, mark that is the arrival point
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ORIGINAL targetPosition " + targetPosition);
        FindArrival();
        rend = Target.GetComponent<Renderer>();
    }



    // Update is called once per frame
    void Update()
    {
        if (changed)
        {
            Debug.Log("CHANGED targetPosition " + targetPosition);
            changed = false;
        }
        //Debug.DrawRay(transform.position, new Vector3(50, 1, 200) * hit.distance, Color.yellow);

        //Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        //Debug.DrawRay(transform.position, forward, Color.green);
    }

    void FindArrival()
    {

        Vector3 size = Vector3.Scale(transform.localScale, Target.GetComponent<Renderer>().bounds.size);


        //Vector3 testing = new Vector3((Random.value - 0.5f) * Target.GetComponent<Renderer>().bounds.size.x, (Random.value - 0.5f) * Target.GetComponent<Renderer>().bounds.size.y, (Random.value - 0.5f) * Target.GetComponent<Renderer>().bounds.size.z);

        Debug.Log("Random Point = " + new Vector3((Random.value - 0.5f) * Target.GetComponent<Renderer>().bounds.size.x, (Random.value - 0.5f) * Target.GetComponent<Renderer>().bounds.size.y, (Random.value - 0.5f) * Target.GetComponent<Renderer>().bounds.size.z));


        /*
        //Raycast towards Center of Map to find arrival target
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        //Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;

        Vector3 fromPosition = transform.position;
        Vector3 toPosition = Cylinder.transform.position;
        Vector3 direction = toPosition - fromPosition;

        if (Physics.Raycast(transform.position, direction, out hit, 100))
        {
            //Debug.DrawRay(transform.position, new Vector3(50, 1, 200) * hit.distance, Color.yellow);
            Debug.Log("Did Hit" + hit.transform.position);
            //Set new targetPosition to hit
            targetPosition = hit.transform.position;

            Debug.Log("targetPosition " + targetPosition);

            changed = true;
        }
        else
        {
            Debug.Log("DID NOT HIT");
        }
        */
    }


    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        //Gizmos.DrawWireCube(detectionPoint.position, detectionRange);
        //Vector3 forward = transform.TransformDirection(Vector3.forward) * 100;

        Vector3 fromPosition = transform.position;
        Vector3 toPosition = Cylinder.transform.position;
        Vector3 direction = toPosition - fromPosition;

        Debug.DrawRay(transform.position, direction, Color.green);


        // A sphere that fully encloses the bounding box.
        Vector3 center = rend.bounds.center;
        float radius = rend.bounds.extents.magnitude;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, radius);
    }
}
