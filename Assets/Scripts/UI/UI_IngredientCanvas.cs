using Consts;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//è assegnato all'IngredientCanvas
public class UI_IngredientCanvas : MonoBehaviour
{
    [SerializeField] private List<Button> _buttonIngredients = new List<Button>();

    [SerializeField] private List<TMP_Text> _textIngredients = new List<TMP_Text>();

    [SerializeField] private GameObject _warningMessage;

    private float _timer = 0f;
    private bool _isWarningMessageActive = false;

    private int _previousSelectedCount = 0;
    private AudioSource _audioSource;

    private bool _hasPlayedWarningSound = false;

    private void Awake()
    {
        if(_warningMessage == null)
        {
            Debug.LogError("Warning message GameObject is not assigned in UI_IngredientCanvas.");
        }
        else
        {
            _warningMessage.SetActive(false); // Inizialmente il messaggio è nascosto
            _audioSource = _warningMessage.GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                Debug.Log("audio source non collegato per il no del robot");
            }
        }
    }

    private void Start()
    {
        PrintRecipe();
        _previousSelectedCount = GameData.recipe.selectedIngredients_enum.Count;
    }

    private void Update()
    {
        ChangeButtonSprites();
        ChangeColorText();

        CheckForNewIngredient();

        ShowWarningMessage();
        WarningMessageTimer();
    }

    // Nuovo metodo per controllare se è stato aggiunto un ingrediente
    private void CheckForNewIngredient()
    {
        int currentSelectedCount = GameData.recipe.selectedIngredients_enum.Count;

        // Se è stato aggiunto un nuovo ingrediente
        if (currentSelectedCount > _previousSelectedCount)
        {
            // Controlla se il nuovo ingrediente è corretto
            bool allIngredientsValid = true;
            foreach (Ingredients ingredient in GameData.recipe.selectedIngredients_enum)
            {
                if (!GameData.recipe.ingredients.Contains(ingredient))
                {
                    allIngredientsValid = false;
                    break;
                }
            }

            // Se tutti gli ingredienti sono validi, nascondi immediatamente il messaggio
            if (allIngredientsValid)
            {
                HideWarningMessage();
            }
        }

        _previousSelectedCount = currentSelectedCount;
    }

    // Gestisce il timer per il messaggio di warning
    private void WarningMessageTimer()
    {
        if (_isWarningMessageActive)
        {
            _warningMessage.gameObject.SetActive(true);
            PlayWarningSoundOnce();
            _timer += Time.deltaTime;

            if (_timer >= 3f) // Dopo 3 secondi nascondi automaticamente
            {
                HideWarningMessage();
            }
        }
    }

    private void PlayWarningSoundOnce()
    {
        if (!_hasPlayedWarningSound && _audioSource != null)
        {
            _audioSource.Play();
            _hasPlayedWarningSound = true;
        }
    }



    private void HideWarningMessage()
    {
        _warningMessage.gameObject.SetActive(false);
        _isWarningMessageActive = false;
        _timer = 0f;
        _hasPlayedWarningSound = false; // Resetta lo stato del suono
    }

    // Metodo per mostrare il messaggio di warning
    private void ShowWarningMessage()
    {
        bool hasWrongIngredient = false;

        
        List<Ingredients> ingredientsToRemove = new List<Ingredients>();

        foreach (Ingredients ingredient in GameData.recipe.selectedIngredients_enum)
        {
            if (!GameData.recipe.ingredients.Contains(ingredient))
            {
                ingredientsToRemove.Add(ingredient);
                hasWrongIngredient = true;
            }
        }

        // Rimuovi gli ingredienti sbagliati
        foreach (Ingredients ingredient in ingredientsToRemove)
        {
            GameData.recipe.selectedIngredients_enum.Remove(ingredient);
        }

        // Mostra il messaggio solo se c'è un ingrediente sbagliato e non è già attivo
        if (hasWrongIngredient && !_isWarningMessageActive)
        {
            _isWarningMessageActive = true;
            _timer = 0f;
        }
    }


    //quando viene raccolto un ingrediente della lista viene mostrato nell'inventario
    public void ChangeButtonSprites()
    {
        for (int i = 0; i < GameData.recipe.selectedIngredients.Count; i++)
        {
            Color newColor = _buttonIngredients[i].image.color;
            newColor.a = 1;
            _buttonIngredients[i].image.color = newColor;
            _buttonIngredients[i].image.sprite = GameData.recipe.selectedIngredients[i].GetSprite();
        }
    }

    //quando viene raccolto un ingrediente viene rimosso dalla lista laterale
    public void ChangeColorText()
    {
        for (int i = 0; i < GameData.recipe.ingredients.Count; i++)
        {
            if(GameData.recipe.selectedIngredients_enum.Contains(GameData.recipe.ingredients[i]))
            {
                Color newColor = Color.gray;
                _textIngredients[i].color = newColor;
            }
        }
    }

    //stampa la ricetta su un pannello laterale
    public void PrintRecipe()
    {
        for (int i = 0; i < GameData.recipe.ingredients.Count; i++)
        {
            _textIngredients[i].text = GameData.recipe.ingredients[i].ToString();
        }
    }
}