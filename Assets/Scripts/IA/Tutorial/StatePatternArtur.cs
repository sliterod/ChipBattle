using UnityEngine;
using System.Collections;

public class StatePatternArtur : MonoBehaviour 
{
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
    private Gamestate gamestate;
    public float searchingTurnSpeed = 120f;
    public float searchingDuration = 4f;
    public float sightRange = 20f;
    public Vector3 offset = new Vector3 (0,.5f,0);
    public Transform eyes;
    public MeshRenderer meshRendererFlag;


    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public PatrolState patrolState;

    private void Awake()
    {
        chaseState = new ChaseState (this);
        alertState = new AlertState (this);
        patrolState = new PatrolState (this);
        meshRendererFlag.GetComponent<Renderer>().enabled = true;
        this.eyes.GetComponent<sensor>().parent = this;
        this.initIa();
    }

    // Use this for initialization
    void Start () 
    {
        currentState = patrolState;
    }
    
    // Update is called once per frame
    void Update () 
    {
        currentState.UpdateState ();
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter (other);
    }
    

    void initControl()
    {
        gamestate = (Gamestate)GameObject.Find("Gamestate").GetComponent("Gamestate");
        this.GetComponent<CharacterControl>().FlushChips();
        this.GetComponent<CharacterControl>().setChip("GrenadeChip", 1);
        this.GetComponent<CharacterControl>().setChip("GrenadeChip", 2);
        this.GetComponent<CharacterControl>().setChip("Cannon", 3);
        //this.GetComponent<CharacterControl>().setChip("SkyBoxAttkChip", 4);
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
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        this.initControl();
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
    public void ChildCollisionEnter(Collider other){
        currentState.OnTriggerEnter (other);
        Debug.Log("Pener gefunden!!!");
    }
}