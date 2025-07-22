using Consts;
using System.Collections;
using UnityEngine;

public class TurnTable : MonoBehaviour
{
    [SerializeField] private Animator _animTurnTable;
    [SerializeField] private Animator _animVinyl;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _robotMessage;

    private bool _isSpinning = false;
    private float _currentAudioTime = 0f;
    private AudioClip _defaultMusic;



    private void Awake()
    {
        if (_animTurnTable == null || _animVinyl == null)
        {
            Debug.LogError("Animator components are not assigned in the TurnTable script.");
        }
        else
        {
            _animTurnTable.speed = 0f; // Start with the turntable stopped
            _animVinyl.speed = 0f; // Start with the vinyl stopped
        }

        if (_robotMessage == null)
        {
            Debug.LogError("Robot message GameObject non collegato");
        }
        else
        {
            _robotMessage.SetActive(false); // Assicurati che il messaggio sia nascosto all'inizio
        }

        if(GameData.isVinylMusicPlaying)
        {
            _animTurnTable.speed = 1f; // Set the turntable speed to normal
            _animTurnTable.SetBool("isOn", true);
            _isSpinning = true;
            _animVinyl.speed = 1.0f;
        }

        _defaultMusic = MusicManager.Instance.GetClipFromKey("LivingRoom"); //Salva la clip base

    }

    private void Start()
    {
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource non assegnato nel TurnTable script.");
        }
        else
        {
            GameData.currentLivingRoomMusic = _audioSource.clip; // Imposta la musica corrente
            Debug.Log("La musica del vinile è già in riproduzione: " + Consts.GameData.currentLivingRoomMusic.name);
        }
    }

    private void OnMouseDown()
    {
        if (!_isSpinning)
        {
            StartTurnTable();

        }
        else
        {
            StopTurnTable();
        }
    }

    IEnumerator AspettaFineAnimazione(string name)
    {
        yield return new WaitUntil(() => _animTurnTable.GetCurrentAnimatorStateInfo(0).IsName(name) && _animTurnTable.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1); // <-- pausa, finché l'animazione non è completata
        Debug.Log("Animazione completata!");

        if (_isSpinning)
        {
            _animVinyl.speed = 1.0f;
            //StartCoroutine(FadeInAudio(1.5f)); // Durata in secondi
            GameData.currentLivingRoomMusic = _audioSource.clip; // Aggiorna la musica corrente
            MusicManager.Instance.PlayMusic(Consts.GameData.currentLivingRoomMusic); // Riprende la musica
            GameData.isVinylMusicPlaying = true; // Imposta la musica come in riproduzione
            _robotMessage.SetActive(true);
        }
        else
        {
            _animVinyl.speed = 0f;
            //_currentAudioTime = _audioSource.time; // Salva la posizione
            //_audioSource.Stop();
            MusicManager.Instance.PlayMusic(_defaultMusic); // Ferma la musica
            GameData.isVinylMusicPlaying = false; // Imposta la musica come non in riproduzione
        }
    }

    IEnumerator FadeInAudio(float duration)
    {
        _audioSource.volume = 0f;
        _audioSource.time = _currentAudioTime; // Riprende da dove era stata interrotta
        _audioSource.Play();

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(0f, 1f, timer / duration);
            yield return null;
        }
        _audioSource.volume = 1f;
    }

    IEnumerator Timer(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _robotMessage.SetActive(false); // Nasconde il messaggio dopo il timer
    }

    public void ChangeMusicClip(AudioClip newClip)
    {
        bool wasPlaying = _audioSource.isPlaying;

        //StopTurnTable(); PROVA!!!

        // Cambia la clip
        _audioSource.clip = newClip;
        _isSpinning = false;
        StartTurnTable(); // Avvia il giradischi

        // Reset del tempo
        _currentAudioTime = 0f;
    }

    public void StartTurnTable()
    {
        if (_isSpinning)
        {
            Debug.Log("Il giradischi è già in funzione.");
            return;
        }
        else
        {
            _animTurnTable.speed = 1f; // Set the turntable speed to normal
            _animTurnTable.SetBool("isOn", true);
            _isSpinning = true;
            StartCoroutine(AspettaFineAnimazione("TurnTableOn")); // fa quello che c'è dentro la coroutine dopo che finisce l'animazione
            Debug.Log("sta girando!");
        }
    }

    public void StopTurnTable()
    {
        _robotMessage.SetActive(false);

        if (_isSpinning)
        {
            _animTurnTable.SetBool("isOn", false);
            _isSpinning = false;
            StartCoroutine(AspettaFineAnimazione("TurnTableOff")); // fa quello che c'è dentro la coroutine dopo che finisce l'animazione
            Debug.Log("si è fermato!");
        }
        else
        {
            Debug.Log("Il giradischi è già fermo.");
        }
    }
}
