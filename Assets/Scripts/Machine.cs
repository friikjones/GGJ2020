using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public ToolWheel tools;

    Ray ray;
    RaycastHit hit;

    public float timerCount;
    public float hp;
    public string requiredTool;
    public bool isActive;
    public bool isDead;
    public int damageCounter;

    public enum State
    {
        Active,
        Damaged,
        Dead
    }

    public State _state;

    // Start is called before the first frame update
    void Start()
    {
        tools = this.GetComponent<ToolWheel>();
        selectTool();
        hp = 95;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        timerCount += Time.deltaTime;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonUp(0))
            {
                repair();
            }
        }

        doDamage();
        switchState();
    }

    void selectTool()
    {
        int rnd = Random.Range(0,4);
        switch(rnd)
        {
            case 0:
            requiredTool = "Hammer";
            break;
            case 1:
            requiredTool = "Wrench";
            break;
            case 2:
            requiredTool = "Drill";
            break;
            case 3:
            requiredTool = "DuctTape";
            break;
            default:
            requiredTool = "Hammer";
            break;
        }
    }

    void repair()
    {
        if (tools.currentTool == requiredTool)
        {
            hp += 5;
        }
        else
        {
            Debug.Log("wrong tool");
        }
    }

    void doDamage()
    {
        if (timerCount > 1f)
        {
            if (_state == State.Damaged)
            {
                hp -= 2;
            }
            timerCount = 0;
        }
    }

    void switchState()
    {
        if (hp >= 100)
        {
            _state = State.Active;
            isActive = true;
            isDead = false;
        }
        else if (hp > 0 && hp < 100)
        {
            _state = State.Damaged;
            isActive = false;
            isDead = false;
        }
        else if (hp <= 0)
        {
            _state = State.Dead;
            isDead = true;
            isActive = false;
        }
    }
}
