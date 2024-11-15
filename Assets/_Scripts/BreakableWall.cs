using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakableWall : MonoBehaviour
{
    public UnityEvent OnWallBroken;

    Damage _dmg;

    private void Awake()
    {
        TryGetComponent<Damage>(out _dmg);
    }

    private void Start()
    {
        _dmg.OnDamage += BreakWall;
    }

    void BreakWall()
    {
        GraphMaker.Instance.ActivatePoint(GraphMaker.Instance.PointDict[new Vector2Int((int)transform.position.x, (int)transform.position.y)]);
        //OnWallBroken.Invoke();
        Destroy(gameObject);
    }
}
