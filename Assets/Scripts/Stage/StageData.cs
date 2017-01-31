using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour {

    public Transform blueArea;
    public Transform redArea;

    /// <summary>
    /// Local scale of blue battle area.
    /// </summary>
    /// <returns>A Vector with the current size of the area (local values)</returns>
    public Vector3 GetBlueAreaLocalScale()
    {
        return blueArea.localScale;
    }

    /// <summary>
    /// Local scale of red battle area.
    /// </summary>
    /// <returns>A Vector with the current size of the area (local values)</returns>
    public Vector3 GetRedAreaLocalScale()
    {
        return redArea.localScale;
    }

    /// <summary>
    /// Min boundary line of the red area
    /// </summary>
    /// <returns>A vector with the coordinates of the boundary</returns>
    public Vector3 GetRedAreaMinBounds()
    {
        return redArea.GetComponent<Collider>().bounds.min;
    }

    /// <summary>
    /// Max boundary line of the red area. 
    /// </summary>
    /// <returns>A vector with the coordinates of the boundary</returns>
    public Vector3 GetRedAreaMaxBounds()
    {
        return redArea.GetComponent<Collider>().bounds.max;
    }

    /// <summary>
    /// Min boundary line of the red area
    /// </summary>
    /// <returns>A vector with the coordinates of the boundary</returns>
    public Vector3 GetBlueAreaMinBounds()
    {
        return blueArea.GetComponent<Collider>().bounds.min;
    }

    /// <summary>
    /// Max boundary line of the red area. 
    /// </summary>
    /// <returns>A vector with the coordinates of the boundary</returns>
    public Vector3 GetBlueAreaMaxBounds()
    {
        return blueArea.GetComponent<Collider>().bounds.max;
    }
}
