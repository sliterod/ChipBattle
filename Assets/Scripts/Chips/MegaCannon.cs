﻿using UnityEngine;
using System.Collections;

public class MegaCannon : Chip {

    
    Transform projectilePoint;

    /// <summary>
    /// Class constructor
    /// </summary>
    public MegaCannon()
    {
        _chipName = "#MegaCannon";
        _chipPrefabName = "MegaCannon";
        _animation = (int)ChipAnimations.SingleShot;
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
        if(!isActive) //To prevent using the chip multiple times
        {

            projectilePoint = transform.root.Find(RIGHT_HAND_PATH);
            isActive = true;
            foreach (GameObject element in GameObject.FindGameObjectsWithTag("AnimationController"))
                //We search for every "animationController" objects in the scene
            {
                if(element.transform.root == this.transform.root)
                {
                    Debug.Log("Animation Controller found");
                    //we select the one inside our hierchy
                    element.GetComponent<CharacterAnimationController>().PlayChipAnimation(Animation);
                    //and tell it to play the corresponding animation 
                }
            }

        }
        
        /*
        GameObject projectile = Instantiate(Resources.Load("Projectiles/CannonBall", typeof(GameObject))) as GameObject;
        //We take the projectile form the resources
        projectile.transform.position = this.transform.position; //Put it into position
        projectile.GetComponent<CannonBall>().Launch(StageSide.blue); //And we shoot it

        // This is a temporal solution, the chip shouldn't be destroyed before the amimation ends
        KillSelf();
        */
    }

    void OnHitFrame()
    {
        if (isActive)
        {
            GameObject projectile = Instantiate(Resources.Load("Projectiles/MegaCannonBall", typeof(GameObject))) as GameObject;
            GameObject projectileTwo = Instantiate(Resources.Load("Projectiles/MegaCannonBallSecond", typeof(GameObject))) as GameObject;
            GameObject projectileThree = Instantiate(Resources.Load("Projectiles/MegaCannonBallThree", typeof(GameObject))) as GameObject;
            //We take the projectile form the resources
            projectile.transform.position = projectilePoint.position; //Put it into position
            projectileTwo.transform.position = projectilePoint.position; //Put it into position
            projectileThree.transform.position = projectilePoint.position; //Put it into position
            Vector3 position = projectileTwo.transform.position;
            position.y += 0.5f;
            position.x -= 0.1f;
            position.z -= 0.1f;
            projectileTwo.transform.position = position;

            Vector3 positionThree = projectileThree.transform.position;
            positionThree.y -= 0.5f;
            positionThree.x += 0.1f;
            positionThree.z += 0.1f;
            projectileThree.transform.position = position;

            

            if (transform.root.gameObject.layer == 8)
            {
                projectile.GetComponent<MegaCannonBall>().Launch(StageSide.blue); //And we shoot it
                projectileTwo.GetComponent<MegaCannonBallSecond>().Launch(StageSide.blue); //And we shoot it
                projectileThree.GetComponent<MegaCannonBallThree>().Launch(StageSide.blue); //And we shoot it
            }
            else
            {
                projectile.GetComponent<MegaCannonBall>().Launch(StageSide.red); //And we shoot it
                projectileTwo.GetComponent<MegaCannonBallSecond>().Launch(StageSide.red); //And we shoot it
                projectileThree.GetComponent<MegaCannonBallThree>().Launch(StageSide.red); //And we shoot it
            }
                
        } 
    }
}
