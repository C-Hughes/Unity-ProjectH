using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ClickToMove : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3[] childPositions;

    public static GameObject controlledUnit = null;
    public static GameObject groundTiles;
    public static GameObject unitPositions;
    public static GameObject childPositionsContainer;
    //LOGGING
    //Debug.Log("TEST" + hit.collider.tag);

    void Start()
    {
        groundTiles = GameObject.Find("/TestScene/GroundTiles");
        ToggleMoveToObjects(false); //Make Highlighted MoveTo Squares Invisible
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !PauseMenu.GameIsPaused)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                //Check if Hit is Player
                if (hit.collider.tag == "Player")
                {
                    controlledUnit = hit.transform.gameObject; // if using custom type, cast the result to type here
                    childPositionsContainer = controlledUnit.transform.parent.gameObject.transform.GetChild(controlledUnit.transform.parent.gameObject.transform.childCount - 1).gameObject;
                    ToggleMoveToObjects(true);
                }
                else if (hit.collider.tag == "Enemy")
                {
                    controlledUnit = hit.transform.gameObject; // if using custom type, cast the result to type here
                    ToggleMoveToObjects(true);
                }
                else if (hit.collider.tag == "MoveTo")
                {
                    if (controlledUnit != null)
                    {
                        //Move Agent to Center of Square
                        agent = controlledUnit.GetComponent<NavMeshAgent>();
                        agent.destination = hit.transform.position;
                        //Move GridObject to New Locations
                        if(controlledUnit.GetComponent<Collider>().tag == "Player")
                        {
                            childPositionsContainer.transform.position = agent.destination;
                            MoveGroup();
                        }  
                    }
                    ToggleMoveToObjects(false);
                }
                else
                {
                    controlledUnit = null;
                    ToggleMoveToObjects(false);
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            controlledUnit = null;
        }
    }


    void MoveGroup()
    {
        NavMeshAgent childAgent;
        childPositions = new Vector3[childPositionsContainer.transform.childCount];

        //Get Array of Child Positions
        for (int i = 0; i < childPositionsContainer.transform.childCount; i++)
        {
            childPositions[i] = childPositionsContainer.transform.GetChild(i).position;
        }

        //Get Siblings of Main controlledUnit
        int j = 0;
        foreach (Transform child in controlledUnit.transform.parent.gameObject.transform)
        {
            if (child.gameObject.tag == "Child")
            {
                //Set destination of all children to same as Parent
                childAgent = child.GetComponent<NavMeshAgent>();
                childAgent.destination = childPositions[j];
                j++;
            }
        }  
    }

    void ToggleMoveToObjects(bool enable)
    {
        if (enable)
        {
            //Enable SlowMo Overlay
            Time.timeScale = 0.05f;
        } 
        else
        {
            //Disable SlowMo Overlay
            Time.timeScale = 1f;
        }

        foreach (Transform child in groundTiles.transform)
        {
            foreach (Transform grandchild in child)
            {
                if (grandchild.gameObject.tag == "MoveTo")
                {
                    grandchild.gameObject.GetComponent<MeshRenderer>().enabled = enable;
                }
            }
        }
    }

    public void StopNavigation()
    {
        Debug.Log("Stopping Navigation");
        transform.GetComponent<NavMeshAgent>().isStopped = true;
    }
}
