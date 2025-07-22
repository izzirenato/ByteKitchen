using System.Collections;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public OvenController ovenController;
    public GameObject blinkingOutline;  // ← assegnalo da Unity
    [SerializeField] private CameraZoom _camera; // Riferimento alla camera principale
    [SerializeField] private AudioSource _audioSource1;
    [SerializeField] private AudioSource _audioSource2;
    [SerializeField] private AudioClip _audioClipGlow;
    [SerializeField] private AudioClip _audioClipDrum;

    private void Awake()
    {
        if (ovenController == null)
        {
            Debug.LogError("OvenController non assegnato nel ButtonClick script.");
        }
        if (_camera == null)
        {
            Debug.LogError("CameraZoom non assegnato nel ButtonClick script.");
        }
        // Assicurati che il bottone sia attivo all'inizio
        gameObject.SetActive(true);
        // Assicurati che l'outline lampeggiante sia attivo all'inizio
        if (blinkingOutline != null)
        {
            blinkingOutline.SetActive(true);
        }

        if(_audioSource1 == null || _audioSource2 == null)
        {
            Debug.LogError("AudioSource non assegnato.");
        }

        if (_audioClipGlow == null || _audioClipDrum == null)
        {
            Debug.LogError("AudioClip non assegnato nel ButtonClick script.");
        }
    }

    void OnMouseDown()
    {
        ovenController.OpenOven();
        _camera.ZoomToTarget(); // Esegui lo zoom sulla telecamera

        // Disattiva subito il bottone
        gameObject.SetActive(false);

        // Disattiva anche la parte lampeggiante (il contorno)
        if (blinkingOutline != null)
        {
            blinkingOutline.SetActive(false);
        }

        if(MusicManager.Instance != null)
            MusicManager.Instance.StopMusic(); // Riproduci il suono del click
        _audioSource1.clip = _audioClipGlow; // Assegna il clip audio
        _audioSource1.Play(); // Riproduci il suono del click

        _audioSource2.clip = _audioClipDrum; // Assegna il clip audio
        _audioSource2.Play();
    }
}
