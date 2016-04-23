using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour {

    //General objects
    public GameObject battleCustomBar;
    public GameObject battleChipHelp;
    public GameObject battleAreaDescription;
    public GameObject battlePause;
    public GameObject battleStandBy;
    Text battleStandByText;

    public RectTransform selectionScreen;
    public GameObject selectionEnemies;
    RectTransform selectionCursor;
    RectTransform selectionOk;
    int selCursorIndex;

    public GameObject resultsScreen;

    //Control booleans
    bool isScreenInitialized;
    bool isCursorMoved;
    bool isStandbyTimerOffline;
    bool isResultsTimerOffline;

    //General variables
    float[] cursorPositionsArray;
    float standByTimer;

    void Update() {
        if (isStandbyTimerOffline)
            StandByMessageTimer();

        if (Input.GetKeyDown(KeyCode.M)) {
            GameObject cannon = Instantiate(Resources.Load("Cannon", typeof(GameObject))) as GameObject;
            
            cannon.transform.parent = selectionScreen.transform;

            //Scale
            cannon.transform.localScale = Vector3.one;

            //Position
            cannon.transform.localPosition = Vector3.zero;
        }
    }


    /// <summary>
    /// Shows current position of chips on controller
    /// </summary>
    /// <param name="state"></param>
    public void ShowBattleChipHelp(bool state) {
        if (state)
        {
            iTweenEvent.GetEvent(battleChipHelp.gameObject, "Show").Play();
        }
        else {
            iTweenEvent.GetEvent(battleChipHelp.gameObject, "Hide").Play();
        }
    }

    /// <summary>
    /// Shows Chip selection screen UI
    /// </summary>
    /// <param name="state">State to indicate if the bar should be shown or not</param>
    public void ShowSelectionScreen(bool state) {
        if (state)
        {
            Debug.Log("Showing selection screen, setting state to selectionScreen...");

            //Player HP
            GameObject.Find("Player1").SendMessage("RenderHpCanvas", false);

            //Selection screen
            selectionScreen.gameObject.SetActive(true);
            InitializeSelectionScreenCursor();                
            
            iTweenEvent.GetEvent(selectionScreen.gameObject, "Show").Play();
        }
        else {
            Debug.Log("Hiding selection screen, setting state to standby...");

            //Selection screen
            isScreenInitialized = false;
            iTweenEvent.GetEvent(selectionScreen.gameObject, "Hide").Play();
        }
    }

    /// <summary>
    /// Moves the selection screen cursor to choose a chip
    /// </summary>
    /// <param name="cursorMovement">Direction where the cursor must be moved</param>
    public void MoveSelectionScreenCursor(Movement cursorMovement) {

        switch (cursorMovement) {
            case Movement.left:
                if (!isCursorMoved)
                {
                    isCursorMoved = true;
                    StartCoroutine(ChangeCursorPosition(-1));
                }
                break;

            case Movement.right:
                if (!isCursorMoved)
                {
                    isCursorMoved = true;
                    StartCoroutine(ChangeCursorPosition(1));
                }
                break;

            case Movement.down:
                selectionCursor.gameObject.SetActive(false);
                selectionOk.gameObject.SetActive(true);
                break;

            case Movement.up:
                selectionCursor.gameObject.SetActive(true);
                selectionOk.gameObject.SetActive(false);
                break;
        }

    }

    /// <summary>
    /// Changes selection screen cursor position
    /// </summary>
    /// <param name="newCursorIndex">New cursor index value</param>
    /// <returns>New cursor position</returns>
    IEnumerator ChangeCursorPosition(int newCursorIndex) {

        int posArraySize = cursorPositionsArray.Length - 1;

        selCursorIndex = selCursorIndex + newCursorIndex;

        if (selCursorIndex < 0) {
            selCursorIndex = posArraySize;
        }
        else if (selCursorIndex > posArraySize) {
            selCursorIndex = 0;
        }

        selectionCursor.anchoredPosition = new Vector2(cursorPositionsArray[selCursorIndex],
                                                        selectionCursor.anchoredPosition.y);

        yield return new WaitForSeconds(0.25f);

        isCursorMoved = false;
    }

    /// <summary>
    /// Initializes the object and the positions of selection screen cursor
    /// </summary>
    void InitializeSelectionScreenCursor() {
       
        if (!isScreenInitialized)
        {
            cursorPositionsArray = new float[] { 18.0f, 118.0f, 218.0f, 318.0f, 418.0f, 518.0f };
            selectionCursor = (RectTransform)selectionScreen.FindChild("selection_cursor");
            selectionOk = (RectTransform)selectionScreen.FindChild("selection_ok");

            isScreenInitialized = true;
        }

        //Index
        selCursorIndex = 0;

        //Cursors
        selectionOk.gameObject.SetActive(false);
        selectionCursor.gameObject.SetActive(true);

        //Cursor position
        selectionCursor.anchoredPosition = new Vector2( cursorPositionsArray[selCursorIndex],
                                                        selectionCursor.anchoredPosition.y);
        
    }

    /// <summary>
    /// Shows custom bar UI
    /// </summary>
    /// <param name="state">State to indicate if the bar should be shown or not</param>
    public void ShowCustomBar(bool state) {
        if (state)
        {
            Debug.Log("Showing custom bar");
            iTweenEvent.GetEvent(battleCustomBar, "Show").Play();
            battleCustomBar.SendMessage("ActivateGaugeFilling", true);
        }
        else
        {
            Debug.Log("Hiding custom bar");
            iTweenEvent.GetEvent(battleCustomBar, "Hide").Play();
            battleCustomBar.SendMessage("ResetGaugeFilling", true);
        }
    }

    /// <summary>
    /// Shows StandBy UI
    /// </summary>
    /// <param name="state">State to indicate if the stanby bar should be shown or not</param>
    public void ShowStandBy(bool state) {
        battleStandBy.SetActive(state);

        if (state)
        {
            //Player HP
            GameObject.Find("Player1").SendMessage("RenderHpCanvas", true);

            //Stand By
            battleStandByText = battleStandBy.transform
                .FindChild("standby_text")
                .GetComponent<Text>();

            standByTimer = 3.0f;
            isStandbyTimerOffline = true;
        }
    }

    /// <summary>
    /// Change the message of the StandBy UI message
    /// </summary>
    void StandByMessageTimer() {
        if (standByTimer >= 1.0f) {
            standByTimer -= Time.deltaTime;
            SetStandByMessage(standByTimer.ToString("0.00"));
        }
        else if (standByTimer < 1.0f)
        {
            standByTimer = 0.0f;
            isStandbyTimerOffline = false;

            SetStandByMessage("BATTLE START");
        }
    }

    /// <summary>
    /// Sets StandBy bar message
    /// </summary>
    /// <param name="message">The message to display on the bar</param>
    public void SetStandByMessage(string message) {
        battleStandByText.text = message;
    }

    /// <summary>
    /// Shows pause screen
    /// </summary>
    /// <param name="state">Current state of pause</param>
    public void ShowPauseHud(bool state) {

        //Battle Hud
        ShowCustomBar(!state);
        ShowBattleChipHelp(!state);

        //Pause Screen
        battlePause.SetActive(state);
    }

    /// <summary>
    /// Renders the player gameobject and its current HP
    /// </summary>
    public void ShowPlayerAndHp(Player playerToRender) {

        if (playerToRender == Player.player1) {

            GameObject player = GameObject.Find("Player1").transform.FindChild("Mesh").gameObject;
            player.SetActive(true);
            //player.gameObject.GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Player1").SendMessage("EnableHpUpdate");
        }
        else if (playerToRender == Player.player2 || playerToRender == Player.ai) {

            GameObject player = GameObject.Find("Player2").transform.FindChild("Mesh").gameObject;

            player.SetActive(true);
            //player.gameObject.GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Player2").SendMessage("EnableHpUpdate");
        }
    }

    /// <summary>
    /// Shows Results Screen UI
    /// </summary>
    public void ShowResultsScreen() {
        StartCoroutine(ResultsScreen());
    }

    /// <summary>
    /// Starts a coroutine to show standby bar and results screen UI elements
    /// </summary>s
    IEnumerator ResultsScreen()
    {
        ShowCustomBar(false);
        ShowBattleChipHelp(false);

        battleStandBy.SetActive(true);
        SetStandByMessage("BATTLE FINISHED");

        yield return new WaitForSeconds(2.0f);

        battleStandBy.SetActive(false);

        ResultsScreenTween(true);
    }

    /// <summary>
    /// Triggers results screen iTween
    /// </summary>
    /// <param name="state">Indicates which tween should be activated</param>
    public void ResultsScreenTween(bool state) {
        if (state)
        {
            resultsScreen.gameObject.SetActive(state);
            iTweenEvent.GetEvent(resultsScreen.gameObject, "Show").Play();
        }
        else {
            iTweenEvent.GetEvent(resultsScreen.gameObject, "Hide").Play();
        }
    }
}
