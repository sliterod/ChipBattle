using UnityEngine;
using System.Collections;

public class CrackedPanel : MonoBehaviour
{
    public Material emptyPanelMaterial;
    public MeshRenderer emptyPanel;
    
    void OnTriggerExit(Collider other) {
        Debug.Log("Cracking panels, activating colliders, changing texture");

        //Turning panel into a hole
        this.GetComponent<BoxCollider>().isTrigger = false;

        //Changing texture
        emptyPanel.material = emptyPanelMaterial;
    }
}
