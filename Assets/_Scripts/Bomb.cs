using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float _explosionCountDown;

    [HideInInspector] public GameObject Pickup;

    private void OnEnable()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(_explosionCountDown);
        PoolManager.Instance.AccessPool(Pools.Bomb).ReturnToPool(gameObject);
        BombPickup bombPickup = Pickup.GetComponent<BombPickup>();
        God.Instance.SummonBombPickup();
        PoolManager.Instance.AccessPool(Pools.CrossExplosion).TakeFromPoolAtPos(transform.position);
    }
}
