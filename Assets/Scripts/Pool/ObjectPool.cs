using System.Collections.Generic;
using UnityEngine;


public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected GameObject poolingPrefab;
    [SerializeField] protected GameObject parentObj;

    protected Queue<T> pooling = new Queue<T>();

    public static ObjectPool<T> instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            pooling.Enqueue(CreateNewObject());
        }
    }

    public virtual T CreateNewObject()
    {
        var newObj = Instantiate(poolingPrefab, transform, false).GetComponent<T>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    public virtual T GetObject()
    {
        if (pooling.Count > 0)
        {
            var obj = pooling.Dequeue();
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(parentObj.transform, false);
            return obj;
        }
        else
        {
            var newObj = CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(parentObj.transform, false);
            return newObj;
        }
    }

    public void ReturnObject(T obj)
    {
        obj.transform.SetParent(transform, false);
        obj.gameObject.SetActive(false);
        pooling.Enqueue(obj);
    }
}

