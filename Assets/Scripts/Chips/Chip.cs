using UnityEngine;
using System.Collections;

public class Chip : MonoBehaviour {

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
    public void Activate()
    {

    }

    /// <summary>
    /// Destroy the current chip
    /// </summary>
    public void KillSelf()
    {
        Destroy(gameObject);
    }

}
