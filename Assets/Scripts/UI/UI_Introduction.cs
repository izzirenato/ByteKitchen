using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Introduction : MonoBehaviour
{
    [Header("Dialogues")]
    public List<string> dialogues = new List<string>(5);
    //public List<Sprite> characterSprites = new List<Sprite>(5);

    [Header("UI Elements")]
    public Image characterImage;
    public TMP_Text dialogueText;
    public Button dialogueButton;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickSound;

    [Header("Choice Buttons")]
    public Button yesButton;
    public Button noButton;
    public GameObject choiceButtonsPanel; // Panel che contiene i bottoni Si/No

    [Header("Settings")]
    public float deltaType = 0.03f;

    private int _currentDialogueIndex = 0;
    private Coroutine _typingCoroutine;
    private bool _isTyping = false;
    private bool _showingChoices = false;
    private bool _showingFinalConfirmation = false;
    

    void Start()
    {
        _audioSource.clip = _clickSound;
        SetupButtons();
        StartDialogue();
    }

    private void SetupButtons()
    {
        // Setup del bottone principale del dialogo
        if (dialogueButton != null)
        {
            dialogueButton.onClick.RemoveAllListeners();
            dialogueButton.onClick.AddListener(OnDialogueClick);
        }

        // Setup bottoni scelta
        if (yesButton != null)
        {
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(OnYesButtonClick);
        }

        if (noButton != null)
        {
            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(OnNoButtonClick);
        }
        choiceButtonsPanel.SetActive(false);
    }

    private void StartDialogue()
    {
        _currentDialogueIndex = 0;
        _showingChoices = false;
        _showingFinalConfirmation = false;
        ShowCurrentDialogue();
    }

    private void ShowCurrentDialogue()
    {
        switch (_currentDialogueIndex)
        {
            case 0:
            characterImage.sprite = Resources.Load<Sprite>("Sprites/Robot/robot_1");
                break;
            case 1:
            characterImage.sprite = Resources.Load<Sprite>("Sprites/Robot/robotOk");
                break;
            case 2:
            characterImage.sprite = Resources.Load<Sprite>("Sprites/Robot/robot accogliente");
                break;
            case 3:
            characterImage.sprite = Resources.Load<Sprite>("Sprites/Robot/robot pensante");
                break;
            case 4:
            characterImage.sprite = Resources.Load<Sprite>("Sprites/Robot/robot occhioni");
                break;
            default:
            characterImage.sprite = Resources.Load<Sprite>("Sprites/Robot/robot_1");
                break;
        }

        // Avvia il typing del testo
        if (_currentDialogueIndex < dialogues.Count)
        {
            if (_typingCoroutine != null)
            {
                StopCoroutine(_typingCoroutine);
            }
            _typingCoroutine = StartCoroutine(TypeText(dialogues[_currentDialogueIndex]));
        }
    }

    private IEnumerator TypeText(string fullText)
    {
        dialogueText.text = "";
        _isTyping = true;

        foreach (char c in fullText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(deltaType);
        }

        _isTyping = false;

        // Dopo il quarto dialogo (indice 3), mostra i bottoni scelta
        if (_currentDialogueIndex == 3 && !_showingChoices)
        {
            _showingChoices = true;
            choiceButtonsPanel.SetActive(true);
        }
    }

    public void OnDialogueClick()
    {
        if (_currentDialogueIndex < 3 || _isTyping)
            _audioSource.Play();

        // Se sta ancora scrivendo, completa il testo
        if (_isTyping)
        {
            if (_typingCoroutine != null)
            {
                StopCoroutine(_typingCoroutine);
            }
            dialogueText.text = dialogues[_currentDialogueIndex];
            _isTyping = false;

            // Controlla se dopo aver completato il testo dobbiamo mostrare i bottoni
            if (_currentDialogueIndex == 3 && !_showingChoices)
            {
                _showingChoices = true;
                choiceButtonsPanel.SetActive(true);
            }
            return;
        }

        // Se non sta scrivendo e non stiamo mostrando scelte, passa al prossimo dialogo
        if (!_showingChoices && !_showingFinalConfirmation)
        {
            _currentDialogueIndex++;

            // Se abbiamo ancora dialoghi da mostrare (0-3)
            if (_currentDialogueIndex <= 3)
            {
                ShowCurrentDialogue();
            }
        }
    }

    public void OnYesButtonClick()
    {
        StartCoroutine(LoadSceneAfterSound(Consts.SceneNames.Character));
    }

    public void OnNoButtonClick()
    {
        if (_showingFinalConfirmation)
        {
            StartCoroutine(LoadSceneAfterSound(Consts.SceneNames.MainMenu));
        }
        else if (_showingChoices)
        {
            _audioSource.Play();
            // Dalla prima scelta, mostra il quinto dialogo di conferma
            _showingChoices = false;
            _showingFinalConfirmation = true;
            _currentDialogueIndex = 4;
            choiceButtonsPanel.SetActive(false);
            ShowCurrentDialogue();
            // Dopo aver mostrato il quinto dialogo, rimostra i bottoni
            StartCoroutine(ShowChoicesAfterFifthDialogue());
        }
    }

    private IEnumerator ShowChoicesAfterFifthDialogue()
    {
        // Aspetta che il typing del quinto dialogo finisca
        while (_isTyping)
        {
            yield return null;
        }
        choiceButtonsPanel.SetActive(true);
    }

    IEnumerator LoadSceneAfterSound(string scene)
    {
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
        SceneManager.LoadScene(scene);
    }
}