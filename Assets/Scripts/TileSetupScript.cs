using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSetupScript : MonoBehaviour
{
    public GameObject[,] tileMap = new GameObject[8,8];
    public Material[] materials = new Material[5];
    public bool[,] pattern_0, pattern_1, pattern_2, pattern_3, pattern_4, pattern_5;
    public Transform test;
    // Start is called before the first frame update
    void Start()
    {
        //Adding patterns
        pattern_0 = new bool[,]{{false,true ,false,false,false,false,true ,false},
                                {true ,true ,true ,false,false,true ,true ,true },
                                {false,true ,false,true ,true ,false,true ,false},
                                {false,false,true ,false,false,true ,false,false},
                                {false,false,true ,false,false,true ,false,false},
                                {false,true ,false,true ,true ,false,true ,false},
                                {true ,true ,true ,false,false,true ,true ,true },
                                {false,true ,false,false,false,false,true ,false}};
        
        pattern_1 = new bool[,]{{true ,false,true ,true ,true ,true ,false,true },
                                {false,false,true ,false,false,true ,false,false},
                                {true ,true ,true ,false,false,true ,true ,true },
                                {true ,false,false,false,false,false,false,true },
                                {true ,false,false,false,false,false,false,true },
                                {true ,true ,true ,false,false,true ,true ,true },
                                {false,false,true ,false,false,true ,false,false},
                                {true ,false,true ,true ,true ,true ,false,true }};
        
        
        pattern_2 = new bool[,]{{false,false,true ,false,false,true ,false,false},
                                {false,false,true ,false,false,true ,false,false},
                                {true ,true ,false,true ,true ,false,true ,true },
                                {false,false,true ,false,false,true ,false,false},
                                {false,false,true ,false,false,true ,false,false},
                                {true ,true ,false,true ,true ,false,true ,true },
                                {false,false,true ,false,false,true ,false,false},
                                {false,false,true ,false,false,true ,false,false}};
        
        pattern_3 = new bool[,]{{true ,true ,false,false,false,false,true ,true },
                                {true ,true ,false,false,false,false,true ,true },
                                {false,false,true ,false,false,true ,false,false},
                                {false,false,false,true ,true ,false,false,false},
                                {false,false,false,true ,true ,false,false,false},
                                {false,false,true ,false,false,true ,false,false},
                                {true ,true ,false,false,false,false,true ,true },
                                {true ,true ,false,false,false,false,true ,true }};
        
        pattern_4 = new bool[,]{{false,false,false,true ,true ,false,false,false},
                                {false,false,false,true ,true ,false,false,false},
                                {false,false,false,true ,true ,false,false,false},
                                {true ,true ,true ,true ,true ,true ,true ,true },
                                {true ,true ,true ,true ,true ,true ,true ,true },
                                {false,false,false,true ,true ,false,false,false},
                                {false,false,false,true ,true ,false,false,false},
                                {false,false,false,true ,true ,false,false,false}};

        pattern_5 = new bool[,]{{true ,true ,false,false,false,false,true ,true },
                                {true ,true ,true ,false,false,true ,true ,true },
                                {false,true ,true ,true ,true ,true ,true ,false},
                                {false,false,true ,false,false,true ,false,false},
                                {false,false,true ,false,false,true ,false,false},
                                {false,true ,true ,true ,true ,true ,true ,false},
                                {true ,true ,true ,false,false,true ,true ,true },
                                {true ,true ,false,false,false,false,true ,true }};

        FindAllTiles();
        ApplyMaterials();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FindAllTiles(){
        for (int i = 0; i < tileMap.GetLength(0); i++)
        {
            for (int j = 0; j < tileMap.GetLength(1); j++)
            {
                string aux = "Tile_" + i + "_" + j;
                tileMap[i, j] = transform.Find(aux).gameObject;
                Debug.Log("@" + i + "," + j + ", found: " + tileMap[i, j].name);
            }
        }
    }

    void ApplyMaterials(){
        switch(Random.Range(0,7)){
            case 0:
                ApplyPatterMaterial(pattern_0);
                break;
            case 1:
                ApplyPatterMaterial(pattern_1);
                break;
            case 2:
                ApplyPatterMaterial(pattern_2);
                break;
            case 3:
                ApplyPatterMaterial(pattern_3);
                break;
            case 4:
                ApplyPatterMaterial(pattern_4);
                break;
            case 5:
                ApplyPatterMaterial(pattern_5);
                break;
            default:
                ApplyUniformMaterials(materials[Random.Range(0,5)]);
                break;
        }
    }

    void ApplyPatterMaterial(bool[,] pattern){
        Material mat1 = materials[Random.Range(0,5)];
        Material mat2;
        do{
            mat2 = materials[Random.Range(0,5)];
        }while(mat2 == mat1);

        for (int i = 0; i < tileMap.GetLength(0); i++)
        {
            for (int j = 0; j < tileMap.GetLength(1); j++)
            {
                if(pattern[i,j]){
                    tileMap[i,j].GetComponent<MeshRenderer>().material = mat1;
                }else{
                    tileMap[i,j].GetComponent<MeshRenderer>().material = mat2;
                }
            }
        }
    }

    void ApplyUniformMaterials(Material inputMaterial){
        for (int i = 0; i < tileMap.GetLength(0); i++)
        {
            for (int j = 0; j < tileMap.GetLength(1); j++)
            {
                tileMap[i,j].GetComponent<MeshRenderer>().material = inputMaterial;
            }
        }
    }
}
