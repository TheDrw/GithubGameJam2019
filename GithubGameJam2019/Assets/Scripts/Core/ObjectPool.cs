using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drw.Core
{
    /// <summary>
    /// an article on a typesafe object pool if you don't understand things
    /// https://www.gamasutra.com/blogs/SamIzzo/20180611/319671/Typesafe_object_pool_for_Unity.php
    /// I used a queue because it is easier, initially
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPool<T> : MonoBehaviour where T: MonoBehaviour
    {
        [SerializeField] int poolSize = 10;
        [SerializeField] int resizeAmount = 5;
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
            if (objectPool.Peek().isActiveAndEnabled) AddMoreObjectsToPool();

            var temp = objectPool.Dequeue();
            objectPool.Enqueue(temp);
            return temp? temp : null;
        }

        void AddMoreObjectsToPool()
        {
            Debug.Log($"{this} POOL GOT EMPTY - ADDING MORE TO POOL");
            var resizedQueue = new Queue<T>();

            // add by resizeAmount to the pool
            for (int i = 0; i < resizeAmount; i++)
            {
                var temp = Instantiate(prefab, transform);
                temp.gameObject.SetActive(false);
                resizedQueue.Enqueue(temp);
            }

            // replace prev pool with new resized pool
            // not sure if foreach causes memory problems still
            foreach(var obj in objectPool)
            {
                resizedQueue.Enqueue(obj);
            }

            objectPool = resizedQueue;
            print($"new pool size {objectPool.Count}");
        }
    }
}