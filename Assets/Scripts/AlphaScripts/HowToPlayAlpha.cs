using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HowToPlayAlpha : MonoBehaviour {

    public GameObject howToKeyboard;
    public GameObject howToXbox;
    public Text resumeText;
    public GameObject qKey;
    public GameObject aButton;

	// Use this for initialization
	void Awake () {
        CheckJoystickConnection();
        ScreenHelpButtons();
    }
	
    void CheckJoystickConnection()
    {
        int joysticksConnected = Input.GetJoystickNames().Length;

        if (joysticksConnected > 0)
        {
            //Controller Help
            howToKeyboard.SetActive(false);
            howToXbox.SetActive(true);
        }
        else
        {
            //Keyboard help
            howToKeyboard.SetActive(true);
            howToXbox.SetActive(false);
        }
    }

    void ScreenHelpButtons() {

        if (Application.loadedLevelName == "Demo") {
            BattleState currentState;
            currentState = GameObject.Find("Gamestate")
                .GetComponent<Gamestate>()
                .CurrentBattleState;

            if (currentState == BattleState.pause) {
                resumeText.text = "BACK/START";
            }
        }
        else if (Application.loadedLevelName == "splash_alpha")
        {

        }
    }
}
