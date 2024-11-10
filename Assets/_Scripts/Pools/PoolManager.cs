using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public enum Pools
{
    BombPickup,
    Bomb,
    CrossExplosion
}

public class PoolManager : MonoBehaviour
{
    //singleton
    private static PoolManager instance;

    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Pool Manager");
                instance = go.AddComponent<PoolManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }


    [SerializeField] List<ObjectPool> _poolsList = new List<ObjectPool>();

    public ObjectPool AccessPool(Pools choosenPool)
    {
        return _poolsList[(int)choosenPool];
    }
}
