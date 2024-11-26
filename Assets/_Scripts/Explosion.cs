using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float _endDelay;

    [SerializeField] LayerMask _mask;

    private void OnEnable()
    {
        StartCoroutine(End());
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.right * transform.localScale.x / 2, Color.red);
        Debug.DrawRay(transform.position, -transform.right * transform.localScale.x / 2, Color.red);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<BreakableWall>(out BreakableWall wall))
        {
            wall.DamageWall();
        }
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(_endDelay);
        PoolManager.Instance.AccessPool(Pools.CrossExplosion).ReturnToPool(transform.parent.gameObject);
    }
}
