using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolIconScript : MonoBehaviour
{
    public Sprite hammer;
    public Sprite wrench;
    public Sprite drill;
    public Sprite ductTape;
    public string currentTool;
    public ToolWheel script;

    public Vector3 mousePos;
    public Camera mainCamera;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateTool();
        
    }

    void UpdateTool(){
        currentTool = GameObject.Find("/GameManager").GetComponent<ToolWheel>().currentTool;
        switch(currentTool){
            case "Hammer":
                transform.GetComponent<SpriteRenderer>().sprite = hammer;
                offset = new Vector3(.1f,-.04f,0);
                break;
            case "Wrench":
                transform.GetComponent<SpriteRenderer>().sprite = wrench;
                offset = new Vector3(.08f,-.08f,0);
                break;
            case "Drill":
                transform.GetComponent<SpriteRenderer>().sprite = drill;
                offset = new Vector3(.1f,-.15f,0);
                break;
            case "DuctTape":
                transform.GetComponent<SpriteRenderer>().sprite = ductTape;
                offset = new Vector3(.1f,-.08f,0);
                break;
        }
        mousePos = Input.mousePosition;
        mousePos.z = 1;
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);
        transform.position = mousePos+offset;

    }
}
