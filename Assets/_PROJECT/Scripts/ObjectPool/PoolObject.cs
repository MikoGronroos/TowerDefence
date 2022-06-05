using UnityEngine;
using System;

namespace Finark.Pooling
{
    [Serializable]
    public class PoolObject
    {

        public int instanceId;
        public GameObject Go;

        public PoolObject(int instanceId, GameObject go)
        {
            this.instanceId = instanceId;
            Go = go;
        }
    }
}