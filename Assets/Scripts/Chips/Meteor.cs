﻿using UnityEngine;
using System.Collections;

public class Meteor : Chip {

    
    Transform projectilePoint;
    AudioSource shotSound;
	int numberOfProjectils=3;
	private GameObject target;
    /// <summary>
    /// Class constructor
    /// </summary>
    public Meteor()
    {
        _chipName = "#Meteor";
        _chipPrefabName = "Meteor";
        _animation = (int)ChipAnimations.SingleShot;
    }

    // Use this for initialization
    void Start () {
        shotSound=GameObject.Find("ChipsSounds/shot").GetComponent<AudioSource>();

		if (transform.root.name == "Player1") {
			target=GameObject.Find("Player2");
		} else {
			target=GameObject.Find("Player1");
		}

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
				Debug.Log("Meteor Activated");
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
						shotSound.PlayDelayed(0);
					}
				}
            
        }
        
    }

    void OnHitFrame()
    {
        if (isActive)
        {
			//DrawLine (projectilePoint.position, target.transform.position, Color.yellow, 1);

				GameObject projectile = Instantiate(Resources.Load("Projectiles/Meteor", typeof(GameObject))) as GameObject;
				//We take the projectile form the resources
				projectile.transform.position = new Vector3(projectilePoint.position.x,30,projectilePoint.position.z); ; //Put it into position
				if (transform.root.gameObject.layer == 8)
				{
				projectile.GetComponent<MeteorController>().Launch(StageSide.blue,target.transform); //And we shoot it
				}
				else
				{
				projectile.GetComponent<MeteorController>().Launch(StageSide.red,target.transform); //And we shoot it
				}	
			
                
        } 
    }
}
