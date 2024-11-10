using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBombsHandler : MonoBehaviour
{
    public bool HasABomb { get;private set; }

    PlayerInputs _playerInput;

    private void Awake()
    {
        TryGetComponent<PlayerInputs>(out _playerInput);
    }

    private void Start()
    {
        _playerInput.OnPlant += DeployBomb;
    }

    public void PickupBomb(GameObject bombPickup)
    {
        HasABomb = true;
        PoolManager.Instance.AccessPool(Pools.BombPickup).AddToQueue(bombPickup);
    }

    void DeployBomb()
    {
        if (!HasABomb) return;
        GameObject bomb = PoolManager.Instance.AccessPool(Pools.Bomb).TakeFromQueue();
        bomb.transform.position = transform.position;
    }
}
