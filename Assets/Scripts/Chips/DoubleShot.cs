using UnityEngine;
using System.Collections;

public class DoubleShot : Chip {

    bool isActive = false; //Flag to know if the chip had been activated

    int shootsCount = 0; //How many shots were fired since the activation

    Transform projectilePoint;

    /// <summary>
    /// Class constructor
    /// </summary>
    public DoubleShot()
    {
        _chipName = "#DoubleShot";
        _chipPrefabName = "DoubleShot";
        _animation = (int)ChipAnimations.DoubleShot;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Since this is a fixed chip and it should'nt be destroyed on use we need a reset function to call instead of the killself
    void Reset()
    {
        isActive = false; //Deactivate the chip
        shootsCount = 0; //Reset the shots count
    }

    /// <summary>
    /// Override of the Chip's Activate function
    /// </summary>
    public new void Activate()
    {
        if (!isActive) //To prevent using the chip multiple times
        {

            Debug.Log("Double Shot Activated");
            projectilePoint = GameObject.Find("Hand_R").transform;
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
                projectile.transform.position = projectilePoint.position; //Put it into position
                projectile.GetComponent<StraightShot>().Launch(StageSide.blue); //And we shoot it
                shootsCount ++;
            }
            else
            {
                projectilePoint = GameObject.Find("Hand_L").transform;
                projectile.transform.position = projectilePoint.position; //Put it into position
                projectile.GetComponent<StraightShot>().Launch(StageSide.blue); //And we shoot it
            }
            
        }
    }

    void OnChipAnimationFinish()
    {
        if (isActive)
        {
            Reset();
        }
    }

    
}
