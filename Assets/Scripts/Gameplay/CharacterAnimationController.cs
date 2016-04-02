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

    public void PlayRunAnim(float HorizontalAxis)
    {
        animator.SetInteger("AnimState", (int)PlayerStates.moving);
        animator.SetFloat("InputH", HorizontalAxis);
    }

    public void PlayIdleAnim()
    {
        animator.SetInteger("AnimState", (int)PlayerStates.idle);
    }

    public void PlayChipAnimation(int animation)
    {
        animator.SetInteger("AnimState", (int)PlayerStates.usingChip);
        animator.SetInteger("ChipAnim", animation);
    }

    void OnHitFrame()
    {
        Debug.Log("hit frame");
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
        Debug.Log("Animation Finish");
        transform.root.SendMessage("OnChipAnimationFinish");
        foreach (GameObject element in GameObject.FindGameObjectsWithTag("Chip"))
        //We search for every "Chip" objects in the scene
        {
            if (element.transform.root == this.transform.root)
            {
                //we select the one inside our hierchy
                element.SendMessage("OnChipAnimationFinish");
                //and tell it to play the corresponding animation 
            }
        }
    }
}
