using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbScript : MonoBehaviour
{
    private Vector2 zeroPosition = new Vector2(-6, -4);
    private int diff = 4;

    public Vector2 gridPosition;
    public int dangerState;
    private bool isOn;

    private SpriteRenderer spriteR;
    public Sprite[] sprites;


    // Start is called before the first frame update
    void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        transform.localPosition = new Vector3(gridPosition.x * diff + zeroPosition.x, gridPosition.y * diff + zeroPosition.y, 0);
        transform.name = "Lightbulb_" + gridPosition.x + "_" + gridPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
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
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                isOn = true;
                break;
            default:
                isOn = false;
                break;

        }
    }
}
