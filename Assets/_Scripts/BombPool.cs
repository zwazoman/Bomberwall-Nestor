using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class BombPool : MonoBehaviour
{
    //singleton
    private static BombPool instance;

    public static BombPool Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Bomb Pool");
                instance = go.AddComponent<BombPool>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] int _poolSize;
    [SerializeField] GameObject _prefab;

    Queue<GameObject> _pool = new Queue<GameObject>();

    private void Start()
    {
        for(int i = 0; i < _poolSize; i++)
        {
            GameObject thing = Instantiate(_prefab);
            AddToQueue(thing);
        }
    }

    public void AddToQueue(GameObject thing)
    {
        thing.SetActive(false);
        _pool.Enqueue(thing);
    }

    public void TakeFromQueue(GameObject thing)
    {
        if (!_pool.Contains(thing))
        {
            print("Pas le bon item ou pool vide");
            return;
        }
    }
}
