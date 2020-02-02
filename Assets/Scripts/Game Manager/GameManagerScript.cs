using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    [HideInInspector]
    public static GameManagerScript instance;

    public GameCamera mainCamera;
    public GameObject[,] lightBoard = new GameObject[4, 3];
    public GameObject[,] roomBoard = new GameObject[4,3];
    public ExitButton exitButton;

    public bool zoomed;
    public GameObject openedRoom = null;


    void Awake(){
        instance = this;
    }

    void Start()
    {
        FindLights();
        // RandomLights();
        FindRooms();
    }

    void Update()
    {
        // Detect any clicked room.
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Room"))) {
                openedRoom = hit.collider.transform.parent.gameObject;
                if(mainCamera.ZoomInRoom(openedRoom)){
                    exitButton.ShowAnim();
                    zoomed = true;
                }
            }
        }
        if(zoomed && Input.GetKeyDown(KeyCode.RightShift)){
            ExitRoom();
        }

        UpdateDanger();
    }

    void RandomLights()
    {
        for (int i = 0; i < lightBoard.GetLength(0); i++)
        {
            for (int j = 0; j < lightBoard.GetLength(1); j++)
            {
               lightBoard[i, j].GetComponent<LightbulbScript>().dangerState = Random.Range(0, 6);
            }
        }
    }

    void FindLights()
    {
        for (int i = 0; i < lightBoard.GetLength(0); i++)
        {
            for (int j = 0; j < lightBoard.GetLength(1); j++)
            {
                string aux = "Minimap/Minimap Canvas/Lightbulb_" + i + "_" + j;
                lightBoard[i, j] = GameObject.Find(aux);
            }
        }
    }

    void FindRooms()
    {
        for (int i = 0; i < roomBoard.GetLength(0); i++)
        {
            for (int j = 0; j < roomBoard.GetLength(1); j++)
            {
                string aux = "Map/RoomGrid/Room_" + i + "_" + j;
                roomBoard[i, j] = GameObject.Find(aux);
            }
        }
    }

    void UpdateDanger(){
        for (int i = 0; i < roomBoard.GetLength(0); i++)
        {
            for (int j = 0; j < roomBoard.GetLength(1); j++)
            {
                lightBoard[i,j].GetComponent<LightbulbScript>().dangerState = roomBoard[i,j].GetComponentInChildren<GenericMachine>().damageCounter;
            }
        }
    }

    public void ExitRoom(){
        if(mainCamera.ZoomOutRoom()){
            exitButton.HideAnim();
            openedRoom = null;
            zoomed = false;
        }
    }

}
