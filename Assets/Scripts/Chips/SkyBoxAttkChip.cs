﻿using UnityEngine;
using System.Collections;

public class SkyBoxAttkChip : Chip
{

    Transform projectilePoint;
    /// <summary>
    /// Class constructor
    /// </summary>
    public SkyBoxAttkChip()
    {
        _chipName = "#SkyBoxAttkChip";
        _chipPrefabName = "SkyBoxAttkChip";
        _animation = (int)ChipAnimations.Launch;
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Override of the Chip's Activate function
    /// </summary>
    public override void Activate()
    {
        if (!isActive) //To prevent using the chip multiple times
        {
            GameObject player1 = GameObject.Find("Player1");
            projectilePoint = new GameObject().transform;
            projectilePoint.position = new Vector3(player1.transform.position.x, player1.transform.position.y + 10, player1.transform.position.z);
            base.Activate();
        }           
    }

    void OnHitFrame()
    {
        if (isActive)
        {
            GameObject player1 = GameObject.Find("Player1");
            GameObject projectile = Instantiate(Resources.Load("Projectiles/SkyBoxAttk", typeof(GameObject))) as GameObject;
            //We take the projectile form the resources
            projectile.transform.position = projectilePoint.position; //Put it into position
            projectilePoint.position = new Vector3(player1.transform.position.x, player1.transform.position.y + 10, player1.transform.position.z);
            if (transform.root.gameObject.layer == 8)
            {
                projectile.transform.GetChild(1).GetComponent<SkyBoxAttk>().Launch(StageSide.blue); //And we shoot it
            }
            else
            {
                projectile.transform.GetChild(1).GetComponent<SkyBoxAttk>().Launch(StageSide.red); //And we shoot it
            }

        }
    }

}
