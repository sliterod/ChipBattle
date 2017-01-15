using UnityEngine;
using System.Collections;

public class StunGrenade : Chip {

    Transform projectilePoint;

    /// <summary>
    /// Class constructor
    /// </summary>
    public StunGrenade()
    {
        _chipName = "#StunGrenade";
        _chipPrefabName = "StunGrenade";
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
        projectilePoint = transform.root.Find(RIGHT_HAND_PATH);
        base.Activate();
    }

    void OnHitFrame()
    {
        if (isActive)
        {
            GameObject projectile = Instantiate(Resources.Load("Projectiles/StunGrenade", typeof(GameObject))) as GameObject;
            //We take the projectile form the resources
            projectile.transform.position = projectilePoint.position; //Put it into position
            Debug.Log(projectilePoint.position);
            if(transform.root.gameObject.layer == 8)
            {
                projectile.transform.GetChild(0).GetComponent<StunGrenadeProjectil>().Launch(StageSide.blue); //And we shoot it
            }
            else
            {
                projectile.transform.GetChild(0).GetComponent<StunGrenadeProjectil>().Launch(StageSide.red); //And we shoot it
            }
            
        }
    }

}
