using UnityEngine;
using System.Collections;

public class CharacterAnimationController : MonoBehaviour {

    private Animator animator;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}


    /// <summary>
    /// Changes the animation state
    /// </summary>
    /// <param name="newState">The new state</param>
    private void ChangeAnimState(PlayerStates newState)
    {
        animator.SetInteger("AnimState", (int)newState);
    }

    public void PlayRunAnim(float HorizontalAxis)
    {
        ChangeAnimState(PlayerStates.moving);
        animator.SetFloat("InputH", HorizontalAxis);
    }

    public void PlayIdleAnim()
    {
        ChangeAnimState(PlayerStates.idle);
    }

    public void PlayChipAnimation(int animation)
    {
        ChangeAnimState(PlayerStates.usingChip);
        animator.SetInteger("ChipAnim", animation);
    }

    public void PlayDamageAnimation()
    {
        ChangeAnimState(PlayerStates.takingDamage);
    }

    void OnHitFrame()
    {
        foreach (GameObject element in GameObject.FindGameObjectsWithTag("Chip"))
        //We search for every "Chip" objects in the scene
        {
            if (element.transform.root == this.transform.root)
            {
                //we select the one inside our hierchy
                element.SendMessage("OnHitFrame");
                //and tell it to play the corresponding animation 
            }
        }
    }

    void OnChipAnimationFinish()
    {
        transform.root.SendMessage("OnChipAnimationFinish");
        foreach (GameObject element in GameObject.FindGameObjectsWithTag("Chip"))
        //We search for every "Chip" objects in the scene
        {
            if (element.transform.root == this.transform.root)
            {
                //we select the one inside our hierchy
                element.SendMessage("OnChipAnimationFinish");
                ChangeAnimState(PlayerStates.idle);
                //and tell it to play the corresponding animation 
            }
        }
    }

    void OnDamageAnimationFisnish()
    {
        transform.root.SendMessage("OnDamageAnimationFisnish"); //We report it to the Character Control
        ChangeAnimState(PlayerStates.idle);
    }
}
