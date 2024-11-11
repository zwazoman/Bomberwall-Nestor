using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBombsHandler : MonoBehaviour
{
    public bool HasABomb { get;private set; }

    GameObject _bombPickup;

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
        _bombPickup = bombPickup;
    }

    void DeployBomb()
    {
        if (!HasABomb) return;
        GameObject bomb = PoolManager.Instance.AccessPool(Pools.Bomb).TakeFromPoolAtPos(new Vector2(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y)));
        bomb.GetComponent<Bomb>().Pickup = _bombPickup;
        HasABomb = false;
    }
}
