using UnityEngine;
using UnityEngine.Rendering;

public class VinylMusicChanger : MonoBehaviour
{
    [SerializeField] private AudioClip _newMusicClip;
    [SerializeField] private TurnTable _turnTable; // Riferimento al TurnTable
    [SerializeField] private SpriteRenderer _spriteRenderer; // Riferimento all'AudioSource
    [SerializeField] private Sprite _vinylSprite; // Riferimento al messaggio del robot

    private float _yPosition = 0.7f;

    private void Awake()
    {
        if (_turnTable == null)
        {
            Debug.LogError("TurnTable non assegnato!");
        }
        if (_newMusicClip == null)
        {
            Debug.LogError("AudioClip non assegnato nel MusicChanger script.");
        }

        if(_spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer non assegnato nel MusicChanger script.");
        }

        if (_vinylSprite == null)
        {
            Debug.LogError("Sprite non assegnato nel MusicChanger script.");
        }

        if(Consts.GameData.isVinylMusicPlaying && Consts.GameData.currentLivingRoomMusic == _newMusicClip)
        {
            Debug.Log("La musica è già in riproduzione: " + _newMusicClip.name);
            _turnTable.ChangeMusicClip(_newMusicClip);//NON FUNZIONA PERCHé CI STA STOPMUSIC!!!!
            _spriteRenderer.sprite = _vinylSprite; // Cambia lo sprite del vinile se la musica è già in riproduzione
        }
    }

    private void OnMouseEnter()
    {
        Vector3 newPosition = this.transform.position;
        newPosition.y += _yPosition; // Solleva leggermente l'oggetto quando il mouse entra
        this.transform.position = newPosition;
    }

    private void OnMouseExit()
    {
        Vector3 oldPosition = this.transform.position;
        oldPosition.y -= _yPosition; // Abbassa leggermente l'oggetto quando il mouse entra
        this.transform.position = oldPosition;
    }

    private void OnMouseDown()
    {
        if (_turnTable != null && _newMusicClip != null)
        {
            _turnTable.ChangeMusicClip(_newMusicClip);
            Debug.Log("Musica cambiata: " + _newMusicClip.name);
            Consts.GameData.currentLivingRoomMusic = _newMusicClip; // Aggiorna il nome della musica corrente
            _spriteRenderer.sprite = _vinylSprite; // Cambia lo sprite del vinile
        }
    }
}
