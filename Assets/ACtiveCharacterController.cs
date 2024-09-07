using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACtiveCharacterController : MonoBehaviour
{
    public Character Character1;
    public Character Character2;
    public Character ActiveCharacter;
    public Vector3 Movement;
    private void Start()
    {
        SwitchActiveCharacterTo(Character1);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ActiveCharacter.Move(Movement);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchActiveCharacterTo(Character1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchActiveCharacterTo(Character2);
    }
    public void SwitchActiveCharacterTo(Character newController)
    {
        ActiveCharacter!.enabled = false;
        newController.enabled = true;
        ActiveCharacter = newController;
    }
}
