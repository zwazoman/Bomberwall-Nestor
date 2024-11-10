using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class God : MonoBehaviour
{
    //singleton
    private static God instance;

    public static God Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("God");
                instance = go.AddComponent<God>();
            }
            return instance;
        }
    }

    public event Action SummonEnnemyBomb;

    [SerializeField] int _maxBombPickups;

    List<GameObject> _ennemyBombPickups = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for(int i =0;  i < _maxBombPickups; i++)
        {
            //summon pour le joueur
            SummonBombPickup(new Vector2(UnityEngine.Random.Range(-16, -8), UnityEngine.Random.Range(-8, 9)));
            //summon pour le bot
            _ennemyBombPickups.Add(SummonBombPickup(new Vector2(UnityEngine.Random.Range(9, 17), UnityEngine.Random.Range(9, -8))));
        }
        SummonEnnemyBomb?.Invoke();
    }

    public GameObject SummonBombPickup(Vector2 spawnPos)
    {
        GameObject bombPickup =  PoolManager.Instance.AccessPool(Pools.BombPickup).TakeFromQueue();
        bombPickup.transform.position = spawnPos;
        return bombPickup;
    }
}
