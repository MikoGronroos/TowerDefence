using UnityEngine;
using Photon.Pun;

public class SoundEffectManager : MonoBehaviourSingletonDontDestroyOnLoad<SoundEffectManager>
{

    [SerializeField] private SoundEffect[] SoundEffects;

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    public void PlaySoundInstantlyWithID(string id, bool networked)
    {

        if (networked)
        {
            _photonView.RPC("RPCPlaySoundEffect", RpcTarget.All, id);
        }
        else
        {
            GameObject source = new GameObject("SoundEffect");
            AudioSource audioSource = source.AddComponent<AudioSource>();
            var sound = GetSoundEffectClipWithID(id);
            Destroy(source, sound.length);
            audioSource.PlayOneShot(sound);
        }
    }

    [PunRPC]
    private void RPCPlaySoundEffect(string id)
    {
        GameObject source = new GameObject("SoundEffect");
        AudioSource audioSource = source.AddComponent<AudioSource>();
        var sound = GetSoundEffectClipWithID(id);
        Destroy(source, sound.length);
        audioSource.PlayOneShot(sound);
    }

    private AudioClip GetSoundEffectClipWithID(string id)
    {
        foreach (var sound in SoundEffects)
        {
            if (sound.EffectID == id)
            {
                return sound.EffectClip;
            }
        }
        return null;
    }

}

[System.Serializable]
public class SoundEffect
{
    public string EffectID;
    public AudioClip EffectClip;
}