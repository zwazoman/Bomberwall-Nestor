using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] int _poolSize;
    [SerializeField] GameObject _object;

    Queue<GameObject> _pool = new Queue<GameObject>();
    List<GameObject> __poolContent = new List<GameObject>();

    private void Start()
    {
        for(int i = 0; i < _poolSize; i++)
        {
            GameObject poolObject = Instantiate(_object);
            poolObject.SetActive(false);
            __poolContent.Add(poolObject);
            AddToQueue(poolObject);
        }
    }

    /// <summary>
    /// returns an object to its pool
    /// </summary>
    /// <param name="objectToReturn"></param>
    public void AddToQueue(GameObject objectToReturn)
    {
        if (!__poolContent.Contains(objectToReturn))
        {
            print("wrong object inserted");
        }
        objectToReturn.SetActive(false);
        _pool.Enqueue(objectToReturn);
    }

    /// <summary>
    /// returns an object removed from the pool and activated
    /// </summary>
    /// <returns></returns>
    public GameObject TakeFromQueue()
    {
        if (_pool.Count == 0)
        {
            print("pool empty");
            return null;
        }
        GameObject poolObject = _pool.Dequeue();
        poolObject.SetActive(true);
        return poolObject;
    }
}
