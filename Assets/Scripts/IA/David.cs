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
    private bool reloadChips;
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
        else {
            return true;
        }
    }

    void initControl()
    {
        gamestate = (Gamestate)GameObject.Find("Gamestate").GetComponent("Gamestate");
        if (this.reloadChips) {
            this.GetComponent<CharacterControl>().FlushChips();
            this.GetComponent<CharacterControl>().setChip("GrenadeChip", 1);
            this.GetComponent<CharacterControl>().setChip("GrenadeChip", 2);
            this.GetComponent<CharacterControl>().setChip("Cannon", 3);
            //this.GetComponent<CharacterControl>().setChip("Heal150Chip", 4);
            this.GetComponent<CharacterControl>().setChip("SkyBoxAttkChip", 4);
            this.reloadChips = false;
        }
        
    }
    void reloadIaChips() {
        if (this.reloadChips)
        {
            this.GetComponent<CharacterControl>().FlushChips();
            this.GetComponent<CharacterControl>().setChip("GrenadeChip", 1);
            this.GetComponent<CharacterControl>().setChip("GrenadeChip", 2);
            this.GetComponent<CharacterControl>().setChip("Cannon", 3);
            //this.GetComponent<CharacterControl>().setChip("Heal300Chip", 4);
            this.GetComponent<CharacterControl>().setChip("SkyBoxAttkChip", 4);
            this.reloadChips = false;
        }
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
        this.reloadChips = true;
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
        gamestate = (Gamestate)GameObject.Find("Gamestate").GetComponent("Gamestate");
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
    void stop() {
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
        else {
            this.stop();
            this.wallking = false;
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
        int bigDesition = 0;

        if (weAreInBatlle())
        {
            this.reloadChips = true;
            if ((!this.wallking) && (!this.attaking))
            {
                this.probability = this.calculateProbability();
                bigDesition = this.calculateProbability();
            }
            if (bigDesition > 50)
            {
                if (this.probability <= 50) {
                    if (Math.Truncate(this.player1.transform.position.z * -1) == Math.Truncate(gameObject.transform.position.z))
                    {
                        if (this.canIAttack())
                        {
                            this.GetComponent<CharacterControl>().UseChip(4);
                            this.attaking = true;
                        }
                    }
                }
                else if (this.probability <= 100)
                {
                        if (Math.Truncate(this.player1.transform.position.z * -1) == Math.Truncate(gameObject.transform.position.z))
                        {
                            if (this.canIAttack())
                            {
                                this.GetComponent<CharacterControl>().UseChip(4);
                                this.attaking = true;
                            }
                        }
                    
                }
            }
            else
            {
                if (this.canIAttack())
                {
                    if (this.probability < 50)
                    {
                        if (this.getDisctance(this.player1, this.player2) > 5)
                        {
                            goToPosition(this.player1.transform.position.x * -1, this.player1.transform.position.z * 1, gameObject.transform.position.x, gameObject.transform.position.z);
                        }
                        else if (this.getDisctance(this.player1, this.player2) <= 5)
                        {
                            goToPosition(this.player1.transform.position.x * 1, this.player1.transform.position.z * -1, gameObject.transform.position.x, gameObject.transform.position.z);
                        }
                    }
                    else if (this.probability <= 100)
                    {
                        goToPosition(this.player1.transform.position.x * -1, this.player1.transform.position.z * 1, gameObject.transform.position.x, gameObject.transform.position.z);

                    }

                }

            }
            this.attaking = false;
        }
        else {
            reloadIaChips();
        }
    }
}
