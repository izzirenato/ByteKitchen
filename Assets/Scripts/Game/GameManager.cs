using Consts;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _alex;
    [SerializeField] private GameObject _bob;
    [SerializeField] private GameObject _mango;
    [SerializeField] private GameObject _margherita;
    [SerializeField] private GameObject _messageBox;

    private Characters _player;

    private int _lastInteractionCount = 0;
    private AudioSource _audioSource;

    private void Awake()
    {
        if(_alex == null || _bob == null || _mango == null || _margherita == null)
        {
            Debug.Log("Riferimenti ai gameObject dei characters mancanti");
        }

        if(_messageBox == null)
        {
            Debug.Log("Riferimento al messageBox mancante");
        }else
        {
            _messageBox.SetActive(false); //inizialmente il messageBox è nascosto
            _audioSource = _messageBox.GetComponent<AudioSource>();
        }


        //controllo se characterselected non è null
        _player = GameData.characterSelected;
        switch(_player)
        {
            case Characters.MANGO:
                _mango.SetActive(true);
                break;
            case Characters.MARGHERITA:
                _margherita.SetActive(true);
                break;
            case Characters.ALEX:
                _alex.SetActive(true);
                break;
            case Characters.BOB:
                _bob.SetActive(true);
                break;

        }
    }

    private void Update()
    {
        // Se il numero di interazioni è aumentato
        if (Consts.GameData.interactionCount > _lastInteractionCount)
        {
            _lastInteractionCount = Consts.GameData.interactionCount;

            if (Consts.GameData.interactionCount >= 2)
            {
                Consts.GameData.isMealReady = true;
                StartCoroutine(ShowMessage(1f));
            }
        }
    }

    IEnumerator ShowMessage(float duration)
    {
        yield return new WaitForSeconds(duration);
        //mostra un messaggio che dice che il giocatore può andare in cucina
        Debug.Log("Il piatto è pronto!");
        _messageBox.SetActive(true);
        _audioSource.Play(); // riproduce il suono del messaggio

        yield return new WaitForSeconds(4f); // attende 4 secondi prima di nascondere il messaggio

        _messageBox.SetActive(false); // nasconde il messageBox
    }
}
