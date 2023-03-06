using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject[] hoops;
    public int amountToPool = 20;
    private int hoopIndex;
    

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject obj;

        for(int i = 0; i < amountToPool; i++)
        {
            hoopIndex = Random.Range(0, hoops.Length);

            obj = Instantiate(hoops[hoopIndex]);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

    }

    public GameObject GetHoop()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
        
    }
}
