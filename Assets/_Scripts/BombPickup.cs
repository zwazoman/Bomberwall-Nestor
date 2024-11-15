using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent<BombsHandler>(out BombsHandler playerBombHandler))
        {
            PoolManager.Instance.AccessPool(Pools.BombPickup).ReturnToPool(gameObject);
            God.Instance.BombPickups.Remove(this);
            playerBombHandler.PickupBomb(gameObject);
        }
    }
}
