using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolWheel : MonoBehaviour
{
    public string currentTool;
    // Start is called before the first frame update
    void Start()
    {
        currentTool = "Hammer";
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyUp(KeyCode.Alpha1)) {
           currentTool = "Hammer";
           Debug.Log("Hammer equipped");
       } 
       else if (Input.GetKeyUp(KeyCode.Alpha2)) {
           currentTool = "Wrench";
           Debug.Log("Wrench equipped");
       }
       else if (Input.GetKeyUp(KeyCode.Alpha3)) {
           currentTool = "Drill";
           Debug.Log("Drill equipped");
       }
       else if (Input.GetKeyUp(KeyCode.Alpha4)) {
           currentTool = "DuctTape";
           Debug.Log("DuctTape equipped");
       }
    }
}
