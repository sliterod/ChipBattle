using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour {

    Transform character;

    void Start() {
        character = this.transform;
    }

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
}
