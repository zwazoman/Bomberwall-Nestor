using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakableWall : MonoBehaviour
{
    public UnityEvent OnWallBroken;

    [SerializeField] int _maxHP;

    float _currentHP;

    private void Awake()
    {
        _currentHP = _maxHP;
    }

    public void DamageWall(int damage)
    {
        print("damageWall");
        _currentHP -= damage;
        if (_currentHP <= 0) BreakWall();
    }

    public void BreakWall()
    {
        GraphMaker.Instance.ActivatePoint(GraphMaker.Instance.PointDict[new Vector2Int((int)transform.position.x, (int)transform.position.y)]);
        OnWallBroken.Invoke();
        Destroy(gameObject);
        Time.timeScale = 0;
        //mettre a jour le graph
    }
}
