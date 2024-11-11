using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("connard");

        if (collision.gameObject.TryGetComponent<PlayerBombsHandler>(out PlayerBombsHandler playerBombHandler))
        {
            print("player picked up");
            if (playerBombHandler.HasABomb) return;
            PoolManager.Instance.AccessPool(Pools.BombPickup).ReturnToPool(gameObject);
            playerBombHandler.PickupBomb(gameObject);
        }
    }
}
