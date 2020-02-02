using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMachine : MonoBehaviour
{
    public ToolWheel tools;

    Ray ray;
    RaycastHit hit;

    public float timerCount;
    public float hp;
    public const float maxHp = 100f;
    public const float minHp = 0f;
    public string requiredTool;
    public bool isActive;
    public bool isDead;
    public bool isDormant;
    public int damageCounter;
    public float dormantTimer;

    public bool isFocused;

    public enum State
    {
        Active,
        Damaged,
        Dead,
        Dormant
    }

    public State _state;

    // Start is called before the first frame update
    void Start()
    {
        tools = GameObject.Find("/GameManager").GetComponent<ToolWheel>();
        
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
    }

    void setVariables()
    {
        selectTool();
        dormantTimer = 10f;
        hp = 95;
        _state = State.Dormant;
        isDormant = true;
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
            Debug.Log("wrong tool, active or dormant");
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
        if (hp >= 100 && isDormant == false)
        {
            _state = State.Active;
            hp = maxHp;
            damageCounter = 0;
            isActive = true;
            isDead = false;
        }
        else if ((hp > 0 && hp < 100) && isDormant == false)
        {
            _state = State.Damaged;
            if(isFocused){
                MouseCheck();
            }
            isActive = false;
            isDead = false;
            setDamageCounter();
        }
        else if (hp <= 0 && isDormant == false)
        {
            _state = State.Dead;
            hp = minHp;
            isDead = true;
            isActive = false;
        }
    }

    void setDamageCounter()
    {
        if (hp > 80)
        {
            damageCounter = 1;
        }
        else if (hp > 60)
        {
            damageCounter = 2;
        }
        else if (hp > 40)
        {
            damageCounter = 3;
        }
        else if (hp > 20)
        {
            damageCounter = 4;
        }
        else
        {
            damageCounter = 5;
        }
    }

    void wakeUpMachine()
    {
        if (dormantTimer <= 0)
        {
            isDormant = false;
            dormantTimer = 0f;
        }
    }

    void MouseCheck()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonUp(0))
            {
                repair();
            }
        }
    }
}
