using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        // transform.localPosition = new Vector3(gridPosition.x * diff.x + zeroPosition.x, gridPosition.y * diff.y + zeroPosition.y, 0);
        transform.name = "Room_" + gridPosition.x + "_" + gridPosition.y;
        overlayRenderer = transform.Find("Overlay").GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
