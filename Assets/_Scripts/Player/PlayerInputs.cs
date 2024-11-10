using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public event Action OnPlant;

    [HideInInspector] public Vector2 MoveDirection;

    public void MoveInput(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();
    }

    public void PlantInput(InputAction.CallbackContext context)
    {
        if (context.started) OnPlant?.Invoke();
    }
}
