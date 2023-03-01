using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Pool
{
     public Queue<GameObject> PoolObjects;

     public int poolLength;
}

public class ObjectPooling : MonoBehaviour
{

     public Pool[] Pools;
     public GameObject ObjectPrefab;


     private void Awake()
     {
          TryGetComponent<Manager>(out Manager manager);

          if (manager != null) ObjectPrefab = manager.playerManager.Bullet;
          for (int i = 0; i < Pools.Length; i++)
          {
               Pools[i].PoolObjects = new Queue<GameObject>();

               AddObject(Pools[i].poolLength, i);
          }
     }

     public GameObject GetObjectPooling(int number)
     {
          if (number >= Pools.Length) return null;

          if (Pools[number].PoolObjects.Count == 0) AddObject(5, number);

          GameObject obj = Pools[number].PoolObjects.Dequeue();

          if (obj == null) return null;

          obj.SetActive(true);
          return obj;
     }

     public void SetObjectPool(GameObject obj, int number)
     {
          if (number >= Pools.Length) return;
          Pools[number].PoolObjects.Enqueue(obj);
          obj.SetActive(false);

     }

     private void AddObject(int amount, int number)
     {
          for (int i = 0; i < amount; i++)
          {
               GameObject obj = Instantiate(ObjectPrefab);
               obj.transform.parent = transform;
               obj.SetActive(false);
               Pools[number].PoolObjects.Enqueue(obj);
          }
     }
}
