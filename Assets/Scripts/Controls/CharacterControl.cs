using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {

    Transform character;
    CharacterController movementController; 
    Character characterState;
    Vector3 movementArray = new Vector3(0, 0, 0);

    void Start() {
        character = this.transform;
        movementController = gameObject.GetComponent<CharacterController>();
        characterState = gameObject.GetComponent<Character>();
    }

    /// <summary>
    /// Move the character using unity's character controller script
    /// it checks if the player is able to move before executing the movement
    /// </summary>
    public void Move(float x_axis,float y_axis)
    {
        if(characterState.CurrentState == PlayerStates.idle || characterState.CurrentState == PlayerStates.moving) {
            //If the character can be moved or is already moving 
            if (x_axis == 0.0f && y_axis == 0.0f) //If the character stop moving
            {
                //We report it to the player status controller
                characterState.eventReportStopMovement();
            }
            else
            {
                characterState.eventReportMovement(x_axis, y_axis); //We report it to the player status controller
                movementArray.x = x_axis;
                movementArray.z = y_axis;
                movementArray *= characterState.movementSpeed;
                movementController.Move(movementArray * Time.deltaTime);

            }
        }

    }

    /// <summary>
    /// Moves character gameobject through mouse clicking
    /// </summary>
    public void MoveCharacterMouse() {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 newPosition = character.position;

        if (Physics.Raycast(ray, out hit))
        {
            newPosition = hit.point;

            //Moving object if raycast position is lower or equal than zero
            if (newPosition.x <= -0.5f)
            {
                if (newPosition.z >= -4.8f && newPosition.z <= 3.5f) {
                    character.position = new Vector3(newPosition.x,
                                                     character.position.y,
                                                     newPosition.z);
                }
            }
        }
    }

    /// <summary>
    /// Moves character through controller and keyboard input
    /// </summary>
    public void MoveCharacter(Movement direction) {

        Vector3 newPosition = Vector3.zero;

        switch (direction) {
            case Movement.up:
                newPosition = new Vector3(character.position.x, character.position.y, character.position.z + 0.1f);
                break;

            case Movement.down:
                newPosition = new Vector3(character.position.x, character.position.y, character.position.z - 0.1f);
                break;

            case Movement.left:
                newPosition = new Vector3(character.position.x - 0.1f, character.position.y, character.position.z);
                break;

            case Movement.right:
                newPosition = new Vector3(character.position.x + 0.1f, character.position.y, character.position.z);
                break;
        }

        //Boundaries
        if (newPosition.x >= -9.0 && newPosition.x <= -0.6) {
            if (newPosition.z >= -4.4 && newPosition.z <= 4.4)
            {
                character.position = newPosition;
            }
        }
        
        Debug.Log(character.position);
    }

    /// <summary>
    /// Tells the character to use the appropiate chip, if that chip is not available it automatically use the default Chip
    /// It also check if Chips are usable in the first place so it can be called any time without worrying about the state of the character
    /// </summary>
    /// <param name="chipSlot">The slot of the chip (1-4) any other number will result in using the default chip </param>
    public void UseChip(int chipSlot)
    {
        if(characterState.CurrentState == PlayerStates.idle || characterState.CurrentState == PlayerStates.moving)
        {
            switch (chipSlot)
            {
                case 1:
                    transform.FindChild("Chip1").GetChild(0).SendMessage("Activate");
                    break;

                default:
                    transform.FindChild("DefaultChip").GetChild(0).SendMessage("Activate");
                    break;
            }


        }
        else
        {
            //Play a sound... maybe
        }
    }


}
