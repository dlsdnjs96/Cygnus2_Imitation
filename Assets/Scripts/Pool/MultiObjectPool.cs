using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MultiObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{

    [SerializeField] GameObject parentObj;

    [SerializeField] protected List<T> prefabsList;
    Dictionary<string, T> prefabs;
    Dictionary<string, Queue<T>> pools;

    public static MultiObjectPool<T> instance;




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
        Load();
    }

    private void Load()
    {
        pools = new Dictionary<string, Queue<T>>();
        prefabs = new Dictionary<string, T>();

        foreach (T obj in prefabsList)
        {
            pools[obj.name] = new Queue<T>();
            prefabs[obj.name] = obj;

            for (int i = 0; i < 30; i++)
                pools[obj.name].Enqueue(Create(obj.name));
        }
    }

    public T Create(string _objName)
    {
        T newObj = Instantiate(prefabs[_objName], transform, false).GetComponent<T>();
        newObj.name = _objName;
        newObj.gameObject.SetActive(false);
        return newObj;
    }



    public T GetObject(string _objName)
    {
        if (pools[_objName] == null) return null;

        if (pools[_objName].Count > 0)
        {
            var obj = pools[_objName].Dequeue();
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(parentObj.transform, false);
            return obj;
        }
        else
        {
            var newObj = Create(_objName);
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(parentObj.transform, false);
            return newObj;
        }
    }

    public void Set(string _objName, int _count)
    {
        while (pools[_objName].Count < _count)
            pools[_objName].Enqueue(Create(_objName));
    }

    public void Return(T obj)
    {
        obj.transform.SetParent(transform, false);
        obj.gameObject.SetActive(false);
        pools[obj.name].Enqueue(obj);
    }
    public void Clear(string _objName)
    {
        foreach (T obj in pools[_objName])
            obj.gameObject.SetActive(false);
    }

    public void ReturnAll()
    {
        foreach (T obj in transform.GetComponentsInChildren<T>())
        {
            if (obj.gameObject.activeSelf)
                Return(obj);
        }
    }

    public void ReturnWithTag(string _tag)
    {
        foreach (T obj in transform.GetComponentsInChildren<T>())
        {
            if (obj.gameObject.activeSelf && obj.CompareTag(_tag))
                Return(obj);
        }
    }
    public int GetObjectsCount()
    {
        return prefabsList.Count;
    }
}
