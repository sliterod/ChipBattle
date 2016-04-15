using UnityEngine;
using System.Collections;

public class StageModification : MonoBehaviour {

    /********************** PLAYER OBJECTS  *************************/
    Transform player1;              //Transform. Player 1
    Transform player2;              //Transform. Player 2
    GameObject opponentObject;      //Current oppoent. Changes depending on which stage side this script is located
    GameObject playerObject;        //Current player. Changes depending on which stage side this script is located

    /********************** STAGE *************************/
    Transform limitWall;            //Stage boundary object. Can be moved on certain conditions

    /********************** AREA STEAL  *************************/
    float[] limitWallPositions;     //Positions where stage boundary can be moved to
    float[] stolenAreaPositions;    //Spawn positions for stolen area panels

    int currentAreaIndex;           //Current index for positioning limit wall and stolen areas

    GameObject stolenArea;          //Object containing the latest instantiated stolen area
    Transform opponentStolenArea;   //Object containing an area stolen by the opponent (for retrieve purposes)

    /********************** TIMERS *************************/
    float defaultAreaStealTime;     //Default duration for area steal effect.
    float currentAreaStealTime;     //Current area steal timer after decreasing on update
    float areaStealTimeOnReset;     //Contains default value to restore after timer reaches zero
    float areaStealBonusTime;       //Bonus time to apply only if a second area steal was made with < 5sec on timer

    /********************** CONTROL BOOLS *************************/
    bool canAreaStealTimerDecrease; //Indicates if update can activate timer decrease for Area Steal
    bool isAreaStealBonusTimeAdded; //Indicates if bonus time for area steal was added

    void Start() {

        InitializeStageRelatedObjects();

        InitializeTimers();
        InitializeArrays();
    }


    void Update() {
        if (canAreaStealTimerDecrease)
            UpdateAreaStealTimer();
    }

    /// <summary>
    /// Initializes the player, opponent and limit wall objects.
    /// </summary>
    void InitializeStageRelatedObjects() {
        limitWall = GameObject.Find("LimitLine").transform;

        player1 = GameObject.Find("Player1").transform;
        player2 = GameObject.Find("Player2").transform;
    }

    /// <summary>
    /// Iniatializes arrays according to current player object
    /// </summary>
    void InitializeArrays() {

        if (this.name.Contains("P1"))
        {
            limitWallPositions = new float[] { 0.0f, 3.33f, 6.66f };
            stolenAreaPositions = new float[] { 1.67f, 5.0f };
            opponentObject = player2.gameObject;
            playerObject = player1.gameObject;
        }
        else if (this.name.Contains("P2"))
        {
            limitWallPositions = new float[] { 0.0f, -3.33f, -6.66f };
            stolenAreaPositions = new float[] { -1.67f, -5.0f };
            opponentObject = player1.gameObject;
            playerObject = player2.gameObject;
        }

        currentAreaIndex = 0;
    }

    /// <summary>
    /// Initializes all timers used for stage modifications
    /// </summary>
    void InitializeTimers() {
        //Area Steal
        defaultAreaStealTime = 20.0f;
        areaStealBonusTime = 5.0f;

        currentAreaStealTime = defaultAreaStealTime;
        areaStealTimeOnReset = defaultAreaStealTime;
    }

    /**********************************************************
                            AREA STEAL
    **********************************************************/

    /// <summary>
    /// Steals a three panel area row from an opponent
    /// </summary>
    void AreaSteal() {

        Debug.Log("Trying to destroy a previously stolen area");
        //Check areas


        Debug.Log("Stealing area...");
        if (currentAreaIndex < 2)
        {
            //Change index
            currentAreaIndex += 1;

            //Stealing area
            AreaStealInstantiate();
            AreaStealChangePositions();

            //Activating timer
            AreaStealActivateTimer(true);

            if (this.name.Contains("P1"))
                GameObject.Find("P2Side").SendMessage("AreaStealOpponentArea", (Transform)stolenArea.transform);
            else if (this.name.Contains("P2"))
                GameObject.Find("P1Side").SendMessage("AreaStealOpponentArea", (Transform)stolenArea.transform);

            //Check if bonus should be added to timer
            AreaStealBonusTime();
        }
    }

    /// <summary>
    /// Send a message to opponent stage object to retrieves an area stolen from current player.
    /// </summary>
    void AreaStealRetrieve() {
        if (this.name.Contains("P1"))
            GameObject.Find("P2Side").SendMessage("AreaStealRestore");
        else if (this.name.Contains("P2"))
            GameObject.Find("P1Side").SendMessage("AreaStealRestore");
    }

    /// <summary>
    /// Stores data about an area stolen by opponent. Is used to retrieve
    /// the space in case of an area steal call
    /// </summary>
    /// <param name="opponentArea">Opponent area stolen from player</param>
    void AreaStealOpponentArea(Transform opponentArea) {
        opponentStolenArea = opponentArea.FindChild("area");
    }

    /// <summary>
    /// Restores stolen area to the original player
    /// </summary>
    void AreaStealRestore() {
        AreaStealDestroy();
    }

    /// <summary>
    /// Restores the areas stolen from player after timer reaches zero
    /// </summary>
    /// <returns>Player areas return to their normal state</returns>
    IEnumerator AreaStealTimeUp() {
        AreaStealRestore();

        yield return new WaitForSeconds(0.2f);

        AreaStealRestore();
    }

    /// <summary>
    /// Gives bonus secs. to timer if player performed a second area steal action
    /// with less than 5 seconds remaining
    /// </summary>
    void AreaStealBonusTime() {
        if (currentAreaStealTime < 5.0f && !isAreaStealBonusTimeAdded)
        {
            isAreaStealBonusTimeAdded = true;

            Debug.Log("Bonus time of " + areaStealBonusTime + " added to the area steal timer");
            currentAreaStealTime += areaStealBonusTime;
        }
    }

    /// <summary>
    /// Destroys the gameobject containing a previously stolen area
    /// </summary>
    void AreaStealDestroy() {

        string player = "";
        Renderer areaToRestore;

        if (stolenArea)
        {
            //Last stolen area. Used to restore player positions
            areaToRestore = stolenArea.transform.FindChild("area").GetComponent<Renderer>();

            //Current player
            if (this.name.Contains("P1"))
                player = "P1";
            else if (this.name.Contains("P2"))
                player = "P2";

            //Destroying object
            Destroy(stolenArea);

            //Reducing index. Setting to zero if negative to avoid out of bounds exceptions
            currentAreaIndex -= 1;

            if (currentAreaIndex < 0)
                currentAreaIndex = 0;

            //Setting new stolen area object
            string newStolenAreaName = "stolenArea" + player + "_" + currentAreaIndex.ToString();

            if (GameObject.Find(newStolenAreaName))
                stolenArea = GameObject.Find(newStolenAreaName);

            //Restoring player positions
            AreaStealRestorePositions(areaToRestore.bounds.min.x, areaToRestore.bounds.max.x);

            //Restoting timer values
            if (currentAreaIndex == 0) {
                Debug.Log("Stolen areas destroyed. Setting timers to default");
                AreaStealReset();
            }
        }
    }


    /// <summary>
    /// Changes limit wall and player positions
    /// </summary>
    /// <param name="isAreaRestoring">Indicates if the area position is being modified by a steal or a restore action</param>
    void AreaStealChangePositions() {

        Transform area = stolenArea.transform.FindChild("area");

        //Wall position
        limitWall.position = new Vector3(limitWallPositions[currentAreaIndex],
                                         limitWall.position.y,
                                         limitWall.position.z);


        if (opponentObject.transform.position.x <= area.GetComponent<Renderer>().bounds.max.x &&
            opponentObject.transform.position.x >= area.GetComponent<Renderer>().bounds.min.x)
        {
            Debug.Log("Opponent on stolen area, pushing aside");

            float newPlayerPos = 0.0f;

            if (opponentObject.transform.position.x > limitWall.position.x)
                newPlayerPos = limitWall.position.x - 0.5f;
            else if (opponentObject.transform.position.x < limitWall.position.x)
                newPlayerPos = limitWall.position.x + 0.5f;

            iTween.MoveTo(opponentObject,
                            new Hashtable() {   {"x",newPlayerPos},
                                            {"easetype","easeOutElastic"},
                                            {"time",0.2f}});
        }

    }

    /// <summary>
    /// Changes the position of the player and the limit wall after
    /// destroying a previously stolen area
    /// </summary>
    /// <param name="minBoundsX">Destroyed area X axis minimum bounds</param>
    /// <param name="maxBoundsX">Destroyed area X axis maximum bounds</param>
    void AreaStealRestorePositions(float minBoundsX, float maxBoundsX) {

        Debug.Log("Checking if player is on restored area");

        //Wall position
        limitWall.position = new Vector3(limitWallPositions[currentAreaIndex],
                                         limitWall.position.y,
                                         limitWall.position.z);

        if (playerObject.transform.position.x <= maxBoundsX &&
            playerObject.transform.position.x >= minBoundsX)
        {
            Debug.Log("Player on stolen area, pushing aside");

            float newPlayerPos = 0.0f;

            if (playerObject.transform.position.x > limitWall.position.x)
                newPlayerPos = limitWall.position.x - 0.5f;
            else if (playerObject.transform.position.x < limitWall.position.x)
                newPlayerPos = limitWall.position.x + 0.5f;

            iTween.MoveTo(playerObject,
                            new Hashtable() {   {"x",newPlayerPos},
                                                {"easetype","easeOutElastic"},
                                                {"time",0.2f}});
        }

    }

    /// <summary>
    /// Instantiates new object which serves as a stolen area for current player
    /// </summary>
    void AreaStealInstantiate() {

        string player = "";

        if (this.name.Contains("P1"))
            player = "P1";
        else if (this.name.Contains("P2"))
            player = "P2";

        stolenArea = Instantiate(Resources.Load("Stage/Stolen" + player, typeof(GameObject))) as GameObject;

        stolenArea.transform.position = new Vector3(stolenAreaPositions[currentAreaIndex - 1],
                                                        stolenArea.transform.position.y,
                                                        stolenArea.transform.position.z);

        stolenArea.name = "stolenArea" + player + "_" + currentAreaIndex.ToString();
    }

    /// <summary>
    /// Updates remaining area steal time using delta time
    /// </summary>
    void UpdateAreaStealTimer()
    {
        if (currentAreaStealTime > 0.0f)
        {
            currentAreaStealTime -= Time.smoothDeltaTime;
            Debug.Log("Area steal, remaining time: " + currentAreaStealTime);
        }
        else if (currentAreaStealTime <= 0.0f)
        {
            //Reseting values
            AreaStealReset();

            //Restoring stolen area
            Debug.Log("Area Steal time ended. Restoring areas");
            StartCoroutine(AreaStealTimeUp());
        }
    }

    /// <summary>
    /// Activates area steal timer after changing states
    /// </summary>
    /// <param name="state"></param>
    void AreaStealActivateTimer(bool state)
    {
        Debug.Log("Setting area steal activation to: " + state.ToString());
        canAreaStealTimerDecrease = state;
    }

    /// <summary>
    /// Resets timers and bools related to area steal action
    /// </summary>
    void AreaStealReset() {
        AreaStealActivateTimer(false);
        currentAreaStealTime = areaStealTimeOnReset;
        isAreaStealBonusTimeAdded = false;
    }

    /**********************************************************
                            CRACK PANELS
    **********************************************************/

    void CrackPanels() {

    }


    /**********************************************************
                            STAGE CHANGE
    **********************************************************/

    void ChangeField() {

    }


    /**********************************************************
                            AREA CHANGE
    **********************************************************/
    void ChangeArea() {

    }
}
