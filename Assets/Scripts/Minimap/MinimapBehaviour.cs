using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapBehaviour : MonoBehaviour
{
    private Camera minimapCamera;
    private Rect minimapRect;
    public float minimapSpeed;
    // Start is called before the first frame update
    void Start()
    {
        minimapCamera = GameObject.Find("Minimap Camera").GetComponent<Camera>();
        minimapRect = new Rect (1, 0, 0.15f, 0.2f);
        minimapCamera.rect = minimapRect;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){
            if(minimapRect.x > .85f){
                minimapRect.x -= minimapSpeed * Time.deltaTime;
            }
        }else{
            if(minimapRect.x < 1){
                minimapRect.x += minimapSpeed * Time.deltaTime;
            }
        }
    minimapCamera.rect = minimapRect;
    }
}
