using UnityEngine;
using System.Collections;

public class Cannon : Chip {

    /// <summary>
    /// Class constructor
    /// </summary>
    public Cannon()
    {
        _chipName = "#Cannon";
        _chipPrefabName = "Cannon";
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
    public new void Activate()
    {
        GameObject projectile = Instantiate(Resources.Load("Projectiles/CannonBall", typeof(GameObject))) as GameObject;
        //We take the projectile form the resources
        projectile.transform.position = this.transform.position; //Put it into position
        projectile.GetComponent<CannonBall>().Launch(StageSide.blue); //And we shoot it

        // This is a temporal solution, the chip shouldn't be destroyed before the amimation ends
        KillSelf();
    }
}
