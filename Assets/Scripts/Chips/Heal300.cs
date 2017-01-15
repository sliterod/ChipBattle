using UnityEngine;
using System.Collections;

public class Heal300 : Chip
{

    Transform projectilePoint;

    /// <summary>
    /// Class constructor
    /// </summary>
    public Heal300()
    {
        _chipName = "#Heal300";
        _chipPrefabName = "Heal300";
        _animation = (int)ChipAnimations.Buff;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnHitFrame()
    {
        if (isActive)
        {
            GameObject player = transform.root.gameObject;
            player.SendMessage("heal", 300);
        }
    }

}
