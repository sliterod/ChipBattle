public enum PlayerStates
{
    idle, //Default state
    moving, //The character is moving
    usingChip, //The character is in the middle of the chip animation thus it can't move or use another chip
    takingDamage, //The character is in the middle of the damage animation thus it can't move or use chip
    dead //The character is dead so it can do nothing (maybe it can wait for a miraculous resurrection :P)
}
