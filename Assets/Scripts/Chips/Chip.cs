using UnityEngine;
using System.Collections;

public class Chip : MonoBehaviour {

    protected static string RIGHT_HAND_PATH = "Mesh/Dummy/Armature/Torso/UpperArm_R/LowerArm_R/Hand_R"; //Heirachy path to find the right hand object form the root.
    protected static string LEFT_HAND_PATH = "Mesh/Dummy/Armature/Torso/UpperArm_L/LowerArm_L/Hand_L"; //Heirachy path to find the left hand object form the root.
    private float coolDown=40;//seconds
    private float currentCoolDown=0; 
    protected bool isActive = false; //Flag to know if the chip had been activated
    protected bool isFixed = false; //Flag to know if the chip shuouldn't be destroyed because is a fixed ability 

    protected string _chipName = "Chip"; //Name or stringTag of the chip
    public float getCoolDown(){
        return this.coolDown;
    }
    public float getCurrentCoolDown(){
        return this.currentCoolDown;
    }
    public void setCoolDown(float newCoolDown){
        this.coolDown=newCoolDown;
    }
    public void setCurrentCoolDown(float newCoolDown){
        this.coolDown=newCoolDown;
    }
    public string ChipName //Property to be read from other scripts 
    {
        get
        {
            return _chipName;
        }
    }

    protected string _chipPrefabName = "ChipPrefab"; //Name of the prefab
    public string ChipPrefabName ////Property to be read from other scripts 
    {
        get
        {
            return _chipPrefabName;
        }
    }

    protected int _animation; //Animation to be played by the character
    public int Animation
    {
        get
        {
            return _animation;
        }
    }

    protected int chipAttk = 0;
    public int ChipAttk
    {
        get
        {
            return chipAttk;
        }
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(isActive){
            if(currentCoolDown>0){
                currentCoolDown-=Time.deltaTime;
            }
            else if(currentCoolDown!=0){
                currentCoolDown=0;
            }
        }
	
	}

    /// <summary>
    /// Use this to activate the chip
    /// </summary>
    virtual public void Activate()
    {
        if (!isActive&&currentCoolDown==0) //To prevent using the chip multiple times and spaming
        {
            currentCoolDown=coolDown;
            //Debug.Log("Double Shot Activated");
            isActive = true;
            foreach (GameObject element in GameObject.FindGameObjectsWithTag("Player"))
            //We search for every "animationController" objects in the scene
            {
                if (element.transform.root == this.transform.root)
                {
                    Debug.Log("Animation Controller found");
                    //we select the one inside our hierchy
                    element.GetComponent<Character>().eventReportChipActivation(_animation);
                    //and tell it to play the corresponding animation 
                }
            }
        }
    }

    /// <summary>
    /// Destroy the current chip
    /// </summary>
    public void KillSelf()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Function used to detroy the chip when the custom windows pops-up and the chip needs to be deleted
    /// </summary>
    public void FlushChip()
    {
        if (!isActive && !isFixed)
        {
            //Active chips will be destroyed when the animation ends.
            KillSelf();
        }
    }

    public void OnChipAnimationFinish()
    {
        if (isActive)
        {
            KillSelf();
        }
    }

    virtual public bool IsReady()
    {
        if (!isActive)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
