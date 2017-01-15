using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class David : MonoBehaviour
{


    private CharacterControl characterControl;
    private Gamestate gamestate;

    private int xSteps;
    private int ySteps;
    private bool wallking;
    private bool attaking;
    private int probability;
    private bool goToTopFlag;
    private bool goToButtonFlag;
    private bool minusMinusFlag;
    private bool plusPlusFlag;
    private bool minusPlusFlag;
    private bool plusMinusFlag;
    private bool canIAttackFlag;
    BattleState battleState;
    private GameObject player1;
    private GameObject player2;

    // Use this for initialization
    void Start()
    {
        initControl();
        initIa();
        this.canIAttackFlag = true;

    }

    // Update is called once per frame
    void Update()
    {
        walkToTheFront();
    }

    Boolean canIAttack()
    {
        if (this.attaking)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void initControl()
    {
        gamestate = (Gamestate)GameObject.Find("Gamestate").GetComponent("Gamestate");
        this.GetComponent<CharacterControl>().FlushChips();
        this.GetComponent<CharacterControl>().setChip("GrenadeChip", 1);
        this.GetComponent<CharacterControl>().setChip("GrenadeChip", 2);
        this.GetComponent<CharacterControl>().setChip("Cannon", 3);
        this.GetComponent<CharacterControl>().setChip("SkyBoxAttkChip", 4);
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
        this.attaking = false;
        this.probability = 0;
        this.goToTopFlag = true;
        this.goToButtonFlag = true;
        this.minusMinusFlag = true;
        this.plusPlusFlag = true;
        this.minusPlusFlag = true;
        this.plusMinusFlag = true;
        initXSteps();
        initYSteps();
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

    }
    void attack()
    {
    }
    void Dodge()
    {
    }
    bool weAreInBatlle()
    {
        battleState = BattleState.battle;
        gamestate = (Gamestate)GameObject.Find("Gamestate").GetComponent("Gamestate");
        if (battleState == gamestate.CurrentBattleState)
        {
            return true;
        }
        return false;
    }

    double getDisctance(GameObject object1, GameObject object2)
    {
        double distance = Vector3.Distance(object1.transform.position, object2.transform.position);
        return distance;

    }

    int calculateProbability()
    {
        System.Random rand = new System.Random();
        int value = rand.Next(1, 101);
        return value;
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("collide (name) : " + col.collider.gameObject.name);
        /*if (col.gameObject.name == "Wall_Back")
        {
            this.wallking = false;
        }*/
    }

    public bool amIInPosition(float coordinateX, float coordinateZ, float positionX, float positionZ)
    {
        if ((Math.Truncate(coordinateX) == Math.Truncate(positionX)) && (Math.Truncate(coordinateZ) == Math.Truncate(positionZ)))
        {
            return true;

        }
        return false;
    }

    void minusMinus()
    {
        gameObject.GetComponent<CharacterControl>().Move(-1.0f, -1.0f);
    }
    void plusPlus()
    {
        gameObject.GetComponent<CharacterControl>().Move(1.0f, 1.0f);
    }
    void plusMinus()
    {
        gameObject.GetComponent<CharacterControl>().Move(1.0f, -1.0f);
    }
    void minusPlus()
    {
        gameObject.GetComponent<CharacterControl>().Move(-1.0f, 1.0f);
    }
    void stop()
    {
        gameObject.GetComponent<CharacterControl>().Move(0.0f, 0.0f);
    }

    bool goToPosition(float coordinateX, float coordinateZ, float positionX, float positionZ)
    {
        if (!amIInPosition(coordinateX, coordinateZ, positionX, positionZ))
        {
            if ((coordinateX > positionX) && (coordinateZ > positionZ))
            {
                plusPlus();
            }
            if ((coordinateX < positionX) && (coordinateZ < positionZ))
            {
                minusMinus();
            }
            if ((coordinateX > positionX) && (coordinateZ < positionZ))
            {
                plusMinus();
            }
            if ((coordinateX < positionX) && (coordinateZ > positionZ))
            {
                minusPlus();
            }
            this.wallking = true;
            return false;
        }
        else
        {
            this.stop();
            this.wallking = false;
            return true;
        }
    }

    void goToTop()
    {
        if (!this.wallking)
        {
            //this.wallking = true;
            //goToPosition(4, 4, gameObject.transform.position.x, gameObject.transform.position.z);
        }
    }

    void walkToTheFront()
    {
        if (weAreInBatlle())
        {
            if ((!this.wallking) && (!this.attaking))
            {
                this.probability = this.calculateProbability();
            }
            if ((Math.Truncate(this.player1.transform.position.z) ) == Math.Truncate(this.player2.transform.position.z))
            {
                if (this.canIAttack())
                {
                    if (this.probability >= 15)
                    {
                        System.Random rand = new System.Random();
                        int value = rand.Next(1, 3);

                        this.GetComponent<CharacterControl>().UseChip(value);
                        this.attaking = true;
                    }
                    else {
                        if ((this.player2.transform.position.x * -1)>-3) {
                            goToPosition((this.player2.transform.position.x * -1) - 4, this.player1.transform.position.z * 1, this.player2.transform.position.x, this.player2.transform.position.z);
                        }
                        else if((this.player2.transform.position.x * -1)> -7) {
                            goToPosition((this.player2.transform.position.x * -1) + 1, this.player1.transform.position.z * 1, this.player2.transform.position.x, this.player2.transform.position.z);
                        }
                        
                    }
                }
            }else
            {
                if (this.probability >= 35)
                {
                    if (this.player1.transform.position.x < 7)
                    {
                        if (this.probability >= 85)
                        {
                            if (this.player2.transform.position.x<-7) {
                                goToPosition(this.player2.transform.position.x + 4, this.player1.transform.position.z, this.player2.transform.position.x, this.player2.transform.position.z);
                            } else {
                                goToPosition(this.player2.transform.position.x - 1, this.player1.transform.position.z, this.player2.transform.position.x, this.player2.transform.position.z);
                            }
                            
                        }
                        else {
                            if (this.player1.transform.position.z<1) {
                                goToPosition((this.player1.transform.position.x * -1) + 4, this.player1.transform.position.z + 3, this.player2.transform.position.x, this.player2.transform.position.z);
                            }
                            else {
                                goToPosition((this.player1.transform.position.x * -1) + 4, this.player1.transform.position.z + 5, this.player2.transform.position.x, this.player2.transform.position.z);
                            }
                            
                        }
                            
                    }
                    else {
                        goToPosition(this.player1.transform.position.x * -1, this.player1.transform.position.z * 1, this.player2.transform.position.x, this.player2.transform.position.z);
                    }
                        
                }
                    else if (this.probability < 35)
                    {
                        goToPosition(this.player1.transform.position.x*-1, (this.player1.transform.position.z * -1), this.player2.transform.position.x, this.player2.transform.position.z);
                    }
            }
            this.attaking = false;
        }
    }
}