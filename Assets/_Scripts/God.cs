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

    public List<GameObject> BotBombPickups = new List<GameObject>();
    public List<GameObject> PlayerBombPickups = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for(int i =0;  i < _maxBombPickups; i++)
        {
            SummonBotBombPickup();
            SummonPlayerBombPickup();
        }
    }

    public void SummonPlayerBombPickup()
    {
        Vector2 spawnPos = new Vector2(UnityEngine.Random.Range(-16, -8), UnityEngine.Random.Range(-8, 9));
        GameObject bombPickup =  PoolManager.Instance.AccessPool(Pools.BombPickup).TakeFromPoolAtPos(spawnPos);
        PlayerBombPickups.Add(bombPickup);
    }

    public void SummonBotBombPickup()
    {
        SummonEnnemyBomb?.Invoke(); //update le graph du bot
        Vector2 spawnPos = new Vector2(UnityEngine.Random.Range(9, 17), UnityEngine.Random.Range(9, -8));
        GameObject bombPickup = PoolManager.Instance.AccessPool(Pools.BombPickup).TakeFromPoolAtPos(spawnPos);
        BotBombPickups.Add(bombPickup);
    }
}
