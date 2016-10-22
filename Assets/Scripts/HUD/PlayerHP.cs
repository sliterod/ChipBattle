using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHP : MonoBehaviour {

    //Objects
    public Transform playerHP;              //Canvas component. Contains player HP to be displayed

    //Control variables
    bool canShowPlayerHp;                   //Can the HP canvas component be shown?

	// Update is called once per frame
	void Update () {
        if (canShowPlayerHp)
            UpdateHpPosition();	
	}

    /// <summary>
    /// Allows the update method to change HP current screen position.
    /// One time only use.
    /// </summary>
    void EnableHpUpdate() {
        canShowPlayerHp = true;
    }

    /// <summary>
    /// Updates player remaining HP text
    /// </summary>
    /// <param name="hp">HP to be displayed</param>
    void UpdateHpValue(int hp) {
        playerHP.GetComponent<Text>().text = hp.ToString();
    }

    /// <summary>
    /// Changes player HP position to current Transform position
    /// </summary>
    void UpdateHpPosition()
    {
        if (playerHP.gameObject.activeSelf)
        {
            Vector3 hpNewPosition = new Vector3(this.transform.position.x,
                                                this.transform.position.y + 1.6f,
                                                this.transform.position.z);

            Vector3 hpScreenPosition = Camera.main.WorldToScreenPoint(hpNewPosition);

            playerHP.position = hpScreenPosition;
        }
    }

    /// <summary>
    /// Enables / disables canvas render during selection screen BattleState
    /// </summary>
    /// <param name="state">Enabled state of Canvas</param>
    void RenderHpCanvas(bool state) {
        //Player HP
        this.transform
            .FindChild("CanvasHP")
            .GetComponent<Canvas>()
            .enabled = state;
    }
}
