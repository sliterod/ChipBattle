using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class BattleHud : MonoBehaviour {

    /******************** CHIP VALUES **********************/
    ChipDictionary chipDictionary;

    /******************** GENERAL OBJECT **********************/
    public GameObject battleCustomBar;
    public GameObject battleChipHelp;
    public GameObject battleAreaDescription;
    public GameObject battlePause;
    public GameObject battleStandBy;
    public GameObject resultsScreen;
    Text battleStandByText;

    /******************** SELECTION SCREEN **********************/
    public RectTransform selectionScreen;
    public GameObject selectionChipInfo;
    public GameObject selectionOpponent;
    public Transform selectionScreenMiniatures;
    public Transform selectionScreenGuide;
    RectTransform selectionCursor;
    RectTransform selectionOk;
    AudioSource selectionCursorAutdio;
    AudioSource selectionOkAutdio;
    int selCursorIndex;

    /******************** CONTROL BOOLEANS **********************/
    bool isScreenInitialized;
    bool isCursorMoved;
    bool isStandbyTimerOffline;
    bool isResultsTimerOffline;

    /******************** GENERAL VARIABLES **********************/
    float[] cursorPositionsArray;
    float standByTimer;
    float realTimeStamp;

    /************************ FOLDER ****************************/
    List<Transform> selScreenChips;                     //Chip images. Toggled on/off according to cursor position
    List<Transform> selScreenMiniatures;                //Chip images as miniatures. Shows current available chips
    Transform[,] selScreenGuides;                       //Chip images as miniatures. Indicates a chip is selected and former parent
    string[] selScreenChipNames;                        //Random selected chips from current equipped folder
    int selScreenChipIndex;

    float[] selMiniaturePositions;
    float[] selGuidePositions;
    float[] battleGuidePositions;

    /************************************************************/

    void Awake()
    {
        chipDictionary = GameObject
                    .Find("Dictionary")
                    .GetComponent<ChipDictionary>();
    }

    void Update() {
        if (isStandbyTimerOffline)
            StandByMessageTimer();
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
    /// Shows chip selection screen UI
    /// </summary>
    /// <param name="state">State to indicate if the bar should be shown or not</param>
    public void ShowSelectionScreen(bool state) {
        if (state)
        {
            Debug.Log("Showing selection screen, setting state to selectionScreen...");

            //Player HP
            GameObject.Find("Player1").SendMessage("RenderHpCanvas", false);

            //Selection checkmarks
            ResetCheckmarkScale();

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
                    ChangeCursorPosition(-1);
                }
                selectionCursorAutdio.Play();
                selectionCursorAutdio.PlayDelayed(0);
                break;

            case Movement.right:
                if (!isCursorMoved)
                {
                    isCursorMoved = true;
                    ChangeCursorPosition(1);
                }
                selectionCursorAutdio.Play();
                selectionCursorAutdio.PlayDelayed(0);
                break;

            case Movement.down:
                if (!selectionOk.gameObject.active)
                {
                    selectionCursorAutdio.Play();
                    selectionCursorAutdio.PlayDelayed(0);
                    selectionCursor.gameObject.SetActive(false);
                    selectionOk.gameObject.SetActive(true);
                }
                break;

            case Movement.up:
                if (selectionOk.gameObject.active)
                {
                    selectionCursor.gameObject.SetActive(true);
                    selectionOk.gameObject.SetActive(false);
                    selectionCursorAutdio.Play();
                    selectionCursorAutdio.PlayDelayed(0);
                }
                break;
        }

    }

    /// <summary>
    /// Changes selection screen cursor position
    /// </summary>
    /// <param name="newCursorIndex">New cursor index value</param>
    void ChangeCursorPosition(int newCursorIndex) {

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

        ShowCurrentChip(selCursorIndex);

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
            selectionCursorAutdio = selectionScreen.GetComponent<AudioSource>();
            selectionOkAutdio = selectionScreen.FindChild("selection_ok").GetComponent<AudioSource>();
            isScreenInitialized = true;
        }

        //Index
        selCursorIndex = 0;

        //Cursors
        selectionOk.gameObject.SetActive(false);
        selectionCursor.gameObject.SetActive(true);

        //Cursor position
        selectionCursor.anchoredPosition = new Vector2(cursorPositionsArray[selCursorIndex],
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
            selectionOkAutdio.gameObject.SetActive(true);
            selectionOkAutdio.Play();
            selectionOkAutdio.Play(44100);
            //Player HP
            GameObject.Find("Player1").SendMessage("RenderHpCanvas", true);

            //Stand By
            battleStandByText = battleStandBy.transform
                .FindChild("standby_text")
                .GetComponent<Text>();

            standByTimer = 1.0f;
            realTimeStamp = Time.realtimeSinceStartup;
            isStandbyTimerOffline = true;
        }
    }

    /// <summary>
    /// Change the message of the StandBy UI message
    /// </summary>
    /// <returns></returns>
    void StandByMessageTimer() {

        float timeNow = Time.realtimeSinceStartup;
        float difference = timeNow - realTimeStamp;

        if (standByTimer > difference) {
            float displayTimer = standByTimer - difference;
            SetStandByMessage(displayTimer.ToString("0.00"));
        }
        else if (standByTimer < difference)
        {
            if (difference < 2.0f)
            {
                SetStandByMessage("BATTLE START");
            }
            else if (difference > 2.0f) {

                standByTimer = 0.0f;
                realTimeStamp = 0.0f;
                isStandbyTimerOffline = false;

                //Returns to battle
                GameObject.Find("Gamestate").SendMessage("ChangeToBattle");
            }

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

        yield return new WaitForSeconds(3.0f);

        ResultScreenExitMessage();
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

    /// <summary>
    /// Displays result screen exit message
    /// </summary>
    public void ResultScreenExitMessage() {
        RectTransform exitMessage;

        exitMessage = (RectTransform) resultsScreen
                        .transform
                        .FindChild("exit");

        exitMessage.localScale = Vector3.one;
    }

    /**********************************************************
                            BATTLE CHIPS
    **********************************************************/

    /// <summary>
    /// Sets the first six chips on list
    /// and shows their images
    /// </summary>
    /// <param name="chipArray">String array with selected chips after random</param>
    public void SetChipList(string[] chipArray) {
        selScreenChipNames = chipArray;
        selScreenMiniatures = new List<Transform>();
        selScreenChips = new List<Transform>();
        selScreenGuides = new Transform[4, 2];

        Debug.Log("Current random selection set. Now instantiating chips");
        for (int i = 0; i < selScreenChipNames.Length; i++)
        {
            InstantiateBattleChip(selScreenChipNames[i]);
        }

        //Filling empty spaces (if any)
        FillEmptyChipSlot(selScreenChipNames.Length);

        Debug.Log("Setting miniature positions");
        SelectionMiniaturePosition();

        Debug.Log("Deactivating other chip instances");
        ShowCurrentChip(0);
    }

    /// <summary>
    /// Sets the first six chips on list and shows their images.
    /// Override, only works if list is already empty
    /// </summary>
    public void SetChipList() {
        selScreenMiniatures = new List<Transform>();
        selScreenChips = new List<Transform>();
        selScreenGuides = new Transform[4, 2];

        Debug.Log("Random selection empty. Now instantiating chips");
        FillEmptyChipSlot(0);

        Debug.Log("Setting miniature positions");
        SelectionMiniaturePosition();

        Debug.Log("Deactivating other chip instances");
        ShowCurrentChip(0);
    }

    /// <summary>
    /// Fills with a No Data image every empty slot
    /// </summary>
    /// <param name="chipListLength">Current amount of chips on folder</param>
    void FillEmptyChipSlot(int chipListLength) {

        int emptySlots; //How many slots are empty

        //Filling empty spaces
        if (chipListLength < 6)
        {
            Debug.Log("Current chips on folder: " + chipListLength + ". Filling empty spaces...");
            emptySlots = 6 - chipListLength;

            for (int i = 0; i < emptySlots; i++)
            {
                InstantiateBattleChip("NoData");
            }
        }
    }

    /// <summary>
    /// Instantiates a battle chip to the selection screen
    /// </summary>
    /// <param name="chipName">Name of the prefab containing the chip</param>
    void InstantiateBattleChip(string chipName)
    {
        string path = "ChipsUI/" + chipName;
        Transform chip = Instantiate(Resources.Load(path, typeof(Transform))) as Transform;

        chip.SetParent(selectionScreen.transform);

        //Scale
        chip.localScale = Vector3.one;

        //Chip Position
        chip.localPosition = Vector3.zero;

        //Chip name
        chip.name = chipName;

        //Chip data
        if (chipName != "NoData") {
            SetChipData(chip);
        }

        //Saving chips
        selScreenChips.Add(chip);

        //Saving miniatures
        if (chip.FindChild("chip_miniature"))
        {
            selScreenMiniatures.Add(chip.transform.FindChild("chip_miniature"));
        }
    }

    /// <summary>
    /// Sets atk and effect data on chip according to the dictionary
    /// </summary>
    /// <param name="chip"></param>
    void SetChipData(Transform chip) {

        Text chipDamageText;
        ChipData chipData;

        //Text 
        chipDamageText = chip.FindChild("stats_atk/atk_value")
                         .GetComponent<Text>();

        //Dictionary values
        chipData = chipDictionary.GetChipAttributes(chip.name);
        
        //Setting text on UI
        //Damage
        if (chipData.Damage > 0)
        {
            chipDamageText.text = chipData.Damage.ToString();
        }
        else
        {
            chipDamageText.text = "- - -";
        }
        
        //Element or effect

    }

    /// <summary>
    /// Destroy selection screen battle chips
    /// </summary>
    public void DestroySelectionBattleChips() {
        Debug.Log("Chips on selScreenChips list: " + selScreenChips.Count);
        //Destroying selection screen battle chip list
        foreach (Transform chip in selScreenChips) {
            Destroy(chip.gameObject);
        }

        selScreenChips.RemoveRange(0, selScreenChips.Count);
        Debug.Log("Chips on selScreenChips list: " + selScreenChips.Count);

        //Destroying selection screen miniatures
        foreach (Transform miniature in selScreenMiniatures)
        {
            Destroy(miniature.gameObject);
        }

        selScreenMiniatures.RemoveRange(0, selScreenMiniatures.Count);
    }

    /// <summary>
    /// Shows chip image according to cursor's current position
    /// </summary>
    /// <param name="position">Current cursor position (index)</param>
    void ShowCurrentChip(int position) {

        //Turning off chips
        foreach (Transform chip in selScreenChips) {
            chip.localScale = Vector3.zero;
        }

        //Turning on current chip
        if (position < selScreenChips.Count)
        {
            selScreenChips[position].localScale = Vector3.one;
        }
    }

    /// <summary>
    /// Sets the position of the miniatures on the selection screen
    /// </summary>
    void SelectionMiniaturePosition() {

        for (int i = 0; i < selScreenMiniatures.Count; i++) {

            //Parent
            selScreenMiniatures[i].SetParent(selectionScreenMiniatures);

            //Position
            selScreenMiniatures[i].localPosition = selectionScreenMiniatures
                                                        .Find("cards_" + (i + 1))
                                                        .localPosition;

            //Scale
            selScreenMiniatures[i].localScale = Vector3.one;
        }
    }

    /// <summary>
    /// Sets the chip guide on a button slot
    /// </summary>
    /// <param name="button">Integer to indicate current button pressed:
    /// 1: Button A - Key Q
    /// 2: Button B - Key W
    /// 3: Button X - Key E
    /// 4: Button Y - Key R
    /// </param>
    public void SelectionGuideSetChipSlot(int button) {

        string buttonGuide = "guide_button";
        int guideIndex = 0;

        //Current button
        switch (button) {
            case 1:
                buttonGuide = buttonGuide + "A";
                guideIndex = 0;
                break;

            case 2:
                buttonGuide = buttonGuide + "B";
                guideIndex = 1;
                break;

            case 3:
                buttonGuide = buttonGuide + "X";
                guideIndex = 2;
                break;

            case 4:
                buttonGuide = buttonGuide + "Y";
                guideIndex = 3;
                break;
        }

        //Changing parent
        SelectionGuideChangeChipParent(buttonGuide, guideIndex);
    }

    /// <summary>
    /// Fills the chip guide slot
    /// </summary>
    /// <param name="chipGuide">Chip guide transform to be relocated</param>
    /// <param name="parentName">New parent for the chip guide</param>
    /// <param name="chipGuideIndex">Index to save chip guide on array (to be used on battle)</param>
    void SelectionGuideFillSlot(Transform chipGuide,
                                    string parentName,
                                    int chipGuideIndex)
    {
        //Setting current chip to selected guide position
        Debug.Log("Setting chip guide to " + parentName);

        if (chipGuide)
        {
            //Saving on array
            Debug.Log("Saving chip guide to array");
            selScreenGuides[chipGuideIndex, 0] = chipGuide; //Chip
            Debug.Log("ChipGuideIndex: " + chipGuideIndex);
            selScreenGuides[chipGuideIndex, 1] = chipGuide.parent; //Original Parent

            //New Parent
            chipGuide.SetParent(selectionScreenGuide.FindChild(parentName));

            //New Position
            chipGuide.localPosition = new Vector3(61.0f, 0.0f);

            //New Scale
            chipGuide.localScale = Vector3.one;
        }
    }

    /// <summary>
    /// Changes the parent of the chip guide if:
    /// *There is no chip on current position
    /// *There is a chip on current position
    /// </summary>
    /// <param name="parentName">New parent for the chip guide</param>
    /// <param name="chipGuideIndex">Index to save chip guide on array (to be used on battle)</param>
    void SelectionGuideChangeChipParent(string parentName, int chipGuideIndex)
    {
        Transform chipGuideTransform;
        string chipGuideName = "chip_guide";

        if (selScreenGuides[chipGuideIndex, 0]) {

            //Setting guide
            chipGuideTransform = selScreenGuides[chipGuideIndex, 0];

            //Returning current chip to its original parent
            Debug.Log("Returning chip to original parent");
            chipGuideTransform.SetParent(selScreenGuides[chipGuideIndex, 1]);

            //Position
            chipGuideTransform.localPosition = Vector3.zero;

            //Scale
            chipGuideTransform.localScale = Vector3.zero;

            //Checking if selected chip guide is the same
            if (selCursorIndex < selScreenChips.Count)
            {
                if (selScreenGuides[chipGuideIndex, 1].transform.GetInstanceID() ==
                selScreenChips[selCursorIndex].transform.GetInstanceID())
                {
                    //Same chip, setting slot empty
                    Debug.Log("Same chip, setting slot empty");
                    selScreenGuides[chipGuideIndex, 0] = null; //Chip
                    selScreenGuides[chipGuideIndex, 1] = null; //Original Parent

                    SetMiniatureCheckmark(chipGuideIndex + 1, false);
                }
                else if (selScreenGuides[chipGuideIndex, 1].transform.GetInstanceID() !=
                    selScreenChips[selCursorIndex].transform.GetInstanceID())
                {
                    //Different chip, setting another chip on slot
                    Debug.Log("Different chip, changing guide transform");
                    chipGuideTransform = selScreenChips[selCursorIndex]
                                        .transform
                                        .FindChild(chipGuideName);

                    if (chipGuideTransform)
                    {
                        Debug.Log("selScreenChips: " + selScreenChips[selCursorIndex].name);
                        Debug.Log("chipGuideIndex: " + chipGuideIndex);
                        Debug.Log("chipGuideTransform: " + chipGuideTransform.name);

                        SelectionGuideFillSlot(chipGuideTransform, parentName, chipGuideIndex);
                        SetMiniatureCheckmark(chipGuideIndex + 1, true);
                    }
                }
            }
        }
        else
        {   //Empty slot, setting chip
            Debug.Log("Guide slot empty, setting chip");

            if (selCursorIndex < selScreenChips.Count)
            {
                chipGuideTransform = selScreenChips[selCursorIndex]
                                    .transform
                                    .FindChild(chipGuideName);

                SelectionGuideFillSlot(chipGuideTransform, parentName, chipGuideIndex);
                SetMiniatureCheckmark(chipGuideIndex + 1, true);
            }
        }

    }

    /// <summary>
    /// Sets a checkmark on the current selected chip miniature
    /// </summary>
    /// <param name="index">Selection cursor index</param>
    /// <param name="isChipSelected">Indicates if chip was selected or deselected</param>
    void SetMiniatureCheckmark(int index, bool isChipSelected) {

        string buttonName = "selection_mark_";
        RectTransform checkmark;

        //Setting object name
        switch (index) {
            case 1:
                buttonName = buttonName + "A";
                break;

            case 2:
                buttonName = buttonName + "B";
                break;

            case 3:
                buttonName = buttonName + "X";
                break;

            case 4:
                buttonName = buttonName + "Y";
                break;
        }

        Debug.Log("Current checkmark: " + buttonName + ", current index: " + index);

        //Changing object position and scale
        Debug.Log(selectionScreen.FindChild(buttonName).name);

        checkmark = (RectTransform)selectionScreen.FindChild(buttonName);

        if (isChipSelected)
        {
            checkmark.localScale = Vector3.one;
            checkmark.anchoredPosition = selectionCursor.anchoredPosition;
        }
        else {
            checkmark.localScale = Vector3.zero;
        }

    }

    /// <summary>
    /// Resets the scale of all checkmarks to zero
    /// </summary>
    void ResetCheckmarkScale() {
        selectionScreen.FindChild("selection_mark_A").localScale = Vector3.zero;
        selectionScreen.FindChild("selection_mark_B").localScale = Vector3.zero;
        selectionScreen.FindChild("selection_mark_X").localScale = Vector3.zero;
        selectionScreen.FindChild("selection_mark_Y").localScale = Vector3.zero;
    }

    /// <summary>
    /// Shows chip info screen
    /// </summary>
    /// <param name="state">Indicates if the info screen should be shown or not</param>
    public void ShowChipInfo(bool state) {

        if (state)
        {
            selectionChipInfo
                .transform
                .localScale = Vector2.one;
        }
        else
        {
            selectionChipInfo
                .transform
                .localScale = Vector2.zero;
        }

        //Change info content
        ChangeChipInformationText();
    }
    
    /// <summary>
    /// Changes text information according to current selected chip
    /// </summary>
    void ChangeChipInformationText() {

        Debug.Log("Changing chip information text...");

        string chipName;
        Text chipInformation;

        chipName = selScreenChips[selCursorIndex].name.ToLower();
        chipInformation = selectionChipInfo
                            .transform
                            .FindChild("info_text")
                            .GetComponent<Text>();

        //Getting information from localize file
        try {            
            //Defining string for localization
            string localeKey = chipName + "info";

            //Finding localization component
            LocalizeText localizeText;

            localizeText = selectionChipInfo
                                .transform
                                .FindChild("info_text")
                                .GetComponent<LocalizeText>();

            //Setting localized text
            localizeText.SetKeyAndLocalize(localeKey);
        }
        catch (NullReferenceException nrex) {
            //If no localize object is found, setting a placeholder information
            Debug.Log("Exception occured: " + nrex.Message);
            chipInformation.text = chipName.ToUpper() + "\ndescription.";
        }
    }

    /// <summary>
    /// Sets the position of the chip guides on the battle screen
    /// </summary>
    public void BattleGuidePosition() {

        Transform chipTransform;
        Transform chipParent;

        string[] chipsInUse = new string[4];
        string chipName = "";


        for (int i = 0; i < selScreenGuides.GetLength(0); i++)
        {
            if (selScreenGuides[i, 0]) { 

                //Chip object
                chipTransform = selScreenGuides[i, 0];

                //Parent
                chipParent = battleChipHelp
                                .transform
                                .FindChild(BattleChipSlot(i));

                chipTransform.SetParent(chipParent);

                //Position
                chipTransform.localPosition = new Vector3(2.0f, chipTransform.localPosition.y);

                //Scale
                chipTransform.localScale = Vector3.one;

                //Chip Name
                chipName = selScreenGuides[i, 1].transform.name;                

                try
                {
                    //Defining string for localization
                    string localeKey = selScreenGuides[i, 1].transform.name.ToLower();

                    //Finding localization component
                    LocalizeText localizeText;

                    localizeText = chipParent
                                    .FindChild("button_name")
                                    .GetComponent<LocalizeText>();

                    //Setting localized text
                    localizeText.SetKeyAndLocalize(localeKey);
                }
                catch (NullReferenceException nrex)
                {
                    //If no localize object is found, setting prefab name on guide
                    Debug.Log("Exception occured: " + nrex.Message);

                    chipParent.FindChild("button_name")
                            .GetComponent<Text>()
                            .text = chipName.ToUpper();
                }

                //Array of chips in use
                chipsInUse[i] = chipName;
                Debug.Log("Chip Name: " + selScreenGuides[i, 1] + ", Position: " + i);
            }
        }

        //Sending chips to folder script
        GameObject.Find("Folder").SendMessage("SetChipOnSlot", chipsInUse);
    }

    /// <summary>
    /// Returns the slot where the battle chip should be set
    /// </summary>
    /// <param name="slot">Slot number</param>
    /// <returns>Name of the battle chip slot</returns>
    string BattleChipSlot(int slot) {

        string chipSlot = "";

        switch (slot)
        {
            case 0: chipSlot = "button_a"; break;
            case 1: chipSlot = "button_b"; break;
            case 2: chipSlot = "button_x"; break;
            case 3: chipSlot = "button_y"; break;
        }

        return chipSlot;
    }

    /// <summary>
    /// Destroys current guide chip
    /// </summary>
    /// <param name="slot"></param>
    public void BattleChipDestroyGuide(int slot) {

        string chipToDestroy = "";
        string chipA = "button_a/chip_guide";
        string chipB = "button_b/chip_guide";
        string chipX = "button_x/chip_guide";
        string chipY = "button_y/chip_guide";

        switch (slot) {
            case 1: chipToDestroy = chipA; break;

            case 2: chipToDestroy = chipB; break;

            case 3: chipToDestroy = chipX; break;

            case 4: chipToDestroy = chipY; break;
        }

        //Checks if guide exists before deleting it
        if (battleChipHelp.transform.FindChild(chipToDestroy))
        {
            Destroy(battleChipHelp.transform.FindChild(chipToDestroy).gameObject);
            Debug.Log("Chip guide destroyed");
        }
    }
}
