using UnityEngine;

public class SoundEffectManager : MonoBehaviourSingletonDontDestroyOnLoad<SoundEffectManager>
{

    [SerializeField] private SoundEffect[] SoundEffects;

    public void PlaySoundInstantlyWithID(string id)
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