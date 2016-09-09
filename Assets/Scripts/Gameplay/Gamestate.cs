﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Gamestate : MonoBehaviour {

    //Scripts
    BattleHud battleHud;                        //UI, used in battle to display HP, chips and custom bar
    BattleCardSelection battleCardSelection;    //UI, used to select battle cards

    //Enums
    BattleState currentBattleState;             //Current status of battle

    //Control variables
    bool isGamePaused;                          //Game is paused/running
    bool isOkButtonHighlighted;                 //OK button is Highlighted on selection screen
    bool isChipInfoActivated;

    public BattleState CurrentBattleState
    {
        get
        {
            return currentBattleState;
        }

        set
        {
            currentBattleState = value;
        }
    }

    // Use this for initialization
    void Awake () {      
        currentBattleState = BattleState.battleStart;

        InitializeHudObjects();
        InitializeEventListeners();

        SetupBattle();
    }
    
    /// <summary>
    /// Initializes listeners to capture changes on BattleState 
    /// </summary>
    void InitializeEventListeners() {
        StateListener.OnBattleStateChanged += OnBattlestateChange;
    }

    /// <summary>
    /// Initializes GameObjects containing HUD elements
    /// </summary>
    void InitializeHudObjects() {
        battleHud = GameObject.Find("HUD").GetComponent<BattleHud>();
        battleCardSelection = GameObject.Find("HUD").GetComponent<BattleCardSelection>();
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A)) {
            currentBattleState = BattleState.standby;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            currentBattleState = BattleState.selectionScreen;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            currentBattleState = BattleState.battle;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            currentBattleState = BattleState.results;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject.Find("P2Side").SendMessage("AreaSteal");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            GameObject.Find("P1Side").SendMessage("AreaSteal");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            GameObject.Find("P1Side").SendMessage("AreaStealRestore");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            GameObject.Find("P1Side").SendMessage("AreaStealRetrieve");
        }
        
    }

    /// <summary>
    /// Pauses the game. This changes current BattleState to "pause" or "battle"
    /// and sets TimeScale to 0 or 1 respectively.
    /// </summary>
    /// <param name="gamePaused">Bool to determinate if game was paused or must be resumed</param>
    void Pause(bool gamePaused) {
        if (gamePaused) {
            //TimeScale and state
            currentBattleState = BattleState.pause;
            Time.timeScale = 0.0f;
        }
        else if (!gamePaused) {
            //TimeScale and state
            currentBattleState = BattleState.battle;
            Time.timeScale = 1.0f;
        }

        //Show or Hide Pause UI
        battleHud.ShowPauseHud(gamePaused);
    }

    /// <summary>
    /// Setup battle before entering the first selection screen
    /// </summary>
    void SetupBattle() {
        StartCoroutine(RenderPlayers());
    }

    /// <summary>
    /// Renders each player GameObject and HP
    /// </summary>
    /// <returns></returns>
    IEnumerator RenderPlayers() {

        //First wait
        yield return new WaitForSeconds(1.0f);

        //Player 1 effect
        battleHud.ShowPlayerAndHp(Player.player1);
        yield return new WaitForSeconds(0.5f);

        //Player 2 effect
        battleHud.ShowPlayerAndHp(Player.player2);
        yield return new WaitForSeconds(0.5f);

        //Changing current gamestate
        currentBattleState = BattleState.selectionScreen;
    }

    /// <summary>
    /// Sends to BattleHud script to which direction the cursor should be moved
    /// </summary>
    /// <param name="cursorMovement"></param>
    public void MoveSelectionScreenCursor(Movement cursorMovement) {

        if (!isChipInfoActivated)
        {
            if (cursorMovement == Movement.up)
            {
                isOkButtonHighlighted = false;
            }
            else if (cursorMovement == Movement.down)
            {
                isOkButtonHighlighted = true;
            }

            battleHud.MoveSelectionScreenCursor(cursorMovement);
        }
    }

    /// <summary>
    /// Sets BattleHud selection screen cursor to OK button. If button is already selected
    /// a state change will occur.
    /// </summary>
    public void SetSelectionScreenCursorOK() {

        if (!isChipInfoActivated)
        {
            if (!isOkButtonHighlighted) {
                isOkButtonHighlighted = true;
                battleHud.MoveSelectionScreenCursor(Movement.down);
            }
            else if (isOkButtonHighlighted) {
                Debug.Log("Changing state");
                isOkButtonHighlighted = false;
                currentBattleState = BattleState.standby;

                battleHud.BattleGuidePosition();
            }
        }
    }

    /// <summary>
    /// Sets selected chip on the selection guide slot
    /// </summary>
    /// <param name="chipSlot">Button pressed:
    /// 1: A - key Q
    /// 2: B - key W
    /// 3: X - key E
    /// 4: Y - key R
    /// </param>
    public void SetChipSelectionScreen(int chipSlot) {
        battleHud.SelectionGuideSetChipSlot(chipSlot);
    }

    /// <summary>
    /// Destroys used chip miniature on battle guide slot
    /// </summary>
    /// <param name="chipSlot">Chip used:
    /// 1: A - key Q
    /// 2: B - key W
    /// 3: X - key E
    /// 4: Y - key R
    /// </param>
    public void DestroyChipBattleScreen(int chipSlot) {
        battleHud.BattleChipDestroyGuide(chipSlot);
    }

    /// <summary>
    /// Shows battle chip information
    /// </summary>
    public void DisplayChipInformation() {
        if (!isChipInfoActivated)
        {
            battleHud.ShowChipInfo(true);
        }
        else
        {
            battleHud.ShowChipInfo(false);
        }

        isChipInfoActivated = !isChipInfoActivated;
    }

    /// <summary>
    /// Shows StandBy bar UI
    /// </summary>
    void ChangeToStandBy() {

        //Stand by and selection screens
        battleHud.ShowSelectionScreen(false);
        battleHud.ShowStandBy(true);

        //Delete chips from selection screen
        battleHud.DestroySelectionBattleChips();

        //Set selected battle chips to guide

        //Send message to character controller
    }

    /// <summary>
    /// Changes BattleState from standby to battle
    /// </summary>
    void ChangeToBattle() {

        Time.timeScale = 1.0f;

        currentBattleState = BattleState.battle;
    }

    /// <summary>
    /// Triggers the activation of battle results screen after any player is defeated
    /// </summary>
    void ChangeToBattleEnd() {
        currentBattleState = BattleState.results;
    }

    /// <summary>
    /// Triggers the activation of battle exit message after a few seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator ChangeToRestart() {
        yield return new WaitForSeconds(3.0f);

        currentBattleState = BattleState.restart;
    }

    /// <summary>
    /// Reloads current scene
    /// </summary>
    public void ReloadScene() {
        StartCoroutine(ReloadSceneCoroutine());
    }

    /// <summary>
    /// Reloads current scene to restart battle
    /// </summary>
    /// <returns>Reloads scene</returns>
    IEnumerator ReloadSceneCoroutine() {
        yield return new WaitForSeconds(0.5f);

        //string sceneName = SceneManager.GetActiveScene().name;
        //string sceneName = "middle_load";
        string sceneName = "alpha_endload";
        Debug.Log(sceneName);

        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Executes a method after StateListener captures a change on currentBattleState variable
    /// </summary>
    /// <param name="newBattleState">The new BattleState value</param>
    void OnBattlestateChange(BattleState newBattleState) {
        Debug.Log("Changing state to: " + newBattleState.ToString());

        switch (newBattleState) {

            case BattleState.battleStart:
                Debug.Log("Battle initial setup");
                //Showing enemy name and area
                break;

            case BattleState.battle:
                Debug.Log("Starting battle");

                //Unpauses the game
                if (isGamePaused) {
                    isGamePaused = false;
                    Pause(isGamePaused);
                }

                //StandBy
                battleHud.ShowStandBy(false);

                //Battle
                battleHud.ShowCustomBar(true);
                battleHud.ShowBattleChipHelp(true);

                GameObject.Find("Gamestate").SendMessage("ActivateCustomGauge", true);
                break;

            case BattleState.selectionScreen:
                Debug.Log("Displaying Selection screen");
                battleHud.ShowCustomBar(false);
                battleHud.ShowBattleChipHelp(false);
                battleHud.ShowSelectionScreen(true);

                //Destroying chip guides
                DestroyChipBattleScreen(1);
                DestroyChipBattleScreen(2);
                DestroyChipBattleScreen(3);
                DestroyChipBattleScreen(4);

                //Refreshing chip list
                GameObject.Find("Folder").SendMessage("SetSelectionScreenChips");

                //Setting time to zero
                Time.timeScale = 0.0f;
                break;

            case BattleState.standby:
                Debug.Log("Standing by...");
                ChangeToStandBy();
                break;

            case BattleState.pause:
                Debug.Log("Game paused");
                isGamePaused = true;
                Pause(isGamePaused);
                break;

            case BattleState.results:
                //Stoping custom gauge
                GameObject.Find("Gamestate").SendMessage("ActivateCustomGauge", false);
                battleHud.ShowSelectionScreen(false);

                //Ending battle
                Debug.Log("Battle has ended, showing results screen");
                battleHud.ShowResultsScreen();

                //Changing to restart 
                StartCoroutine(ChangeToRestart());
                break;
        }
    }
}
