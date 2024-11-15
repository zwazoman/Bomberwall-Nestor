using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float _endDelay;
    [SerializeField] int _explosionDamage = 1;

    [SerializeField] LayerMask _mask;

    private void OnEnable()
    {
        StartCoroutine(End());
        //List<RaycastHit2D> hits = new List<RaycastHit2D>();
        //hits.Add(Physics2D.Raycast(transform.position, transform.right, transform.localScale.x / 2, _mask));
        //hits.Add(Physics2D.Raycast(transform.position, -transform.right, transform.localScale.x / 2, _mask));
        //foreach (RaycastHit2D hit in hits)
        //{
        //    Damage damage;
        //    hit.collider.gameObject.TryGetComponent<Damage>(out damage);  
        //    damage.ApplyDamage(_explosionDamage);
        //}
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.right * transform.localScale.x / 2, Color.red);
        Debug.DrawRay(transform.position, -transform.right * transform.localScale.x / 2, Color.red);
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(_endDelay);
        PoolManager.Instance.AccessPool(Pools.CrossExplosion).ReturnToPool(transform.parent.gameObject);
    }
}
