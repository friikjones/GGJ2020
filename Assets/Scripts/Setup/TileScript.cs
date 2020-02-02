using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Vector2 gridPosition;
    public Vector2 zeroPosition;
    public float diff;
    // Start is called before the first frame update
    void Awake()
    {
        transform.localPosition = new Vector2(gridPosition.x * diff + zeroPosition.x, gridPosition.y * diff + zeroPosition.y);
        transform.name = "Tile_"+gridPosition.x+"_"+gridPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
