using Photon.Pun;
using UnityEngine;

public class EffectGameObject : MonoBehaviour, IPunInstantiateMagicCallback
{
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        var photonView = gameObject.GetPhotonView();
        object[] data = photonView.InstantiationData;
        if (data != null && data.Length == 1)
        {
            var instanceId = (int)data[0];
            transform.parent = UnitSpawner.Instance.GetUnitFromPoolWithInstanceID(instanceId).transform;
        }
    }
}
