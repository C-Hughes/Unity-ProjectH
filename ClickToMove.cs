using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ClickToMove : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3[] childPositions;

    public static GameObject controlledUnit = null;
    public static GameObject groundTiles;
    public static GameObject unitPositions;
    public static GameObject childPositionsContainer;
    //LOGGING
    public GameObject slowMoUI;
    GameObject gameMaster;
    //Debug.Log("TEST" + hit.collider.tag);

    void Start()
    {
        gameMaster = GameObject.Find("/GameMaster");
        slowMoUI = GameObject.Find("/SlowMoCanvas");
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
                if (hit.collider.CompareTag("Player"))
                {
                    controlledUnit = hit.transform.gameObject;
                    childPositionsContainer = controlledUnit.transform.parent.gameObject.transform.GetChild(controlledUnit.transform.parent.gameObject.transform.childCount - 1).gameObject;
                    ToggleMoveToObjects(true);
                }
                //For testing purposes
                else if (hit.collider.CompareTag("Enemy"))
                {
                    controlledUnit = hit.transform.gameObject;
                    ToggleMoveToObjects(true);
                }
                else if (hit.collider.CompareTag("MoveTo"))
                {
                    if (controlledUnit != null)
                    {
                        //Move Agent to Center of Square
                        agent = controlledUnit.GetComponent<NavMeshAgent>();
                        agent.destination = hit.transform.position;
                        //Move GridObject to New Locations
                        if(controlledUnit.GetComponent<Collider>().CompareTag("Player"))
                        {
                            //Move childPositions Object to new position
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
            //ReturnToFormation();
            ToggleMoveToObjects(false);
            //gameMaster.GetComponent<WaveManager>().SpawnOne();
        }
    }


    void MoveGroup()
    {
        //NavMeshAgent childAgent;
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
            if (child.gameObject.CompareTag("Child"))
            {
                //Set destination of all children to same as Parent
                //childAgent = child.GetComponent<NavMeshAgent>();
                //childAgent.destination = childPositions[j];
                //Set new Destination to Childs Script
                child.GetComponent<Child>().UpdateFormationPosition(childPositions[j]);
                child.GetComponent<Child>().GoToFormationPosition();
                j++;
            }
        }  
    }

    void ToggleMoveToObjects(bool enable)
    {
        SlowMo(enable);

        foreach (Transform child in groundTiles.transform)
        {
            foreach (Transform grandchild in child)
            {
                if (grandchild.gameObject.CompareTag("MoveTo"))
                {
                    grandchild.gameObject.GetComponent<MeshRenderer>().enabled = enable;
                }
            }
        }
    }

    void SlowMo(bool enable)
    {
        if (enable)
        {
            Time.timeScale = 0.05f;
        }
        else
        {
            //Disable SlowMo Overlay
            Time.timeScale = 1f;
        }
        slowMoUI.transform.Find("SlowMo").gameObject.SetActive(enable);
    }


    /*
    public void StopNavigation()
    {
        Debug.Log("Stopping Navigation");
        //transform.GetComponent<NavMeshAgent>().isStopped = true;
    }

    public void ReturnToFormation()
    {
        Debug.Log(gameObject.transform.parent.gameObject.transform.GetChild(gameObject.transform.parent.gameObject.transform.childCount - 1).gameObject);
    }*/
}
