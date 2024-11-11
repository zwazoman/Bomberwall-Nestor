using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float _endDelay;
    [SerializeField] int _explosionDamage = 1;

    private void OnEnable()
    {
        StartCoroutine(End());
        //List<RaycastHit2D> hits = new List<RaycastHit2D>();
        //hits.Add(Physics2D.Raycast(transform.position, transform.right, transform.localScale.x / 2));
        //hits.Add(Physics2D.Raycast(transform.position, -transform.right, transform.localScale.x / 2));
        //foreach (RaycastHit2D hit in hits)
        //{
        //    if (hit.collider == null) continue;
        //    if (hit.collider.gameObject.TryGetComponent<BreakableWall>(out BreakableWall breakableWall)) breakableWall.DamageWall(_explosionDamage);
        //}
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, transform.right * transform.localScale.x / 2, Color.red);
        //Debug.DrawRay(transform.position, -transform.right * transform.localScale.x / 2, Color.red);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<BreakableWall>(out BreakableWall breakableWall) )
        {
            breakableWall.DamageWall(_explosionDamage);
        }
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(_endDelay);
        PoolManager.Instance.AccessPool(Pools.CrossExplosion).ReturnToPool(transform.parent.gameObject);
    }
}
