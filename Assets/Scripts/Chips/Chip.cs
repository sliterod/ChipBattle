using UnityEngine;
using System.Collections;

public class Chip : MonoBehaviour {

    protected bool isActive = false; //Flag to know if the chip had been activated
    protected bool isFixed = false; //Flag to know if the chip shuouldn't be destroyed because is a fixed ability 

    protected string _chipName = "Chip"; //Name or stringTag of the chip
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
	
	}

    /// <summary>
    /// Use this to activate the chip
    /// </summary>
    virtual public void Activate()
    {

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
