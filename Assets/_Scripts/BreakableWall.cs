using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] bool _isPlayer;

    [SerializeField] int _hps = 3;


    public void DamageWall()
    {
        _hps--;
        if (_hps <= 0)
        {
            GraphMaker.Instance.ActivatePoint(GraphMaker.Instance.PointDict[new Vector2Int((int)transform.position.x, (int)transform.position.y)]);
            if (_isPlayer) GameManager.Instance.StopGame("The Player"); else GameManager.Instance.StopGame("The AI");
            Destroy(gameObject);
        }
    }
}
