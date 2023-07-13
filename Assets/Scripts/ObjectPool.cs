using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject _prefab;
    private List<GameObject> _objectPool;
    private Transform _parent;

    public ObjectPool(GameObject view, int poolSize, Transform parent)
    {
        _prefab = view;
        _parent = parent;
        _objectPool = new List<GameObject>();
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Object.Instantiate(_prefab, Vector3.zero, Quaternion.identity ,parent);
            obj.SetActive(false);
            _objectPool.Add(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in _objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        
        GameObject newObj = Object.Instantiate(_prefab, Vector3.zero, Quaternion.identity, _parent);
        _objectPool.Add(newObj);
        return newObj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }
}
