using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public event Action OnStep;

    [SerializeField] float _moveSpeed = 10;
    [SerializeField] LayerMask _mask;

    [SerializeField] Move _move;

    PlayerInputs _inputs;

    Task _currentTask = null;

    private void Awake()
    {
        TryGetComponent<PlayerInputs>(out _inputs);
    }

    private void Start()
    {
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
    }

    private void Update()
    {
        //à refaire avec les Task en mieux si possible pour arrêter de péter tout
        if (_inputs.MoveDirection == Vector2.zero || Physics2D.Raycast(transform.position, _inputs.MoveDirection, 1, _mask.value) /*|| _inputs.MoveDirection.x != 0 && _inputs.MoveDirection.y != 0*/) return;
        if (_currentTask == null || _currentTask.IsCompleted)
        {
            Vector2 targetPos = (Vector2)transform.position + _inputs.MoveDirection;
            targetPos = new Vector2(Mathf.Round(targetPos.x), Mathf.Round(targetPos.y));
            _currentTask = _move.StartMoving(targetPos,_moveSpeed);
            OnStep?.Invoke();
        }
    }
}
