using UnityEngine;
using System.Collections;

public class InputCapture : MonoBehaviour {

    /******************** SCRIPT OBJECTS **********************/
    public CharacterControl characterControl;
    public Gamestate gamestate;

    /******************** CONTROL BOOLS **********************/
    bool isDpadInUse;
    bool isStickInUse;

    // Update is called once per frame
    void Update () {
        CardModuleInputs();
        EventInputs();
        MovementInputsController();
        MovementInputsMouse();
	}

    /// <summary>
    /// Captures card module inputs
    /// </summary>
    void CardModuleInputs() {

        //Chip 1
        if (Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.Q)) {
            if (gamestate.CurrentBattleState == BattleState.battle)
            {
                Debug.Log("Input captured. Using Chip 1");
                characterControl.UseChip(1);
                gamestate.DestroyChipBattleScreen(1);
            }

            if (gamestate.CurrentBattleState == BattleState.standby)
            {
                Debug.Log("Input captured. Standing by, no chip can be used.");
            }

            if (gamestate.CurrentBattleState == BattleState.selectionScreen)
            {
                Debug.Log("Input captured. Current highlighted card assigned to Chip 1 button");
                gamestate.SetChipSelectionScreen(1);
            }

            if (gamestate.CurrentBattleState == BattleState.results)
            {
                Debug.Log("Input captured. Result screen, skiping results");
            }

            if (gamestate.CurrentBattleState == BattleState.restart)
            {
                gamestate.ReloadScene();
            }
        }

        //Chip 2
        if (Input.GetButtonDown("B") || Input.GetKeyDown(KeyCode.W))
        {
            if (gamestate.CurrentBattleState == BattleState.battle)
            {
                Debug.Log("Input captured. Using Chip 2");
                characterControl.UseChip(2);
                gamestate.DestroyChipBattleScreen(2);
            }

            if (gamestate.CurrentBattleState == BattleState.selectionScreen)
            {
                Debug.Log("Input captured. Current highlighted card assigned to Chip 1 button");
                gamestate.SetChipSelectionScreen(2);
            }

            if (gamestate.CurrentBattleState == BattleState.restart)
            {
                gamestate.ReloadScene();
            }
        }

        //Chip 3
        if (Input.GetButtonDown("X") || Input.GetKeyDown(KeyCode.E))
        {
            if (gamestate.CurrentBattleState == BattleState.battle)
            {
                Debug.Log("Input captured. Using Chip 3");
                characterControl.UseChip(3);
                gamestate.DestroyChipBattleScreen(3);
            }

            if (gamestate.CurrentBattleState == BattleState.selectionScreen)
            {
                Debug.Log("Input captured. Current highlighted card assigned to Chip 1 button");
                gamestate.SetChipSelectionScreen(3);
            }

            if (gamestate.CurrentBattleState == BattleState.restart)
            {
                gamestate.ReloadScene();
            }
        }

        //Chip 4
        if (Input.GetButtonDown("Y") || Input.GetKeyDown(KeyCode.R))
        {
            if (gamestate.CurrentBattleState == BattleState.battle)
            {
                Debug.Log("Input captured. Using Chip 4");
                characterControl.UseChip(4);
                gamestate.DestroyChipBattleScreen(4);
            }

            if (gamestate.CurrentBattleState == BattleState.selectionScreen)
            {
                Debug.Log("Input captured. Current highlighted card assigned to Chip 1 button");
                gamestate.SetChipSelectionScreen(4);
            }

            if (gamestate.CurrentBattleState == BattleState.restart) {
                gamestate.ReloadScene();
            }
        }

        //Fixed 1
        /*if (Mathf.Round(Input.GetAxis("LT")) == 1.0f || Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Input captured. Using Fixed 1");
        }

        //Fixed 2
        if (Mathf.Round(Input.GetAxis("RT")) == -1.0f || Input.GetKeyDown(KeyCode.RightShift))
        {
            Debug.Log("Input captured. Using Fixed 2");
        }*/
        
    }

    /// <summary>
    /// Captures event inputs like pausing the game or opening selection screen
    /// </summary>
    void EventInputs() {

        //Pause/ OK button
        if (Input.GetButtonDown("Start") || Input.GetKeyDown(KeyCode.Return))
        {
            if (gamestate.CurrentBattleState == BattleState.battle && Time.timeScale > 0)
            {
                Debug.Log("Input captured. Start button, pausing game");
                gamestate.CurrentBattleState = BattleState.pause;
            }

            if (gamestate.CurrentBattleState == BattleState.pause && Time.timeScale == 0)
            {
                Debug.Log("Input captured. Start button, unpausing game");
                gamestate.CurrentBattleState = BattleState.battle;
            }

            if (gamestate.CurrentBattleState == BattleState.selectionScreen)
            {
                Debug.Log("Input captured. Setting cursor on OK button");
                gamestate.SetSelectionScreenCursorOK();
            }

            if (gamestate.CurrentBattleState == BattleState.restart)
            {
                gamestate.ReloadScene();
            }
        }

        //Return
        if (Input.GetButtonDown("Back") || Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamestate.CurrentBattleState == BattleState.battle && Time.timeScale > 0)
            {
                Debug.Log("Input captured. Back button, pausing game");
                gamestate.CurrentBattleState = BattleState.pause;
            }

            if (gamestate.CurrentBattleState == BattleState.pause && Time.timeScale == 0)
            {
                Debug.Log("Input captured. Back button, unpausing game");
                gamestate.CurrentBattleState = BattleState.battle;
            }
        }

        //
        if (Input.GetButtonDown("LB") || Input.GetKeyDown(KeyCode.Tab))
        {
            if (gamestate.CurrentBattleState == BattleState.selectionScreen)
            {
                Debug.Log("Input captured. Showing info");
                gamestate.DisplayChipInformation();
            }

            if (gamestate.CurrentBattleState == BattleState.battle)
            {
                Debug.Log("Input captured. Skill 1 fired");
                /*Debug.Log("Input captured. Custom bar full, entering selection screen");
                characterControl.FlushChips();
                characterControl.setChip("GrenadeChip", 1);
                characterControl.setChip("GrenadeChip", 2);
                characterControl.setChip("Cannon", 3);
                characterControl.setChip("Cannon", 4);*/
            }
        }

        //
        if (Input.GetButtonDown("RB") || Input.GetKeyDown(KeyCode.Space))
        {
            if (gamestate.CurrentBattleState == BattleState.selectionScreen)
            {
                Debug.Log("Input captured. Shuffling folder");
            }

            if (gamestate.CurrentBattleState == BattleState.battle)
            {
                Debug.Log("Input captured. Skill 2 fired");
            }
        }
    }

    /// <summary>33
    /// Captures input from d-pad or sticks
    /// </summary>
    void MovementInputsController() {

        if (gamestate.CurrentBattleState == BattleState.battle)
        {
            Debug.Log("Input captured. Moving character");
            MoveCharacter();
        }

        if (gamestate.CurrentBattleState == BattleState.selectionScreen)
        {
            MoveChipSelector();
        }
    }

    /// <summary>
    /// Moves character after axis input was captured. BattleState = battle
    /// </summary>
    void MoveCharacter() {
        float x_axis = 0; //the amount of movement on the horizontal axis
        float y_axis = 0; //the amount of movement on the vertical axis

        //Sticks
        if (Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.8f || Mathf.Abs(Input.GetAxis("Vertical")) >= 0.8f)
        {
            x_axis = Input.GetAxis("Horizontal");
            y_axis = Input.GetAxis("Vertical") * -1;
        }

        //Dpad y teclado
        if (Input.GetAxis("DpadHorizontal") == -1.0f || Input.GetKey(KeyCode.LeftArrow))
        {
            x_axis = -1.0f;
        }

        if (Input.GetAxis("DpadHorizontal") == 1.0f || Input.GetKey(KeyCode.RightArrow))
        {
            x_axis = 1.0f;
        }

        if (Input.GetAxis("DpadVertical") == -1.0f || Input.GetKey(KeyCode.DownArrow))
        {
            y_axis = -1.0F;
        }

        if (Input.GetAxis("DpadVertical") == 1.0f || Input.GetKey(KeyCode.UpArrow))
        {
            y_axis = 1.0F;
        }

        //Call the movement function on the charater
        characterControl.Move(x_axis, y_axis);
    }

    /// <summary>
    /// Moves chip selector after axis input was captured. BattleState = selectionScreen
    /// </summary>
    void MoveChipSelector() {
        
        //Dpad, Sticks, Keyboard
        if (/*Input.GetAxis("Horizontal") <= -0.8f ||
            Input.GetAxis("DpadHorizontal") == -1.0f || */
            Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gamestate.MoveSelectionScreenCursor(Movement.left);
        }

        if (/*Input.GetAxis("Horizontal") >= 0.8f ||
            Input.GetAxis("DpadHorizontal") == 1.0f || */
            Input.GetKeyDown(KeyCode.RightArrow))
        {
            gamestate.MoveSelectionScreenCursor(Movement.right);
        }

        //Moving cursor with controller
        MoveCursorWithController();

        if (Input.GetAxis("Vertical") >= 0.8f ||
            Input.GetAxis("DpadVertical") == -1.0f || 
            Input.GetKey(KeyCode.DownArrow))
        {
            gamestate.MoveSelectionScreenCursor(Movement.down);
        }

        if (Input.GetAxis("Vertical") <= -0.8f || 
            Input.GetAxis("DpadVertical") == 1.0f || 
            Input.GetKey(KeyCode.UpArrow))
        {
            gamestate.MoveSelectionScreenCursor(Movement.up);
        }
    }

    /// <summary>
    /// Moves cursor with controller. Avoids infinite loop when moving the cursor
    /// with either the stick or the dpad
    /// </summary>
    void MoveCursorWithController()
    {
        //Dpad
        if (Input.GetAxis("DpadHorizontal") != 0.0f)
        {
            if (!isDpadInUse)
            {
                if (Input.GetAxis("DpadHorizontal") == 1.0f)
                {
                    gamestate.MoveSelectionScreenCursor(Movement.right);
                }

                if (Input.GetAxis("DpadHorizontal") == -1.0f)
                {
                    gamestate.MoveSelectionScreenCursor(Movement.left);
                }

                isDpadInUse = true;
            }
        }

        //Sticks
        if (Input.GetAxis("Horizontal") != 0.0f)
        {
            if (!isStickInUse)
            {
                if (Input.GetAxis("Horizontal") >= 0.55f)
                {
                    Debug.Log("Horiz right: " + Input.GetAxis("Horizontal"));
                    gamestate.MoveSelectionScreenCursor(Movement.right);
                }

                if (Input.GetAxis("Horizontal") <= -0.55f)
                {
                    Debug.Log("Horiz left: " + Input.GetAxis("Horizontal"));
                    gamestate.MoveSelectionScreenCursor(Movement.left);
                }

                isStickInUse = true;
            }
        }

        //Reset bool
        if (Input.GetAxis("DpadHorizontal") == 0)
        {
            isDpadInUse = false;
        }

        if (Input.GetAxis("Horizontal") >= -0.5f &&
            Input.GetAxis("Horizontal") <= 0.5f)
        {
            Debug.Log("Horizontal false;");
            isStickInUse = false;
        }
    }

    void MovementInputsMouse() {

        if (Input.GetMouseButtonDown(0))
        {
           /* Debug.Log("Input captured. Moving character to click position");
            Transform cube = GameObject.Find("testchar").transform;
            cube.GetComponent<CharacterControl>().MoveCharacterMouse(); */
            }
        }
}
