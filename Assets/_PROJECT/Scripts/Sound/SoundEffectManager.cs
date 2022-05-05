using UnityEngine;

public class SoundEffectManager : MonoBehaviourSingletonDontDestroyOnLoad<SoundEffectManager>
{

    [SerializeField] private SoundEffect[] SoundEffects;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundInstantlyWithID(string id)
    {
        _audioSource.PlayOneShot(GetSoundEffectClipWithID(id));
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