﻿using UnityEngine;
using System.Collections;

public class Rocket : Chip {

    
    Transform projectilePoint;
    AudioSource shotSound;
	int numberOfProjectils=3;
	private GameObject target;
    /// <summary>
    /// Class constructor
    /// </summary>
    public Rocket()
    {
        _chipName = "#Rocket";
        _chipPrefabName = "Rocket";
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
        projectilePoint = transform.root.Find(RIGHT_HAND_PATH);

        if (!isActive) //To prevent using the chip multiple times
        {
            base.Activate();
            shotSound.PlayDelayed(0);           
        }
        
    }

    void OnHitFrame()
    {
        if (isActive)
        {
			//DrawLine (projectilePoint.position, target.transform.position, Color.yellow, 1);

				GameObject projectile = Instantiate(Resources.Load("Projectiles/Rocket", typeof(GameObject))) as GameObject;
				//We take the projectile form the resources
				projectile.transform.position = projectilePoint.position; //Put it into position
				if (transform.root.gameObject.layer == 8)
				{
				projectile.GetComponent<RocketController>().Launch(StageSide.blue,target.transform); //And we shoot it
				}
				else
				{
				projectile.GetComponent<RocketController>().Launch(StageSide.red,target.transform); //And we shoot it
				}             
        } 
    }
}
