using UnityEngine;

public class SoundTool : MonoBehaviour
{

    [SerializeField] private string soundID;

	public void PlaySound()
    {
        SoundEffectManager.Instance.PlaySoundInstantlyWithID(soundID, true);
    }

}
