using UnityEngine;
using System.Collections;

public class Heal300Chip : Chip
{

    Transform projectilePoint;

    /// <summary>
    /// Class constructor
    /// </summary>
    public Heal300Chip()
    {
        _chipName = "#Heal300Chip";
        _chipPrefabName = "Heal300";
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
            GameObject player = transform.root.gameObject;
            isActive = true;
            foreach (GameObject element in GameObject.FindGameObjectsWithTag("AnimationController"))
            //We search for every "animationController" objects in the scene
            {
                if (element.transform.root == this.transform.root)
                {
                    //Debug.Log("Animation Controller found");
                    //we select the one inside our hierchy
                    element.GetComponent<CharacterAnimationController>().PlayChipAnimation(Animation);
                    //and tell it to play the corresponding animation 
                }
            }

        }
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
