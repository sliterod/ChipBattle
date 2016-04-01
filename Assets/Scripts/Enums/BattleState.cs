public enum BattleState {
    selectionScreen,    //Card selection screen
    battle,             //Ongoing battle
    results,            //Battle end, results screen
    pause,              //Game paused
    standby,            //Enemy appereance, waiting for opponent
    battleStart         //One time use, battle is being setup
}
