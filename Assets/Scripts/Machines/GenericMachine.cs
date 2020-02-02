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
        tools = GameManagerScript.instance.gameObject.GetComponent<ToolWheel>();
        
        setVariables(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if(!IsFinished()){
            timerCount += Time.deltaTime;
            dormantTimer -= Time.deltaTime;

            wakeUpMachine();
            doDamage();
            switchState();
        }
    }

    public void setVariables()
    {
        selectTool();
        dormantTimer = 10f;
        hp = 95;
        _state = State.Dormant;
        isDormant = true;
        isFocused = false;
    }

    public void selectTool()
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

    public void repair()
    {
        if (tools.currentTool == requiredTool)
        {
            hp += 0.44f;
        }
        else
        {
            Debug.Log("wrong tool, active or dormant");
        }
    }

    public void doDamage()
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

    public void switchState()
    {
        if (hp >= 100 && isDormant == false)
        {
            _state = State.Active;
            hp = maxHp;
            damageCounter = 0;
            isActive = true;
            isDead = false;
            GameManagerScript.instance.CheckEnd();
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
            GameManagerScript.instance.CheckEnd();
        }
    }

    public void setDamageCounter()
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

    public void wakeUpMachine()
    {
        if (dormantTimer <= 0)
        {
            isDormant = false;
            dormantTimer = 0f;
        }
    }

    public void MouseCheck()
    {
        if(GameManagerScript.instance.openedRoom != transform.parent.gameObject)
            return;
        
        if (Input.GetMouseButton(0)){
            ray = GameManagerScript.instance.mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.GetRayIntersection ( ray );

            if (hit2D.collider != null)
            {
                if (hit2D.collider.tag == this.tag)
                {   
                    repair();
                }
            }
        }
    }

    public bool IsFinished(){
        return _state == State.Dead || _state == State.Active;
    }

    public bool IsSaved(){
        return _state == State.Active;
    }
}
