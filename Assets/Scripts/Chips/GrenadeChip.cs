using UnityEngine;
using System.Collections;

public class GrenadeChip : Chip {

    Transform projectilePoint;

    /// <summary>
    /// Class constructor
    /// </summary>
    public GrenadeChip()
    {
        _chipName = "#Grenade";
        _chipPrefabName = "GrenadeChip";
        _animation = (int)ChipAnimations.Launch;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Override of the Chip's Activate function
    /// </summary>
    public override void Activate()
    {
        if (!isActive) //To prevent using the chip multiple times
        {

            Debug.Log("Grenade Activated");
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
            GameObject projectile = Instantiate(Resources.Load("Projectiles/Grenade", typeof(GameObject))) as GameObject;
            //We take the projectile form the resources
            projectile.transform.position = projectilePoint.position; //Put it into position
            Debug.Log(projectilePoint.position);
            projectile.transform.GetChild(0).GetComponent<Grenade>().Launch(StageSide.blue); //And we shoot it
        }
    }

}
