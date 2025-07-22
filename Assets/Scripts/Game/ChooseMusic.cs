
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ChooseMusic : MonoBehaviour
//{
//    [SerializeField] private AudioSource _audioSource;
//    [SerializeField] private GameObject _panel;

//    private List<Button> _buttons = new List<Button>();


//    void Awake()
//    {
//        if (_audioSource == null)
//        {
//            Debug.LogError("AudioSource non assegnato nel ChooseMusic script.");
//        }

//        if (_panel == null)
//        {
//            Debug.LogError("Panel non assegnato nel ChooseMusic script.");
//        }
//        else
//        {
//            _panel.SetActive(false); // Assicurati che il pannello sia nascosto all'inizio

//            // Ottieni tutti i bottoni figli del pannello
//            Button[] buttonsInPanel = _panel.GetComponentsInChildren<Button>();

//            if (buttonsInPanel.Length == 0)
//            {
//                Debug.LogWarning("Nessun Button trovato nel pannello.");
//            }
//            else
//            {

//                for (int i = 0; i < buttonsInPanel.Length; i++)
//                {
//                    int index = i; // Fondamentale: cattura il valore corrente
//                    Button btn = buttonsInPanel[i];

//                    _buttons.Add(btn);

//                    btn.onClick.AddListener(() =>
//                    {
//                        Debug.Log("Hai cliccato il bottone numero: " + index);
//                        SelectClip(index);
//                    });
//                }
//            }
//        }
//    }

//    private void SelectClip(int index)
//    {
//        AudioSource audioSource = _buttons[index].GetComponent<AudioSource>();
//        Consts.GameData.currentLivingRoomMusic = audioSource.clip;
//        _audioSource.clip = audioSource.clip;
//    }

//    private void OnMouseDown()
//    {
//        _panel.SetActive(true); // Mostra o nasconde il pannello quando si clicca
//    }

//}
