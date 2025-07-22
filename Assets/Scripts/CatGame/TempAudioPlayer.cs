using UnityEngine;

public class TempAudioPlayer : MonoBehaviour
{
    public void PlayAndDestroy(AudioClip clip, float volume = 0.5f)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        Destroy(gameObject, clip.length); 
    }
}
