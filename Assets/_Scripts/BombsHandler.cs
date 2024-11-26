using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BombsHandler : MonoBehaviour
{
    public event Action OnBombPickup;
    public event Action OnBombDeployed;

    public bool HasABomb => BombsPossessed > 0;

    [HideInInspector] public int BombsPossessed = 0;

    GameObject _bombPickup;

    public void PickupBomb(GameObject bombPickup)
    {
        BombsPossessed++;
        OnBombPickup?.Invoke();
        _bombPickup = bombPickup;
    }

    public void DeployBomb()
    {
        if (!HasABomb) return;
        OnBombDeployed?.Invoke();
        GameObject bomb = PoolManager.Instance.AccessPool(Pools.Bomb).TakeFromPoolAtPos(new Vector2(Mathf.Round(transform.position.x),Mathf.Round(transform.position.y)));
        bomb.GetComponent<Bomb>().Pickup = _bombPickup;
        BombsPossessed--;
    }
}
