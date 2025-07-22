using Consts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableObjectText : MonoBehaviour
{
    [Header("Text References")]
    [SerializeField] private GameObject _textBox;
    [SerializeField] private GameObject _indicator;

    [Header("Text Settings")]
    [SerializeField] private bool _showIndicatorByDefault = true;

    private string _objectTag;

    private TMP_Text _textComponent;
    private bool _lastRobotCookingState;
    private readonly Dictionary<string, TextConfig> _textConfigs = new();

    private struct TextConfig
    {
        public bool ShowTextBox; //determina se mostrare la TextBox
        public bool ShowIndicator; //determina se mostrare l'indicatore (icona sopra l'oggetto)
        public string CustomText; //testo personalizzato da mostrare
        public System.Func<string> DynamicTextProvider; //funzione per ottenere testo dinamico, posso usare anche una lambda function
        public System.Func<bool> ShowCondition; //condizione per mostrare o nascondere il testo

        public TextConfig(bool showTextBox = true, bool showIndicator = false, string customText = "Premi E\n↓",
            System.Func<string> dynamicTextProvider = null, System.Func<bool> showCondition = null)
        {
            ShowTextBox = showTextBox;
            ShowIndicator = showIndicator;
            CustomText = customText;
            DynamicTextProvider = dynamicTextProvider;
            ShowCondition = showCondition;
        }
    }

    private void Awake()
    {
        InitializeComponents();
        InitializeTextConfigs();
        CacheObjectTag();
        //UpdateIndicatorVisibilityBasedOnTag();
    }

    private void Update()
    {
        // Aggiorna solo se lo stato è cambiato
        if (_lastRobotCookingState != GameData.robotIsCooking)
        {
            _lastRobotCookingState = GameData.robotIsCooking;
            UpdateIndicatorVisibilityBasedOnTag();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsPlayer(collision.collider))
        {
            HandlePlayerEnter();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (IsPlayer(collision.collider))
        {
            HandlePlayerEnter();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (IsPlayer(collision.collider))
        {
            HandlePlayerExit();
        }
    }

    private void InitializeComponents()
    {
        // Validate required references
        if (_textBox == null)
        {
            Debug.LogError($"TextBox reference missing on {gameObject.name}!", this);
            return;
        }

        if (_indicator == null)
        {
            Debug.LogError($"Indicator reference missing on {gameObject.name}!", this);
            return;
        }

        // Cache text components
        _textComponent = _textBox.GetComponentInChildren<TMP_Text>();

        // Validate text components
        if (_textComponent == null)
        {
            Debug.LogError($"TMP_Text component not found in TextBox on {gameObject.name}!", this);
        }

    }

    private void CacheObjectTag()
    {
        _objectTag = gameObject.tag;
    }

    private void InitializeTextConfigs()
    {
        // Oggetti semplici che mostrano solo testo base
        var simpleObjects = new[] { "Photo", "TurnTable", "Bookshelf", "Cat", "Computer", "Lamp" };
        foreach (var tag in simpleObjects)
        {
            _textConfigs[tag] = new TextConfig(showTextBox: true, showIndicator: false);
        }

        // Frigo - condizionale basato su robotIsCooking
        _textConfigs["Fridge"] = new TextConfig(
            showTextBox: true,
            showIndicator: false,
            showCondition: () => !GameData.robotIsCooking
        );

        // Credenza - condizionale basato su robotIsCooking
        _textConfigs["Cupboard"] = new TextConfig(
            showTextBox: true,
            showIndicator: false,
            showCondition: () => !GameData.robotIsCooking
        );

        // Soggiorno - mostrato solo quando il robot cucina
        _textConfigs["LivingRoom"] = new TextConfig(
            showTextBox: true,
            showIndicator: false,
            showCondition: () => GameData.robotIsCooking
        );

        // Cucina - mostrato solo quando il robot cucina
        _textConfigs["Kitchen"] = new TextConfig(
            showTextBox: true,
            showIndicator: false,
            showCondition: () => GameData.robotIsCooking
        );

        // Robot - testo dinamico basato sullo stato
        _textConfigs["Robot"] = new TextConfig(
            showTextBox: true,
            showIndicator: false,
            dynamicTextProvider: GetRobotText
        );
    }

    private void HandlePlayerEnter()
    {
        if (!_textConfigs.TryGetValue(_objectTag, out var config))
        {
            // Configurazione di default per tag non configurati
            ShowDefaultText();
            return;
        }

        // Controlla condizione di visibilità se presente
        if (config.ShowCondition != null && !config.ShowCondition())
        {
            HideAllText();
            return;
        }

        // Mostra testo appropriato
        ShowTextForConfig(config);
    }

    private void HandlePlayerExit()
    {
        if (!IsValidTextComponent())
        {
            Debug.Log("Text component destroyed, likely due to scene change", this);
            return;
        }

        HideTextBox();
        UpdateIndicatorVisibilityBasedOnTag();
    }

    private void ShowTextForConfig(TextConfig config)
    {
        // Imposta visibilità delle UI
        SetTextBoxVisibility(config.ShowTextBox);
        SetIndicatorVisibility(config.ShowIndicator);

        // Imposta il testo se necessario
        if (config.ShowTextBox && _textComponent != null)
        {
            string textToShow = GetTextForConfig(config);
            if (!string.IsNullOrEmpty(textToShow))
            {
                _textComponent.text = textToShow;
            }
        }
    }

    private string GetTextForConfig(TextConfig config)
    {
        // Priorità: testo dinamico > testo personalizzato > testo di default
        if (config.DynamicTextProvider != null)
        {
            return config.DynamicTextProvider();
        }

        return config.CustomText;
    }

    private void ShowDefaultText()
    {
        SetTextBoxVisibility(true);
        SetIndicatorVisibility(_showIndicatorByDefault);
    }

    private void HideAllText()
    {
        SetTextBoxVisibility(false);
        SetIndicatorVisibility(false);
    }

    private void UpdateIndicatorVisibilityBasedOnTag()
    {

        bool shouldShowIndicator = true;
        if ((_objectTag == "LivingRoom" || _objectTag == "Kitchen") && !GameData.robotIsCooking) //se il robot non cucina, non mostro l'indicatore
        {
            shouldShowIndicator = false;
        }

        if ((_objectTag == "Fridge" || _objectTag == "Cupboard") && GameData.robotIsCooking) //se il robot sta cucinando, non mostro l'indicatore
        {
            shouldShowIndicator = false;
        }

        if(_objectTag == "Robot" && GameData.robotIsCooking && !GameData.isMealReady) //se il robot sta cucinando, non mostro l'indicatore
        {
            shouldShowIndicator = false;
        }

        if(_objectTag == "LivingRoom" && GameData.isMealReady)
        {
            shouldShowIndicator = false;
        }

        SetIndicatorVisibility(shouldShowIndicator);
    }

    private string GetRobotText()
    {
        if (!GameData.robotIsCooking)
        {
            if (!GameData.recipe.AllIngredientsTaken())
            {
                return "<b>Compito iniziale:</b> acquisizione risorse culinarie!";
            }
            else
            {
                return "Premi E\n↓";
            }
        }
        else if (!GameData.isMealReady)
        {
            return "Elaborazione ricetta in corso... torna tra qualche minuto!";
        }
        else
        {
            return "Cottura al 100%. Premi E per nutrirti!";
        }
    }

    private void SetTextBoxVisibility(bool visible)
    {
        if (_textBox != null)
        {
            _textBox.SetActive(visible);
        }
    }

    private void SetIndicatorVisibility(bool visible)
    {
        if (_indicator != null)
        {
            _indicator.SetActive(visible);
        }
    }

    private void HideTextBox()
    {
        SetTextBoxVisibility(false);
    }

    private bool IsPlayer(Collider2D collider)
    {
        return collider != null && collider.CompareTag("Player");
    }

    private bool IsValidTextComponent()
    {
        return _textComponent != null && !_textComponent.IsDestroyed();
    }


    //private void Awake()
    //{
    //    if (_textBox == null)
    //    {
    //        Debug.Log("Nessun riferimento a _text");
    //    }

    //    _objectTag = gameObject.tag;
    //    _textComponent = _textBox.GetComponentInChildren<TMP_Text>();
    //    _nameComponent = _nameBox.GetComponentInChildren<TMP_Text>();
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.collider.CompareTag("Player"))
    //    {
    //        switch (_objectTag)
    //        {
    //            case "Photo":
    //                _textBox.SetActive(true);
    //                _nameBox.SetActive(false);
    //                break;
    //            case "TurnTable":
    //                _textBox.SetActive(true);
    //                _nameBox.SetActive(false);
    //                break;
    //            case "Bookshelf":
    //                _textBox.SetActive(true);
    //                _nameBox.SetActive(false);
    //                break;
    //            case "Cat":
    //                _textBox.SetActive(true);
    //                _nameBox.SetActive(false);
    //                break;
    //            case "Computer":
    //                _textBox.SetActive(true);
    //                _nameBox.SetActive(false);
    //                break;
    //            case "Fridge":
    //                if (!GameData.robotIsCooking) //se il robot cucina non posso pi� aprire il frigo, vi piace?
    //                {
    //                    _textBox.SetActive(true);
    //                    _nameBox.SetActive(false);
    //                 }
    //                else if (GameData.robotIsCooking)
    //                {
    //                    _textBox.SetActive(false);
    //                    _nameBox.SetActive(false);
    //                }
    //                break;
    //            case "Cupboard":
    //                if (!GameData.robotIsCooking)
    //                {
    //                    _textBox.SetActive(true);
    //                    _nameBox.SetActive(false);
    //                }
    //                else if (GameData.robotIsCooking)
    //                {
    //                    _textBox.SetActive(false);
    //                    _nameBox.SetActive(false);
    //                }
    //                break;
    //            case "LivingRoom":
    //                if (GameData.robotIsCooking)
    //                    _textBox.SetActive(true);
    //                    _nameBox.SetActive(false);
    //                break;
    //            case "Robot":
    //                if (!GameData.robotIsCooking)
    //                {
    //                    if (!GameData.recipe.AllIngredientsTaken())
    //                        _textComponent.text = "<b>Compito iniziale:</b> acquisizione risorse culinarie!";
    //                    else
    //                    {
    //                        _textComponent.text = "Premi E\r\n\\u2193";
    //                    }
    //                }
    //                else if (!GameData.isMealReady) //se il robot sta cucinando ma il piatto non � pronto
    //                {
    //                    _textComponent.text = "Elaborazione ricetta in corso... torna tra qualche minuto!";

    //                }
    //                else
    //                {
    //                    _textComponent.text = "Cottura al 100%. Premi E per nutrirti!";
    //                }
    //                _textBox.SetActive(true);
    //                _nameBox.SetActive(false);
    //                break;
    //            case "Kitchen":
    //                if (GameData.robotIsCooking)
    //                    _textBox.SetActive(true);
    //                    _nameBox.SetActive(false);
    //                break;
    //            default:
    //                _textBox.SetActive(true);
    //                break;
    //        }
    //    }

    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (_textComponent.IsDestroyed())
    //    {
    //        Debug.Log("oggetto text distrutto"); //forse me lo distrugge con i cambio scena?
    //    }
    //    else
    //    {
    //        if (collision.collider.CompareTag("Player"))
    //        {
    //            _textBox.SetActive(false);
    //            if (_objectTag == "LivingRoom" || _objectTag == "Kitchen")
    //            {
    //                _nameBox.SetActive(false);
    //            }
    //            else
    //            {
    //                _nameBox.SetActive(true);
    //            }

    //        }
    //    }
    //}

}
