using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10;
    [SerializeField] LayerMask _mask;

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
        if (_inputs.MoveDirection == Vector2.zero || Physics2D.Raycast(transform.position, _inputs.MoveDirection, 1, _mask.value)) return;
        if (_currentTask == null || _currentTask.IsCompleted)
        {
            Vector2 targetPos = (Vector2)transform.position + _inputs.MoveDirection;
            targetPos = new Vector2(Mathf.Round(targetPos.x), Mathf.Round(targetPos.y));
            _currentTask = StartMoving(targetPos);
        }
    }

    async Task StartMoving(Vector2 targetPos)
    {
        Vector2 offset = targetPos - (Vector2)transform.position;
        Quaternion targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg);
        transform.rotation = targetRotation;
        while ((Vector2)transform.position != targetPos)
        {
            Vector2 offset2 = targetPos - (Vector2)transform.position;
            offset2 = Vector2.ClampMagnitude(offset2, Time.deltaTime * _moveSpeed);
            transform.Translate(offset2 ,Space.World);
            await Task.Yield();
        }
    }
}
