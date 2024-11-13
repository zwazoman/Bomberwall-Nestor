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

    public event Action OnSummonBomb;

    [SerializeField] int _maxBombPickups;

    public List<BombPickup> BombPickups = new List<BombPickup>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for(int i =0;  i < _maxBombPickups; i++)
        {
            SummonBombPickup();
        }
    }

    public void SummonBombPickup()
    {
        Vector2 spawnPos = GraphMaker.Instance.ActivePoints[UnityEngine.Random.Range(0,GraphMaker.Instance.ActivePoints.Count)].transform.position;
        GameObject bombPickup = PoolManager.Instance.AccessPool(Pools.BombPickup).TakeFromPoolAtPos(spawnPos);
        BombPickups.Add(bombPickup.GetComponent<BombPickup>());
        OnSummonBomb?.Invoke();
        FindObjectOfType<BotBehaviour>().HandleWaiting(); // event pété
    }
}
