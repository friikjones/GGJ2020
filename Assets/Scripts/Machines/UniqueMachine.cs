using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueMachine : GenericMachine {
    public GameManagerScript gameManager;
    public RoomSetupScript roomSetup;
    public GameObject OutofpowerOverlay;

    public bool colateralDone;
    public float robotTimer;
    public float gridPosX;
    public float gridPosY;

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
        robotTimer += Time.deltaTime;

        wakeUpMachine ();
        doDamage ();
        switchState ();
        doColateral ();
    }

    public void setVariables () {
        selectTool ();
        dormantTimer = 10f;
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
        } else if (damageCounter > 3 && this.tag == "Robot") {
            if (robotTimer > 5f) {
                robotTimer = 0f;
                int targetX = Random.Range (0, 3);
                int targetY = Random.Range (0, 2);
                roomSetup.gridPosition.x = targetX;
                roomSetup.gridPosition.y = targetY;
            }
        } else if (damageCounter > 4 && this.tag == "Generator") {
            OutofpowerOverlay.SetActive (true);
            if (damageCounter <= 2) {
                OutofpowerOverlay.SetActive (false);
            }
        } else if (damageCounter > 3 && this.tag == "ElevatorControl") {
            GameObject.Find ("/Map/Main Camera").GetComponent<GameCamera> ().roomTransitionDuration = 4;
            if (damageCounter <= 2) {
                GameObject.Find ("/Map/Main Camera").GetComponent<GameCamera> ().roomTransitionDuration = 1;
            }
        }
    }
}