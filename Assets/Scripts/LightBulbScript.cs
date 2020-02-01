using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightbulbScript : MonoBehaviour
{
    // private Vector2 zeroPosition = new Vector2(-6, -4);
    // private int diff = 4;

    public Vector2 gridPosition;
    public int dangerState;
    private bool isOn;

    private SpriteRenderer spriteR;
    public Sprite[] sprites;

    public float timeCounter;

    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        // transform.localPosition = new Vector3(gridPosition.x * diff + zeroPosition.x, gridPosition.y * diff + zeroPosition.y, 0);
        transform.name = "Lightbulb_" + gridPosition.x + "_" + gridPosition.y;
        timeCounter = Random.Range(.1f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        DangerBlink();
        if (isOn)
        {
            spriteR.sprite = sprites[0];
        }
        else
        {
            spriteR.sprite = sprites[1];
        }
    }

    void DangerBlink()
    {
        switch (dangerState)
        {
            case 0:
                isOn = false;
                break;
            case 1:
                if(isOn){
                    if(timeCounter > .2f){
                        isOn = false;
                        timeCounter = 0;
                    }
                }else{
                    if(timeCounter > 1f){
                        isOn = true;
                        timeCounter = 0;
                    }
                }
                break;
            case 2:
                if(isOn){
                    if(timeCounter > .2f){
                        isOn = false;
                        timeCounter = 0;
                    }
                }else{
                    if(timeCounter > .7f){
                        isOn = true;
                        timeCounter = 0;
                    }
                }
                break;
            case 3:
                if(isOn){
                    if(timeCounter > .2f){
                        isOn = false;
                        timeCounter = 0;
                    }
                }else{
                    if(timeCounter > .5f){
                        isOn = true;
                        timeCounter = 0;
                    }
                }
                break;
            case 4:
                if(isOn){
                    if(timeCounter > .2f){
                        isOn = false;
                        timeCounter = 0;
                    }
                }else{
                    if(timeCounter > .3f){
                        isOn = true;
                        timeCounter = 0;
                    }
                }
                break;
            case 5:
                if(isOn){
                    if(timeCounter > .2f){
                        isOn = false;
                        timeCounter = 0;
                    }
                }else{
                    if(timeCounter > .1f){
                        isOn = true;
                        timeCounter = 0;
                    }
                }
                break;
            default:
                isOn = false;
                break;

        }
    }
    IEnumerator OnForMiliseconds(int ms){
        Debug.Log("@"+transform.name+", on for "+ms);
        isOn = true;
        yield return new WaitForSeconds(ms/1000);
    }
    IEnumerator OffForMiliseconds(int ms){
        Debug.Log("@"+transform.name+", off for "+ms);
        isOn = false;
        yield return new WaitForSeconds(ms/1000);
    }
}
