using UnityEngine;
using System.Collections;

public class Heal150 : Chip
{

    Transform projectilePoint;

    /// <summary>
    /// Class constructor
    /// </summary>
    public Heal150()
    {
        _chipName = "#Heal150";
        _chipPrefabName = "Heal150";
        _animation = (int)ChipAnimations.Buff;
    }

    void OnHitFrame()
    {
        if (isActive)
        {
            GameObject player = transform.root.gameObject;
            player.SendMessage("heal", 150);
        }
    }

}
