using System.Collections.Generic;
using UnityEngine;
using System;

namespace Finark.Pooling
{
    [Serializable]
    public class PoolList
    {

        [SerializeField] private Queue<GameObject> poolableItems = new Queue<GameObject>();

        public void Enqueue(GameObject item)
        {
            poolableItems.Enqueue(item);
        }

        public GameObject Dequeue()
        {
            return poolableItems.Dequeue();
        }

        public bool IsEmpty()
        {
            return poolableItems.Count <= 0;
        }


    }
}
