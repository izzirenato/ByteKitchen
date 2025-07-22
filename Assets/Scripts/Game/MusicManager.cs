
using Consts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] private AudioSource audioSource;

    // Dizionario per associare scene a musiche
    [Serializable]
    public class SceneMusic
    {
        public string sceneName;
        public AudioClip musicClip;
    }

    [SerializeField] private SceneMusic[] sceneMusicList;
    private Dictionary<string, AudioClip> sceneMusicDict;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Ottieni l'AudioSource se non assegnato
            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();

            // Crea il dizionario
            sceneMusicDict = new Dictionary<string, AudioClip>();
            foreach (var sceneMusic in sceneMusicList)
            {
                sceneMusicDict[sceneMusic.sceneName] = sceneMusic.musicClip;
            }

            // Ascolta i cambi di scena
            //registra un evento che viene chiamato automaticamente ogni volta che una nuova scena viene caricata in Unity.
            SceneManager.sceneLoaded += OnSceneLoaded;
            //+= aggiunge la funzione OnSceneLoaded alla lista di funzioni che vengono chiamate

            audioSource.loop = true; // Assicurati che sia sempre in loop
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //StartCoroutine(PlayMusicWithDelay(2.0f));
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cambia musica automaticamente
        if (sceneMusicDict.ContainsKey(scene.name))
        {
            if((scene.name == "LivingRoom" || scene.name == "MiniGame #2" || scene.name == "Bookshelf" || scene.name == "TurnTable" || scene.name == "Photo" || scene.name == SceneNames.Kitchen) && Consts.GameData.currentLivingRoomMusic != null && Consts.GameData.isVinylMusicPlaying)
            {
                PlayMusic(Consts.GameData.currentLivingRoomMusic);
            }
            else if(scene.name == Consts.SceneNames.MainMenu)
            {
                StopMusic();
                StartCoroutine(PlayMusicWithDelay(2.0f)); // Aspetta 2 secondi prima di iniziare la musica nel menu principale
            }
            else
            {
                PlayMusic(sceneMusicDict[scene.name]);
            }

        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        if (audioSource.clip != clip)
        {
            // Se c'è già musica in riproduzione, fade out -> cambio -> fade in
            if (audioSource.isPlaying)
            {
                StartCoroutine(ChangeWithFade(clip));
            }
            else
            {
                // Se non c'è musica, cambia direttamente e fai fade in
                audioSource.clip = clip;
                audioSource.Play();
                StartCoroutine(FadeIn(audioSource.volume, 1f));
            }
        }
        else if (!audioSource.isPlaying)
        {
            // Stessa clip ma non sta suonando
            audioSource.UnPause();
        }
    }

    IEnumerator ChangeWithFade(AudioClip newClip)
    {
        float originalVolume = audioSource.volume;

        // Fade out della musica corrente
        yield return StartCoroutine(FadeOut(0f, 0.2f));

        // Cambia la clip
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in della nuova musica
        yield return StartCoroutine(FadeIn(originalVolume, 0.5f));
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return audioSource.volume;
    }

    public AudioClip GetCurrentClip()
    {
        return audioSource.clip;
    }

    public AudioClip GetClipFromKey(string key)
    {
        if (sceneMusicDict.ContainsKey(key))
        {
            return sceneMusicDict[key];
        }
        return null;
    }

    IEnumerator FadeIn(float targetVolume, float duration)
    {
        float startVolume = audioSource.volume;
        float time = 0f;
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = targetVolume;
    }

    IEnumerator FadeOut(float targetVolume, float duration)
    {
        float startVolume = audioSource.volume;
        float time = 0f;
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = targetVolume;
    }

    IEnumerator PlayMusicWithDelay(float seconds)
    {
        // Aspetta il tempo specificato
        yield return new WaitForSeconds(seconds);

        // Fai partire la musica
        audioSource.Play();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
