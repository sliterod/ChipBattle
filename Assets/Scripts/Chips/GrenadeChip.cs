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

    /// <summary>
    /// Override of the Chip's Activate function
    /// </summary>
    public override void Activate(int chipSlot)
    {
        projectilePoint = transform.root.Find(RIGHT_HAND_PATH);
        base.Activate(chipSlot);
    }

    void OnHitFrame()
    {
        if (isActive)
        {
            GameObject projectile = Instantiate(Resources.Load("Projectiles/Grenade", typeof(GameObject))) as GameObject;
            //We take the projectile form the resources
            projectile.transform.position = projectilePoint.position; //Put it into position
            //Debug.Log(projectilePoint.position);
            if (transform.root.gameObject.layer == 8)
            {
                projectile.transform.GetChild(0).GetComponent<Grenade>().Launch(StageSide.blue); //And we shoot it
            }
            else
            {
                projectile.transform.GetChild(0).GetComponent<Grenade>().Launch(StageSide.red); //And we shoot it
            }

        }
    }

}
