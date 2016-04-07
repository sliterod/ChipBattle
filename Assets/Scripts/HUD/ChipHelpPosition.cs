using UnityEngine;
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
        if (Input.GetJoystickNames().Length > 0)
        {
            Debug.Log("Setting helper position. Joystick");
            ControllerPosition();
        }
        else
        {
            Debug.Log("Setting helper position. Keyboard");
            KeyboardPosition();
        }

    }

    /// <summary>
    /// Sets keyboard position for helper buttons
    /// </summary>
    void KeyboardPosition() {
        positionsKeyboard = new[] { new Vector2 (-300.0f, -30.0f),  //Q
                                    new Vector2 (-100.0f, -30.0f),  //W
                                    new Vector2 (100.0f, -30.0f),   //E
                                    new Vector2 (300.0f, -30.0f)};  //R

        chipA.anchoredPosition = positionsKeyboard[0];
        chipB.anchoredPosition = positionsKeyboard[1];
        chipX.anchoredPosition = positionsKeyboard[2];
        chipY.anchoredPosition = positionsKeyboard[3];

        chipA.transform
            .FindChild("button_text")
            .GetComponent<RectTransform>()
            .anchoredPosition = new Vector2(0.0f, 60.0f);
    }

    /// <summary>
    /// Sets controller position for helper buttons
    /// </summary>
    void ControllerPosition() {
        positionsController = new[] {   new Vector2 (0.0f, -120.0f),    //A
                                        new Vector2 (190.0f, -80.0f),   //B
                                        new Vector2 (-190.0f, -80.0f),  //X
                                        new Vector2 (0.0f, -30.0f)};    //Y

        chipA.anchoredPosition = positionsController[0];
        chipB.anchoredPosition = positionsController[1];
        chipX.anchoredPosition = positionsController[2];
        chipY.anchoredPosition = positionsController[3];
    }
}
