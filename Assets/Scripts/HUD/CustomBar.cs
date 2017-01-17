using UnityEngine;
using System.Collections;

public class CustomBar : MonoBehaviour {

    //Custom bar elements
    public RectTransform customBarFill;         //Custom bar filling
    TimeModifiers timeModifier;                 //Timer for custom bar

    //Control booleans
    bool canGaugeFillUp;                        //Can custom gauge be filled?

	// Use this for initialization
	void Awake () {
        timeModifier = GameObject.Find("Gamestate").GetComponent<TimeModifiers>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (canGaugeFillUp)
            FillBar(); 
	}

    /// <summary>
    /// Activates gauge filling
    /// </summary>
    /// <param name="state">State of gauge filling</param>
    void ActivateGaugeFilling(bool state) {
        canGaugeFillUp = state;
    }

    /// <summary>
    /// Resets current gauge scale
    /// </summary>
    void ResetGaugeFilling() {
//        Debug.Log("Resetting gauge filling");
        customBarFill.localScale = new Vector3(0.0f, customBarFill.localScale.y, customBarFill.localScale.z);
    }

    /// <summary>
    /// Fills custom bar from 0.0f to 1.0f
    /// </summary>
    void FillBar() {

        if (customBarFill.localScale.x < 1.0f)
        {
            customBarFill.localScale = new Vector3( timeModifier.GetGaugeTimePercentage(), 
                                                    customBarFill.localScale.y, 
                                                    customBarFill.localScale.z);
        }
        else if (customBarFill.localScale.x >= 1.0f) {
            ActivateGaugeFilling(false);
        }
    }
}
