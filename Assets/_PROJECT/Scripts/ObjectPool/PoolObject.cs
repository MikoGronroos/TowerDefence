using UnityEngine;
using System;

namespace Finark.Pooling
{
    [Serializable]
    public class PoolObject
    {

        public int InstanceId;
        public GameObject Go;

        public PoolObject(int instanceId, GameObject go)
        {
            this.InstanceId = instanceId;
            Go = go;
        }
    }
}