using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Core
{
    /// <summary>
    /// an article on a typesafe object pool
    /// https://www.gamasutra.com/blogs/SamIzzo/20180611/319671/Typesafe_object_pool_for_Unity.php
    /// I used a queue because I don't have to worry about some things lists/arrays have
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPool<T> : MonoBehaviour where T: MonoBehaviour
    {
        [SerializeField] int poolSize = 10;
        [SerializeField] T prefab = null;

        Queue<T> objectPool;
        
        void Awake()
        {
            objectPool = new Queue<T>();
            for (int i = 0; i < poolSize; i++)
            {
                var temp = Instantiate(prefab, transform);
                temp.gameObject.SetActive(false);
                objectPool.Enqueue(temp);
            }
        }

        public T GetGameObjectFromPool()
        {
            if (objectPool.Count == 0) AddMoreObjectsToPool();

            var temp = objectPool.Dequeue();
            objectPool.Enqueue(temp);
            return temp? temp : null;
        }

        void AddMoreObjectsToPool()
        {
            Debug.Log($"{this} POOL GOT EMPTY - ADDING MORE TO POOL");
            for (int i = 0; i < 5; i++)
            {
                var temp = Instantiate(prefab, transform);
                temp.gameObject.SetActive(false);
                objectPool.Enqueue(temp);
            }
        }
    }
}