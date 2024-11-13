using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
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
        Destroy(gameObject);
        Time.timeScale = 0;
        //mettre a jour le graph
    }
}
