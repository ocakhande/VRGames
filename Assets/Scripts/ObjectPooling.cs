using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;

    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> PolledObjects;
        public GameObject objectPrefab;
        public int poolsize;
    }

    [SerializeField] public Pool[] pools = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].PolledObjects = new Queue<GameObject>();
            for (int j = 0; j < pools[i].poolsize; j++)
            {
                GameObject obj = Instantiate(pools[i].objectPrefab);
                obj.SetActive(false);
                pools[i].PolledObjects.Enqueue(obj);
            }
        }
    }

    public GameObject GetPoolObject(int objectType)
    {
        if (objectType >= pools.Length) return null;
        if (pools[objectType].PolledObjects.Count == 0)
        {
            AddSizePool(3, objectType);
        }

        GameObject obj = pools[objectType].PolledObjects.Dequeue();
        obj.SetActive(true);
        return obj;

    }

    public void SetPoolObject(GameObject pooledObject, int objectType)
    {
        if (objectType >= pools.Length) return;
        pools[objectType].PolledObjects.Enqueue(pooledObject);
        pooledObject.SetActive(false);

    }

    public void AddSizePool(float amount, int objectType)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(pools[objectType].objectPrefab);
            obj.SetActive(false);
            pools[i].PolledObjects.Enqueue(obj);

        }
    }



}
