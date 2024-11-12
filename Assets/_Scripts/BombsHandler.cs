using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BombsHandler : MonoBehaviour
{
    public bool HasABomb { get;private set; }

    GameObject _bombPickup;

    public void PickupBomb(GameObject bombPickup)
    {
        HasABomb = true;
        _bombPickup = bombPickup;
    }

    public void DeployBomb()
    {
        if (!HasABomb) return;
        GameObject bomb = PoolManager.Instance.AccessPool(Pools.Bomb).TakeFromPoolAtPos(new Vector2(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y)));
        bomb.GetComponent<Bomb>().Pickup = _bombPickup;
        HasABomb = false;
    }
}
