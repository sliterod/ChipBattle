using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    static float BASIC_SPEED = 5.0f; //The maximum speed of the character (so far is static because every player should move at the same speed
                                     //is important to be aware that a chacter could be moving at a lower speed if the stick is pressed softly
                                     //also a charater could move faster or slower if is affected by a buff or debuff
                                     //and inputs via keyboard or Dpad always result in moving at maximum speed

    float speedModifier = 0; //this will be changing if the player get a buff or debuff

    public CharacterAnimationController animationController;

    public PlayerStates CurrentState
    {
        get
        {
            return animationController.CurrentState;
        }
    }

    private int startingLife = 1000; //The starting and maximum amount of HP of the character
    public int StartingLife
    {
        get
        {
            return startingLife;
        }
        private set
        {
            startingLife = value;
        }
    }

    private int lifePoint = 700; //The current amount of HP of the character, if it reach 0 the character dies
    public int LifePoints
    {
        get
        {
            return lifePoint;
        }
        private set
        {
            lifePoint = value;
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
            currentSpeed = BASIC_SPEED + speedModifier;
            //Here we should check if there're any speed restriction affecting the characater (a half speed debuf for example)
            return currentSpeed;
        }
    }
    // Use this for initialization
    void Start () {
        animationController.PlayIdleAnim();
        lifePoint = startingLife;
        SendMessage("UpdateHpValue", lifePoint);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Functon that is called via SendMessage by the proyectiles when they hit you
    /// </summary>
    /// <param name="damage">The amount of damage deal by the attack</param>
    void OnHit(int damage)
    {
        lifePoint = Mathf.Clamp(LifePoints - damage, 0, startingLife);
        Debug.Log("Damage recieved, Life:" + lifePoint);
        if(lifePoint == 0)
        {
            onLethalDamage();
        }

        //Report damage to the UI
        SendMessage("UpdateHpValue", lifePoint);


        if(CurrentState != PlayerStates.usingChip && CurrentState != PlayerStates.takingDamage && CurrentState != PlayerStates.dead)
        {
            //Chip use and Damage are unstapable animation
            animationController.PlayDamageAnimation();
            //and tell it to play the corresponding animation 
        }
    }
    void heal(int amount)
    {
        lifePoint = LifePoints + amount;

        //Report damage to the UI
        SendMessage("UpdateHpValue", lifePoint);


        if (CurrentState != PlayerStates.usingChip && CurrentState != PlayerStates.takingDamage && CurrentState != PlayerStates.dead)
        {
            //Chip use and Damage are unstapable animation
            animationController.PlayDamageAnimation();
        }
    }

    void setSpeedModifier(float speedModifire) {
        this.speedModifier = speedModifire;
    }

    /// <summary>
    /// Function that should be called every time that the character stop moving
    /// </summary>
    public void eventReportStopMovement()
    {
        if (CurrentState == PlayerStates.moving)
        {
            animationController.PlayIdleAnim();
        }
    }

    /// <summary>
    /// Function that should be called every time that the character strat moving
    /// the X and Y value helps to decide what animation should be played (if the character is walking up or going back, etc)
    /// </summary>
    public void eventReportMovement(float x)
    {
        if (CurrentState == PlayerStates.idle)
        {
            animationController.PlayRunAnim(x);
        }
    }

    /// <summary>
    /// Function that should be called when a chip is activated
    /// </summary>
    public void eventReportChipActivation(int chipAnimation)
    {
        animationController.PlayChipAnimation(chipAnimation);
    }

    /// <summary>
    /// Function that is called at the end of the chip animation
    /// </summary>
    void OnChipAnimationFinish()
    {
        animationController.PlayIdleAnim();
    }

    /// <summary>
    /// Function that is called at the end of the damage animation
    /// </summary>
    void OnDamageAnimationFisnish()
    {
        if(CurrentState != PlayerStates.dead)
        {
            animationController.PlayIdleAnim();
        }
        
    }

    void onLethalDamage()
    {
        SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver);
        GameObject.Find("Gamestate").SendMessage("ChangeToBattleEnd");
    }
}
