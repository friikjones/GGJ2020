using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameCamera mainCamera;

    public GameObject[,] lightBoard = new GameObject[4, 3];
    public GameObject[,] roomBoard = new GameObject[4,3];
    // Start is called before the first frame update
    void Start()
    {
        FindLights();
        RandomLights();
        StartCoroutine(StartAsync());
    }

    IEnumerator StartAsync(){
        yield return new WaitForSeconds(3f);
        mainCamera.ZoomRoom(GameObject.Find("/Map/RoomGrid/Room_0_0"));
    }

    // Update is called once per frame
    void Update()
    {

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
                Debug.Log("@" + i + "," + j + ", found: " + lightBoard[i, j].name);
            }
        }
    }

    void FindRooms()
    {
        for (int i = 0; i < roomBoard.GetLength(0); i++)
        {
            for (int j = 0; j < roomBoard.GetLength(1); j++)
            {
                string aux = "Minimap/Minimap Canvas/Room_" + i + "_" + j;
                roomBoard[i, j] = GameObject.Find(aux);
                Debug.Log("@" + i + "," + j + ", found: " + roomBoard[i, j].name);
            }
        }
    }
}
