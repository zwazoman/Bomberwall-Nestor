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
        if (God.Instance.PlayerBombPickups.Contains(Pickup)) God.Instance.SummonPlayerBombPickup(); else God.Instance.SummonBotBombPickup();
        PoolManager.Instance.AccessPool(Pools.CrossExplosion).TakeFromPoolAtPos(transform.position);
    }
}
