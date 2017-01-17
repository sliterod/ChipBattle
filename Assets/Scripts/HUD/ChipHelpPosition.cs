using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChipHelpPosition : MonoBehaviour {

    //Button help
    public RectTransform chipA;
    public RectTransform chipB;
    public RectTransform chipX;
    public RectTransform chipY;

    //Button positions
    Vector2[] positionsController;
    Vector2[] positionsKeyboard;

    // Use this for initialization
    void Start () {
        ChangeHelperPosition();
	}

    /// <summary>
    /// Changes helper position according to current button layout (keyboard, controller)
    /// </summary>
    void ChangeHelperPosition() {

        int joysticksConnected = Input.GetJoystickNames().Length;

        if (joysticksConnected > 0)
        {
//            Debug.Log("Setting helper position. Joystick");
            ControllerPosition();
            ChangeButtonText(joysticksConnected);
        }
        else
        {
//            Debug.Log("Setting helper position. Keyboard");
            KeyboardPosition();
            ChangeButtonText(joysticksConnected);
        }

    }

    /// <summary>
    /// Sets keyboard position for helper buttons
    /// </summary>
    void KeyboardPosition() {
        positionsKeyboard = new[] { new Vector2 (-300.0f, -40.0f),  //Q
                                    new Vector2 (-100.0f, -40.0f),  //W
                                    new Vector2 (100.0f, -40.0f),   //E
                                    new Vector2 (300.0f, -40.0f)};  //R

        chipA.anchoredPosition = positionsKeyboard[0];
        chipB.anchoredPosition = positionsKeyboard[1];
        chipX.anchoredPosition = positionsKeyboard[2];
        chipY.anchoredPosition = positionsKeyboard[3];
    }

    /// <summary>
    /// Sets controller position for helper buttons
    /// </summary>
    void ControllerPosition() {
        positionsController = new[] {   new Vector2 (100.0f, -80.0f),    //A
                                        new Vector2 (270.0f, -20.0f),   //B
                                        new Vector2 (-270.0f, -80.0f),  //X
                                        new Vector2 (-100.0f, -20.0f)};    //Y

        chipA.anchoredPosition = positionsController[0];
        chipB.anchoredPosition = positionsController[1];
        chipX.anchoredPosition = positionsController[2];
        chipY.anchoredPosition = positionsController[3];
    }

    /// <summary>
    /// Changes the helper button text according to the input method
    /// = 0 - Keyboard
    /// > 0 - Joystick
    /// </summary>
    /// <param name="joystickNamesLength">Length of connected joysticks array</param>
    void ChangeButtonText(int joystickNamesLength) {

        Text _chipA = chipA.FindChild("button_text").GetComponent<Text>();
        Text _chipB = chipB.FindChild("button_text").GetComponent<Text>();
        Text _chipX = chipX.FindChild("button_text").GetComponent<Text>();
        Text _chipY = chipY.FindChild("button_text").GetComponent<Text>();

        if (joystickNamesLength > 0)
        {
            _chipA.text = "A";
            _chipB.text = "B";
            _chipX.text = "X";
            _chipY.text = "Y";
        }
        else
        {
            _chipA.text = "Q";
            _chipB.text = "W";
            _chipX.text = "E";
            _chipY.text = "R";
        }
    }
}
