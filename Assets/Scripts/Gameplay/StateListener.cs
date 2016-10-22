using UnityEngine;
using System.Collections;

public class StateListener : MonoBehaviour {

    public delegate void BstateChange(BattleState battleState);
    public static event BstateChange OnBattleStateChanged;

    Gamestate gamestate;
    BattleState battleState;

    void Awake() {
        gamestate = this.GetComponent<Gamestate>();
        battleState = BattleState.battleStart;
    }

    void Update()
    {
        if (OnBattleStateChanged != null) {
            if (gamestate.CurrentBattleState != battleState) {
                battleState = gamestate.CurrentBattleState;
                OnBattleStateChanged(battleState);
            }
        }
    }
}
