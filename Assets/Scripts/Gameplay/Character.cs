using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    static float BASIC_SPEED = 5.0f; //The maximum speed of the character (so far is static because every player should move at the same speed
                                     //is important to be aware that a chacter could be moving at a lower speed if the stick is pressed softly
                                     //also a charater could move faster or slower if is affected by a buff or debuff
                                     //and inputs via keyboard or Dpad always result in moving at maximum speed

    private PlayerStates currentState;
    public PlayerStates CurrentState
    {
        get
        {
            return currentState;
        }
        private set
        {
            currentState = value;
        }
    }

    /// <summary>
    /// return the speed that should be apllied to the movement
    /// </summary>
    public float movementSpeed
    {
        get
        {
            float currentSpeed;
            currentSpeed = BASIC_SPEED;
            //Here we should check if there're any speed restriction affecting the characater (a half speed debuf for example)
            return currentSpeed;
        }
    }
    // Use this for initialization
    void Start () {
        currentState = PlayerStates.idle;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Function that should be called every time that the character stop moving
    /// </summary>
    public void eventReportStopMovement()
    {
        if (currentState == PlayerStates.moving)
        {
            currentState = PlayerStates.idle;
            //here we should call the animator and tell it to start playing the idle animation
        }
    }

    /// <summary>
    /// Function that should be called every time that the character strat moving
    /// the X and Y value helps to decide what animation should be played (if the character is walking up or going back, etc)
    /// </summary>
    public void eventReportMovement(float x_value,float y_value)
    {
        if (currentState == PlayerStates.idle)
        {
            currentState = PlayerStates.moving;
            //here we should decide wich animation we're going to play using the X and Y parameters
            //here we should call the animator and tell it to start playing the moving animation
        }
    }
}
