using UnityEngine;
using System.Collections;

public class Heal50 : Chip
{

    Transform projectilePoint;

    /// <summary>
    /// Class constructor
    /// </summary>
    public Heal50()
    {
        _chipName = "#Heal50";
        _chipPrefabName = "Heal50";
        _animation = (int)ChipAnimations.Buff;
    }

    void OnHitFrame()
    {
        if (isActive)
        {
            GameObject player = transform.root.gameObject;
            player.SendMessage("heal", 50);
        }
    }

}
