using UnityEngine;
using System.Collections;

public class Chip : MonoBehaviour {

    protected string chipName = "Default";
    public string ChipName
    {
        get
        {
            return chipName;
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

}
