using UnityEngine;
using System.Collections;

public class TimeModifiers : MonoBehaviour {

    //Custom Gauge timer
    float currentCustomGaugeTime;                   //Current custom gauge time
    float customGaugeTimeOnReset;                   //Custom gauge time reset value
    public float defaultCustomGaugeTime;           //Predeterminated time for custom gauge

    //Custom Gauge modifiers
    float slowModifier;
    float fastModifier;
    
    //Control booleans
    bool canGaugeTimerDecrease;

	// Use this for initialization
	void Start () {
        InitializeTimers();
	}

    void Update() {
        /*
        if (Input.GetKeyDown(KeyCode.Z))
        {
            FastGaugeTimer();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            SlowGaugeTimer();
        }
        */
    }

	void FixedUpdate () {
        if (canGaugeTimerDecrease)
            UpdateGaugeTimer();
	}

    /// <summary>
    /// Initializes custom gauge timers
    /// </summary>
    void InitializeTimers() {

        //Custom gauge
        //defaultCustomGaugeTime = 9.0f;

        currentCustomGaugeTime = defaultCustomGaugeTime;
        customGaugeTimeOnReset = defaultCustomGaugeTime;

        slowModifier = 1.5f;
        fastModifier = 0.5f;

    }

    /**********************************************************
                            CUSTOM GAUGE
    **********************************************************/

    /// <summary>
    /// Updates the gauge timer using fixed delta time
    /// </summary>
    void UpdateGaugeTimer() {
        if (currentCustomGaugeTime > 0.0f) {
            currentCustomGaugeTime -= Time.fixedDeltaTime;
//            Debug.Log("Custom gauge, remaining time: " + currentCustomGaugeTime);
        }
        else if (currentCustomGaugeTime <= 0.0f) {
            //Reseting values
            currentCustomGaugeTime = customGaugeTimeOnReset;
            ActivateCustomGauge(false);

            //Send message to UI
            Debug.Log("Sending message, indicates that custom gauge is full");
            this.GetComponent<Gamestate>().ChangeBattleState(BattleState.selectionScreen);
        }
    }

    /// <summary>
    /// Alter custom gauge timer after applying a slow modifier
    /// </summary>
    void SlowGaugeTimer() {

        float newGaugeTimer;

        //Setting new time
        Debug.Log("Gauge set to slow");
        newGaugeTimer = defaultCustomGaugeTime * slowModifier;

        AlterGaugeTimer(newGaugeTimer);
        
    }

    /// <summary>
    /// Alter custom gauge timer after applying a fast modifier
    /// </summary>
    void FastGaugeTimer() {
        float newGaugeTimer;

        //Setting new time
        Debug.Log("Gauge set to fast");
        newGaugeTimer = defaultCustomGaugeTime * fastModifier;

        AlterGaugeTimer(newGaugeTimer);
    }

    /// <summary>
    /// Alters custom gauge time after applying modifiers
    /// </summary>
    /// <param name="newGaugeTimer">New default timer value</param>
    void AlterGaugeTimer(float newGaugeTimer) {

        float gaugeTimePercentage;

        //Percentage covered
        gaugeTimePercentage = (currentCustomGaugeTime * 1.0f) / customGaugeTimeOnReset;

        //Remaining custom gauge time
        currentCustomGaugeTime = gaugeTimePercentage * newGaugeTimer;

        customGaugeTimeOnReset = newGaugeTimer;
        Debug.Log("Current gauge time: " + customGaugeTimeOnReset);
    }

    /// <summary>
    /// Get current gauge fill percentage
    /// </summary>
    /// <returns>Current gauge time percentage</returns>
    public float GetGaugeTimePercentage() {

        float percentage = (currentCustomGaugeTime * 1.0f) / customGaugeTimeOnReset;

        return Mathf.Abs(percentage - 1.0f);
    }

    /// <summary>
    /// Activates custom gauge after changing states
    /// </summary>
    /// <param name="state">State of activation of the gauge</param>
    void ActivateCustomGauge(bool state) {
//        Debug.Log("Setting gauge activation to: " + state.ToString());
        canGaugeTimerDecrease = state;
    }

}
