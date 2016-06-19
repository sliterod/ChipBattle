using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectionScreenText : MonoBehaviour {

    public Text buttonA;
    public Text buttonB;
    public Text buttonX;
    public Text buttonY;
    public Text buttonL;
    public Text buttonR;
    public Text buttonInfo;

    // Use this for initialization
    void Start () {
        CheckJoystickAvailability();
	}

    /// <summary>
    /// Checks if joystick is connected in order to change the guide button text
    /// </summary>
    void CheckJoystickAvailability() {
        int joysticksConnected = Input.GetJoystickNames().Length;
        
        ChangeButtonText(joysticksConnected);
    }

    /// <summary>
    /// Changes the guide button text according to the input method
    /// = 0 - Keyboard
    /// > 0 - Joystick
    /// </summary>
    /// <param name="joystickNamesLength">Length of connected joysticks array</param>
    void ChangeButtonText(int joystickNamesLength)
    {
        if (joystickNamesLength > 0)
        {
            buttonA.text = "A";
            buttonB.text = "B";
            buttonX.text = "X";
            buttonY.text = "Y";
            buttonL.text = "LB";
            buttonR.text = "RB";
        }
        else
        {
            buttonA.text = "Q";
            buttonB.text = "W";
            buttonX.text = "E";
            buttonY.text = "R";
            buttonL.text = "TAB";
            buttonR.text = "SPC";
        }

        buttonInfo.text = buttonL.text;
    }
}
