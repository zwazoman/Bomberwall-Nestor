using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public event Action OnPlant;

    [HideInInspector] public Vector2 MoveDirection;

    BombsHandler _bombHandler;

    private void Awake()
    {
        TryGetComponent<BombsHandler>(out _bombHandler);
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();
    }

    public void PlantInput(InputAction.CallbackContext context)
    {
        _bombHandler.DeployBomb();
    }
}
