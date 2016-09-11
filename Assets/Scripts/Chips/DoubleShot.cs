using UnityEngine;
using System.Collections;

public class DoubleShot : Chip {

    static float COOLDOWN_TIME = 0.3f;

    float cooldownTimeLeft;

    int shootsCount = 0; //How many shots were fired since the activation

    bool isOnCooldown = false;
    AudioSource electroSound;
    Transform projectilePoint;

    /// <summary>
    /// Class constructor
    /// </summary>
    public DoubleShot()
    {
        _chipName = "#DoubleShot";
        _chipPrefabName = "DoubleShot";
        _animation = (int)ChipAnimations.DoubleShot;
        isFixed = true;
    }

    // Use this for initialization
    void Start () {
            electroSound = GameObject.Find("ChipsSounds/dobleshot").GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        if (isOnCooldown)
        {
            cooldownTimeLeft -= Time.deltaTime;
            if (cooldownTimeLeft <= 0.0f)
            {
                isOnCooldown = false;
            }
        }
	}

    //Since this is a fixed chip and it should'nt be destroyed on use we need a reset function to call instead of the killself
    void Reset()
    {
        isActive = false; //Deactivate the chip
        shootsCount = 0; //Reset the shots count
        cooldownTimeLeft = COOLDOWN_TIME;
        isOnCooldown = true;
    }

    /// <summary>
    /// Override of the Chip's Activate function
    /// </summary>
    public override void Activate()
    {
        if (!isActive && !isOnCooldown) //To prevent using the chip multiple times and spaming
        {

            Debug.Log("Double Shot Activated");
            isActive = true;
            foreach (GameObject element in GameObject.FindGameObjectsWithTag("AnimationController"))
            //We search for every "animationController" objects in the scene
            {
                if (element.transform.root == this.transform.root)
                {
                    Debug.Log("Animation Controller found");
                    //we select the one inside our hierchy
                    element.GetComponent<CharacterAnimationController>().PlayChipAnimation(Animation);
                    //and tell it to play the corresponding animation 
                }
            }

        }
    }

    void OnHitFrame()
    {
        if (isActive)
        {
            GameObject projectile = Instantiate(Resources.Load("Projectiles/StraightShot", typeof(GameObject))) as GameObject;
            if (shootsCount == 0) //First shot
            {
                //We take the projectile form the resources
                projectilePoint = transform.root.Find(RIGHT_HAND_PATH);
                projectile.transform.position = projectilePoint.position; //Put it into position
                if(transform.root.gameObject.layer == 8)
                {
                    projectile.GetComponent<StraightShot>().Launch(StageSide.blue); //And we shoot it
                }
                else
                {
                    projectile.GetComponent<StraightShot>().Launch(StageSide.red); //And we shoot it
                }
                
                shootsCount ++;
            }
            else
            {
                projectilePoint = transform.root.Find(LEFT_HAND_PATH);
                projectile.transform.position = projectilePoint.position; //Put it into position
                if (transform.root.gameObject.layer == 8)
                {
                    projectile.GetComponent<StraightShot>().Launch(StageSide.blue); //And we shoot it
                }
                else
                {
                    projectile.GetComponent<StraightShot>().Launch(StageSide.red); //And we shoot it
                }
            }
            electroSound.PlayDelayed(0);


        }
    }

    /// <summary>
    /// Since this a fixed-ability chip it shouldn't be destroyed
    /// </summary>
    new void OnChipAnimationFinish()
    {
        if (isActive)
        {
            Reset();
        }
    }

    public override bool IsReady()
    {
        if (!isActive && !isOnCooldown)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
