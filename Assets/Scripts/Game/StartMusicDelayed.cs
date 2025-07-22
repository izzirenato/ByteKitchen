using UnityEngine;

public class StartMusicDelayed : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private float _delaySeconds = 4f;

    private void Start()
    {
        Invoke(nameof(PlayMusic), _delaySeconds);
    }

    private void PlayMusic()
    {
        if (_musicSource != null)
        {
            _musicSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource non assegnato!");
        }
    }
}

