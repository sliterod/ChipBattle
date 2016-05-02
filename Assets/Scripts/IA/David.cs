using UnityEngine;
using System.Collections;
using System;

public class David : MonoBehaviour
{

    private CharacterControl characterControl;
    private Gamestate gamestate;

    private int xSteps;
    private int ySteps;
    private bool wallking;
    private bool canIAttackFlag;
    BattleState battleState;

    // Use this for initialization
    void Start()
    {
        initControl();
        initIa();
        this.canIAttackFlag = true;
        
    }

    void canIAttack()
    {
        if (this.canIAttackFlag)
        {
            this.canIAttackFlag = false;
        }
        else {
            this.canIAttackFlag = true;
        }
        
    }
    

    // Update is called once per frame
    void Update()
    {
        walkToTheFront();

        if (this.xSteps == 0)
        {
            //initXSteps();
        }

        if (this.ySteps == 0)
        {
            //initYSteps();
        }
    }

    void initControl()
    {
        gamestate = (Gamestate)GameObject.Find("Gamestate").GetComponent("Gamestate");
    }

    void initXSteps()
    {
        this.xSteps = 43;
    }

    void initYSteps()
    {
        this.ySteps = 83;
    }

    void initIa()
    {
        this.wallking = false;
        initXSteps();
        initYSteps();
    }

    bool weAreInBatlle()
    {
        battleState = BattleState.battle;
        if (battleState == gamestate.CurrentBattleState)
        {
            return true;
        }
        return false;
    }

    void walkToTheFront()
    {
        if (weAreInBatlle())
        {
            if (!this.wallking)
            {
                //Debug.Log(this.ySteps);
                InvokeRepeating("canIAttack()", 1, 0.3F);
                if (this.canIAttackFlag) {
                    Debug.Log(this.canIAttackFlag);
                    this.GetComponent<CharacterControl>().UseChip(1);
                }
                //this.GetComponent<CharacterControl>().UseChip(1);
                this.wallking = true;
                //while(this.xSteps>=0)
                if (this.xSteps > 0)
                {
                    gameObject.GetComponent<CharacterControl>().Move(0.0f, 1.0f);
                    this.xSteps--;

                }
                else if (this.xSteps == 0)
                {
                    this.xSteps = -43;
                }
                else if (this.xSteps < 0)
                {
                    gameObject.GetComponent<CharacterControl>().Move(1.0f, -1.0f);
                    this.xSteps++;
                }
                if (this.ySteps > 0)
                {
                    gameObject.GetComponent<CharacterControl>().Move(-1.0f, 0.0f);
                    this.ySteps--;
                }
                else if (this.ySteps == 0)
                {
                    this.ySteps = -83;
                }
                else if (this.ySteps < -2)
                {
                    gameObject.GetComponent<CharacterControl>().Move(1.0f, 1.0f);
                    this.ySteps++;
                }
                else if (this.ySteps == -2)
                {
                    this.ySteps = 83;
                }
                this.wallking = false;
            }
        }
    }
}
