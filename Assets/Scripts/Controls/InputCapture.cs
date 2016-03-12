using UnityEngine;
using System.Collections;

public class InputCapture : MonoBehaviour {

    public CharacterControl characterControl;

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
            Debug.Log("Input captured. Using Chip 1");
            characterControl.UseChip(1);
        }

        //Chip 2
        if (Input.GetButtonDown("B") || Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Input captured. Using Chip 2");
        }

        //Chip 3
        if (Input.GetButtonDown("X") || Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Input captured. Using Chip 3");
        }

        //Chip 4
        if (Input.GetButtonDown("Y") || Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Input captured. Using Chip 4");
        }

        //Fixed 1
        if (Mathf.Round(Input.GetAxis("LT")) == 1.0f || Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Input captured. Using Fixed 1");
        }

        //Fixed 2
        if (Mathf.Round(Input.GetAxis("RT")) == -1.0f || Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Input captured. Using Fixed 2");
        }
        
    }

    /// <summary>
    /// Captures event inputs like pausing the game or opening selection screen
    /// </summary>
    void EventInputs() {

        //Pause/ OK button
        if (Input.GetButtonDown("Start") || Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Input captured. Start button");
        }

        //Return
        if (Input.GetButtonDown("Back") || Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Input captured. Back button");
        }

        //Custom screen
        if (Input.GetButtonDown("LB") || Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Input captured. Custom bar full, entering selection screen");
        }

        //Custom screen
        if (Input.GetButtonDown("RB") || Input.GetKeyDown(KeyCode.RightShift))
        {
            Debug.Log("Input captured. Custom bar full, entering selection screen");
        }
    }

    /// <summary>
    /// Captures input from d-pad or sticks
    /// </summary>
    void MovementInputsController() {

        float x_axis = 0; //the amount of movement on the horizontal axis
        float y_axis = 0; //the amount of movement on the vertical axis

        //Sticks
        if (Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.8f || Mathf.Abs(Input.GetAxis("Vertical")) >= 0.8f)
        {
            x_axis = Input.GetAxis("Horizontal");
            y_axis = Input.GetAxis("Vertical")*-1;
        }

        //Dpad y teclado
        if (Input.GetAxis("DpadHorizontal") == -1.0f || Input.GetKey(KeyCode.LeftArrow)) {
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

    void MovementInputsMouse() {

        if (Input.GetMouseButtonDown(0))
        {
           /* Debug.Log("Input captured. Moving character to click position");
            Transform cube = GameObject.Find("testchar").transform;
            cube.GetComponent<CharacterControl>().MoveCharacterMouse(); */
        }
    }
}
