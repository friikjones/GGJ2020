using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomSetupScript : MonoBehaviour
{
    // public Vector2 zeroPosition;
    // public Vector2 diff;

    public Vector2 gridPosition;
    public int dangerState;

    public Material[] overlayMat;
    private Renderer overlayRenderer;
    public bool completed;
    public bool failed;

    public GameObject[] machines;
    public GameObject machineInstance;
    public float diffX = .2f;
    public float diffY = .2f;

    public Text life;

    // Start is called before the first frame update
    void Start()
    {
        // transform.localPosition = new Vector3(gridPosition.x * diff.x + zeroPosition.x, gridPosition.y * diff.y + zeroPosition.y, 0);
        transform.name = "Room_" + gridPosition.x + "_" + gridPosition.y;
        overlayRenderer = transform.Find("Overlay").GetComponent<Renderer>();

        if(gridPosition.x == 0 && gridPosition.y == 0){
            machineInstance = Instantiate(machines[0],Vector3.zero,Quaternion.identity);
        }else if(gridPosition.x == 1 && gridPosition.y == 1){
            machineInstance = Instantiate(machines[1],Vector3.zero,Quaternion.identity);
        }else if(gridPosition.x == 2 && gridPosition.y == 0){
            machineInstance = Instantiate(machines[2],Vector3.zero,Quaternion.identity);
        }else if(gridPosition.x == 3 && gridPosition.y == 2){
            machineInstance = Instantiate(machines[3],Vector3.zero,Quaternion.identity);
        }else if(gridPosition.x == 3 && gridPosition.y == 1){
            machineInstance = Instantiate(machines[4],Vector3.zero,Quaternion.identity);
        }else if(gridPosition.x == 0 && gridPosition.y == 2){
            machineInstance = Instantiate(machines[5],Vector3.zero,Quaternion.identity);
        }else{
            machineInstance = Instantiate(machines[6],Vector3.zero,Quaternion.identity);
        }
        machineInstance.transform.parent = this.transform;
        machineInstance.transform.localPosition = new Vector3(0,0,-.1f);
        // machineInstance.GetComponent<GenericMachine>().isFocused = true;
    }

    // Update is called once per frame
    void Update()
    {

        life.text = GetComponentInChildren<GenericMachine>().hp.ToString();

        if(failed){
            overlayRenderer.material = overlayMat[1];
            overlayRenderer.enabled = true;
        }
        else if(completed){
            overlayRenderer.material = overlayMat[0];
            overlayRenderer.enabled = true;

        }else{
            overlayRenderer.enabled = false;
        }
    }
}
