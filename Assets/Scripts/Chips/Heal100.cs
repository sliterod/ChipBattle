using UnityEngine;
using System.Collections;

public class Heal100 : Chip
{

    Transform projectilePoint;

    /// <summary>
    /// Class constructor
    /// </summary>
    public Heal100()
    {
        _chipName = "#Heal100";
        _chipPrefabName = "Heal100";
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
            player.SendMessage("heal", 100);
        }
    }

}
