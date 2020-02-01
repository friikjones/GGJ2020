using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public bool[,] problemBoard = new bool[4, 3];
    public GameObject[,] lightBoard = new GameObject[4, 3];
    // Start is called before the first frame update
    void Start()
    {
        FindEveryOne();
        RandomLights();
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
                lightBoard[i, j].GetComponent<LightBulbScript>().dangerState = Random.Range(0, 6);
            }
        }
    }

    void FindEveryOne()
    {
        for (int i = 0; i < problemBoard.GetLength(0); i++)
        {
            for (int j = 0; j < problemBoard.GetLength(1); j++)
            {
                Debug.Log("@" + i + "," + j + ", value: " + problemBoard[i, j]);
                string aux = "Minimap/Minimap Canvas/Lightbulb_" + i + "_" + j;
                Debug.Log(aux);
                lightBoard[i, j] = GameObject.Find(aux);
                Debug.Log("@" + i + "," + j + ", found: " + lightBoard[i, j].name);
            }
        }
    }
}
