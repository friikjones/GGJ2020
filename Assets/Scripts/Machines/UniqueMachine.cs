using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueMachine : GenericMachine
{
    public GameManagerScript gameManager;

    public bool colateralDone;

    // Start is called before the first frame update
    void Start()
    {
        tools = GetComponent<ToolWheel>();
        gameManager = GameObject.Find("/GameManager").GetComponent<GameManagerScript>();

        setVariables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        timerCount += Time.deltaTime;
        dormantTimer -= Time.deltaTime;

        wakeUpMachine();
        doDamage();
        switchState();
        doColateral();
    }

    public void setVariables()
    {
        selectTool();
        dormantTimer = 10f;
        hp = 95;
        _state = State.Dormant;
        isDormant = true;
        isFocused = false;
        colateralDone = false;
    }

    public void doColateral()
    {   
        if (isDead && colateralDone == false)
        {
            if (this.tag == "SewagePump")
            {
                colateralDone = true;
                int targetX = Random.Range(0,3);
                int targetY = Random.Range(0,2); 
                gameManager.roomBoard[targetX,targetY].GetComponentInChildren<GenericMachine>().hp -= 50;
            }
            else if (this.tag == "WaterPump")
            {
                colateralDone = true;   
                gameManager.roomBoard[0,1].GetComponentInChildren<GenericMachine>().hp -= 25;
                gameManager.roomBoard[1,2].GetComponentInChildren<GenericMachine>().hp -= 25;
                gameManager.roomBoard[2,1].GetComponentInChildren<GenericMachine>().hp -= 25;
                gameManager.roomBoard[1,0].GetComponentInChildren<GenericMachine>().hp -= 25;
            }
        }
    }
}
