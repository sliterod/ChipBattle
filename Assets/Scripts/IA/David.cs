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

    void initControl()
    {
        gamestate = (Gamestate)GameObject.Find("Gamestate").GetComponent("Gamestate");
        this.GetComponent<CharacterControl>().FlushChips();
        this.GetComponent<CharacterControl>().setChip("GrenadeChip", 1);
        this.GetComponent<CharacterControl>().setChip("GrenadeChip", 2);
        this.GetComponent<CharacterControl>().setChip("Cannon", 3);
        this.GetComponent<CharacterControl>().setChip("Cannon", 4);
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
    void attack() {
    }
    void Dodge() {
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

    double getDisctance(GameObject object1, GameObject object2) {
        double distance = Vector3.Distance(object1.transform.position, object2.transform.position);
        return distance;

    }

    int calculateProbability() {
        System.Random rand = new System.Random();
        int value=rand.Next(1, 101);
        return value;
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("collide (name) : " + col.collider.gameObject.name);
        if (col.gameObject.name == "Wall_Back")
        {
            this.wallking = false;
        }
    }

    public bool amIInPosition(float coordinateX, float coordinateZ, float positionX, float positionZ)
    {
        if ((Math.Truncate(coordinateX) == Math.Truncate(positionX)) && (Math.Truncate(coordinateZ) == Math.Truncate(positionZ)))
        {
            return true;

        }
        return false;
    }

    void minusMinus() {
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
            this.wallking = false;
            return false;
        }
        else {
            this.wallking = true;
                return true;
            }
    }
    
    void goToTop() {
        if (!this.wallking) {
            //this.wallking = true;
            //goToPosition(4, 4, gameObject.transform.position.x, gameObject.transform.position.z);
        }
    }

    void walkToTheFront()
    {
        //Debug.Log("Position x: "+ gameObject.transform.position.x+" Position z: "+ gameObject.transform.position.z);
        if (weAreInBatlle())
        {
            if ((!this.wallking) &&(!this.attaking)) {
                this.probability=this.calculateProbability();
            }
            //Debug.Log("Distance between player 1 and player 2: "+ this.getDisctance(this.player1, this.player2));
            if (this.getDisctance(this.player1, this.player2) > 11)
            {
                if (this.probability >= 40) {
                    goToPosition(1, this.player1.transform.position.z*-1, gameObject.transform.position.x, gameObject.transform.position.z);
                }
                else if (this.probability >= 1) {
                    if (goToPosition(9, this.player1.transform.position.z, gameObject.transform.position.x, gameObject.transform.position.z)) {
                        this.GetComponent<CharacterControl>().UseChip(1);
                    }
                    
                }
            }
            else {
                if (this.probability >= 70)
                {
                    if (goToPosition(9, this.player1.transform.position.z, gameObject.transform.position.x, gameObject.transform.position.z)) {
                        this.GetComponent<CharacterControl>().UseChip(1);
                    }
                }
                else if (this.probability >= 40)
                {
                    goToPosition(0, 9, gameObject.transform.position.x, gameObject.transform.position.z);
                }
                else if (this.probability >= 1)
                {
                    if (goToPosition(this.player1.transform.position.x * -1, this.player1.transform.position.z, gameObject.transform.position.x, gameObject.transform.position.z)) {
                        this.GetComponent<CharacterControl>().UseChip(2);
                    }

                }
            }
            
            
            /*if (!this.wallking)
            {*/
            //Debug.Log(this.ySteps);
            //InvokeRepeating("canIAttack()", 1, 0.3F);
            //   if (this.canIAttackFlag) {
            //Debug.Log(this.canIAttackFlag);
            //this.GetComponent<CharacterControl>().UseChip(1);
            // }
            // Debug.Log(this.getDisctance(this.player1,this.player2));
            //this.GetComponent<CharacterControl>().UseChip(1);
            //this.wallking = true;
            //while(this.xSteps>=0)
            /*if (this.xSteps > 0)
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
        }*/
        }
    }
    
}
