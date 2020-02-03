using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueMachine : GenericMachine {
    public GameManagerScript gameManager;
    public RoomSetupScript roomSetup;
    public GameObject OutofpowerOverlay;

    public bool colateralDone;
    // public float robotTimer;

    // Start is called before the first frame update
    void Start () {
        tools = GetComponent<ToolWheel> ();
        gameManager = GameObject.Find ("/GameManager").GetComponent<GameManagerScript> ();
        OutofpowerOverlay = GameObject.Find ("/Minimap/Minimap Canvas/OutofpowerOverlay");
        OutofpowerOverlay.SetActive (false);
        roomSetup = this.transform.parent.GetComponent<RoomSetupScript> ();

        setVariables ();
    }

    // Update is called once per frame
    void Update () {

    }

    void FixedUpdate () {
        timerCount += Time.deltaTime;
        dormantTimer -= Time.deltaTime;
        // robotTimer += Time.deltaTime;

        wakeUpMachine ();
        doDamage ();
        switchState ();
        doColateral ();
    }

    public void setVariables () {
        selectTool ();
        dormantTimer = Random.Range(2,12f);
        hp = 95;
        _state = State.Dormant;
        isDormant = true;
        isFocused = false;
        colateralDone = false;
    }

    public void doColateral () {
        if (isDead && colateralDone == false) {
            if (this.tag == "SewagePump") {
                colateralDone = true;
                int targetX = Random.Range (0, 3);
                int targetY = Random.Range (0, 2);
                gameManager.roomBoard[targetX, targetY].GetComponentInChildren<GenericMachine> ().hp -= 50;
            } else if (this.tag == "WaterPump") {
                colateralDone = true;
                gameManager.roomBoard[0, 1].GetComponentInChildren<GenericMachine> ().hp -= 25;
                gameManager.roomBoard[1, 2].GetComponentInChildren<GenericMachine> ().hp -= 25;
                gameManager.roomBoard[2, 1].GetComponentInChildren<GenericMachine> ().hp -= 25;
                gameManager.roomBoard[1, 0].GetComponentInChildren<GenericMachine> ().hp -= 25;
            }
        }
        /*else if (damageCounter > 3 && this.tag == "Robot") {
               //     if (robotTimer > 5f) {
               //         robotTimer = 0f;
               //         int targetX = Random.Range (0, 3);
               //         int targetY = Random.Range (0, 2);
               //         roomSetup.gridPosition.x = targetX;
               //         roomSetup.gridPosition.y = targetY;
               //     }
               }*/
        else if (damageCounter > 4 && colateralDone == false && this.tag == "Generator") {
            colateralDone = true;
            OutofpowerOverlay.SetActive (true);
        } else if (damageCounter <= 2 && colateralDone == true && this.tag == "Generator") {
            colateralDone = false;
            OutofpowerOverlay.SetActive (false);
        } else if (damageCounter > 3 && colateralDone == false && this.tag == "ElevatorControl") {
            colateralDone = true;
            GameObject.Find ("/Map/Main Camera").GetComponent<GameCamera> ().roomTransitionDuration = 4;
        } else if (damageCounter <= 2 && colateralDone == true && this.tag == "ElevatorControl") {
            colateralDone = false;
            GameObject.Find ("/Map/Main Camera").GetComponent<GameCamera> ().roomTransitionDuration = 1;
        } else if (hp < 30 && colateralDone == false && this.tag == "CCTV") {
            colateralDone = true;
            for (int i = 0; i < gameManager.roomBoard.GetLength (0); i++) {
                for (int j = 0; j < gameManager.roomBoard.GetLength (1); j++) {
                    gameManager.roomBoard[i, j].transform.Find ("Overlay").GetComponent<Renderer> ().enabled = true;
                    gameManager.roomBoard[i, j].transform.Find ("Overlay").GetComponent<Renderer> ().material = gameManager.roomBoard[i, j].GetComponent<RoomSetupScript> ().overlayMat[2];
                }
            }
        } else if (hp >= 30 && colateralDone == true && this.tag == "CCTV") {
            colateralDone = false;
            for (int i = 0; i < gameManager.roomBoard.GetLength (0); i++) {
                for (int j = 0; j < gameManager.roomBoard.GetLength (1); j++) {
                    gameManager.roomBoard[i, j].transform.Find ("Overlay").GetComponent<Renderer> ().enabled = false;
                }
            }
        }
    }
}