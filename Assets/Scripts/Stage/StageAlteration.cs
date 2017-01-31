using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageAlteration : MonoBehaviour {

    /**************** STAGE ELEMENTS ******************/
    public Transform centerLimit;       //Area divider
    public Transform blueArea;          //Player 1 battle area
    public Transform redArea;           //Player 2 battle area

    /**************** SCALES ******************/
    Vector3 increasedAreaSize;          //Size of the area once a piece was added
    Vector3 reducedAreaSize;            //Size of the area once a piece was stolen

    /**************** POSITIONS ******************/
    Vector3 blueLimitPosition;
    Vector3 redLimitPosition;

    // Use this for initialization
    void Start () {
        increasedAreaSize = new Vector3 (1.5f, 1.0f, 1.0f);
        reducedAreaSize = new Vector3(0.5f, 1.0f, 1.0f);

        blueLimitPosition = new Vector3 (10.0f, 5.0f, 0.0f);
        redLimitPosition = new Vector3(-10.0f, 5.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
            AreaSteal(Player.player1);
        }
        if (Input.GetMouseButtonDown(1))
        {
            AreaSteal(Player.player2);
        }
    }

    /// <summary>
    /// Steals a big portion of the battle area.
    /// It is restored gradually
    /// </summary>
    /// <param name="currentPlayer">The player who used the chip</param>
    void AreaSteal(Player currentPlayer) {
        switch (currentPlayer) {
            case Player.player1:
                blueArea.transform.localScale = increasedAreaSize;
                redArea.transform.localScale = reducedAreaSize;
                centerLimit.transform.localPosition = blueLimitPosition;

                PushPlayerBack(centerLimit.localPosition.x);

                iTweenEvent.GetEvent(redArea.gameObject, "Restore").Play();
                iTweenEvent.GetEvent(centerLimit.gameObject, "Restore").Play();
                break;

            case Player.player2: case Player.ai:
                blueArea.transform.localScale = reducedAreaSize;
                redArea.transform.localScale = increasedAreaSize;
                centerLimit.transform.localPosition = redLimitPosition;

                PushPlayerBack(centerLimit.localPosition.x);

                iTweenEvent.GetEvent(blueArea.gameObject, "Restore").Play();
                iTweenEvent.GetEvent(centerLimit.gameObject, "Restore").Play();
                break;
        }
    }

    /// <summary>
    /// Pushes player to a different position after the
    /// area steal effect has been applied.
    /// </summary>
    /// <param name="centerLimitX"></param>
    void PushPlayerBack(float centerLimitX) {

        Transform player;

        if (centerLimitX > 0.0f)
        {
            player = GameObject.Find("Player2").transform;

            player
                .transform
                .position = new Vector3(centerLimitX + 5.0f,
                                            player.transform.position.y,
                                            player.transform.position.z);

        }
        else
        {
            player = GameObject.Find("Player1").transform;

            player
                .transform
                .position = new Vector3(centerLimitX - 5.0f,
                                            player.transform.position.y,
                                            player.transform.position.z);
        }
    }
}
