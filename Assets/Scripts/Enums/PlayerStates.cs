public enum PlayerStates
{
    idle = 0, //Default state
    moving = 1, //The character is moving
    usingChip = 2, //The character is in the middle of the chip animation thus it can't move or use another chip
    takingDamage = 3, //The character is in the middle of the damage animation thus it can't move or use chip
    dead = 4//The character is dead so it can do nothing (maybe it can wait for a miraculous resurrection :P)
}
