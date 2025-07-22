using UnityEngine;
using Consts;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private Ingredients _nameIngredient;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private GameObject _boxText;

    private string _nameSpriteIngredient;

    
    private bool _isInTheRecipe;
    private TMP_Text _text;
    public bool isCuttable = false;
    public bool hasBeenCut = false;

 
    private void Awake()
    {
        IsInTheRecipe();
        IsCuttable();

        if (GameData.recipe.selectedIngredients_enum.Contains(_nameIngredient))
        {
            gameObject.SetActive(false);
        }

        SetSpriteName();

        _text = GetComponentInChildren<TMP_Text>(true); //con il true mi controlla anche negli oggetti disattivati

        if(_text != null)
        {
            _text.text = _nameIngredient.ToString();
            //Debug.Log(_text.text);
        }else
        {
            Debug.Log("non collegato");
        }
        
    }


    public Ingredients GetName()
    {
        return _nameIngredient;
    }

    public string GetSpriteName()
    {
        return _nameSpriteIngredient;
    }

    public void SetSpriteName()
    {
        switch (_nameIngredient)
        {
            case Ingredients.ZUCCHINA:
                _nameSpriteIngredient = "zucchina";
                break;
            case Ingredients.CIPOLLA:
                _nameSpriteIngredient = "cipolla";
                break;
            case Ingredients.BURRO:
                _nameSpriteIngredient = "burro";
                break;
            case Ingredients.BASILICO:
                _nameSpriteIngredient = "basilico";
                break;
            case Ingredients.UOVO:
                _nameSpriteIngredient = "uovo";
                break;
            case Ingredients.POMODORO:
                _nameSpriteIngredient = "pomodoro";
                break;
            case Ingredients.MOZZARELLA:
                _nameSpriteIngredient = "mozzarella";
                break;
            case Ingredients.LATTE:
                _nameSpriteIngredient = "latte";
                break;
            case Ingredients.FORMAGGIO:
                _nameSpriteIngredient = "formaggio";
                break;
            case Ingredients.ARANCIA:
                _nameSpriteIngredient = "arancia";
                break;
            case Ingredients.FUNGO:
                _nameSpriteIngredient = "fungo";
                break;
            case Ingredients.SALSICCIA:
                _nameSpriteIngredient = "salsiccia";
                break;
            case Ingredients.PANNA:
                _nameSpriteIngredient = "panna";
                break;
            case Ingredients.MELA:
                _nameSpriteIngredient = "mela";
                break;
            case Ingredients.ZUCCA:
                _nameSpriteIngredient = "zucca";
                break;
            case Ingredients.FRUTTI_DI_BOSCO:
                _nameSpriteIngredient = "fruttiDiBosco";
                break;
            case Ingredients.FORMAGGIO_SPALMABILE:
                _nameSpriteIngredient = "formaggioSpalmabile";
                break;
            case Ingredients.ACQUA:
                _nameSpriteIngredient = "acqua";
                break;
            case Ingredients.CARNE:
                _nameSpriteIngredient = "carne";
                break;
            case Ingredients.RUCOLA:
                _nameSpriteIngredient = "rucola";
                break;
            case Ingredients.BANANA:
                _nameSpriteIngredient = "banana";
                break;
            case Ingredients.SALE:
                _nameSpriteIngredient = "sale";
                break;
            case Ingredients.NOCCIOLA:
                _nameSpriteIngredient = "nocciola";
                break;
            case Ingredients.CANNELLA:
                _nameSpriteIngredient = "cannella";
                break;
            case Ingredients.PASTA:
                _nameSpriteIngredient = "pasta";
                break;
            case Ingredients.PINOLO:
                _nameSpriteIngredient = "pinolo";
                break;
            case Ingredients.AGLIO:
                _nameSpriteIngredient = "aglio";
                break;
            case Ingredients.PEPE:
                _nameSpriteIngredient = "pepe";
                break;
            case Ingredients.VINO_BIANCO:
                _nameSpriteIngredient = "vinoBianco";
                break;
            case Ingredients.PATATA:
                _nameSpriteIngredient = "patata";
                break;
            case Ingredients.ERBE:
                _nameSpriteIngredient = "erbe";
                break;
            case Ingredients.BISCOTTO:
                _nameSpriteIngredient = "biscotto";
                break;
            case Ingredients.FARINA:
                _nameSpriteIngredient = "farina";
                break;
            case Ingredients.CIOCCOLATO:
                _nameSpriteIngredient = "cioccolato";
                break;
            case Ingredients.MANDORLA:
                _nameSpriteIngredient = "mandorla";
                break;
            case Ingredients.OLIO:
                _nameSpriteIngredient = "olio";
                break;
            case Ingredients.ZUCCHERO:
                _nameSpriteIngredient = "zucchero";
                break;
            case Ingredients.LIEVITO:
                _nameSpriteIngredient = "lievito";
                break;
            case Ingredients.COUS_COUS:
                _nameSpriteIngredient = "cousCous";
                break;
            default:
                break;
        }
    }

    public void IsCuttable()
    {
        switch(_nameIngredient)
        {
            case Ingredients.ZUCCHINA:
                isCuttable = true;
                break;
            case Ingredients.CIPOLLA:
                isCuttable = true;
                break;
            case Ingredients.BURRO:
                isCuttable = true;
                break;
            case Ingredients.POMODORO:
                isCuttable = true;
                break;
            case Ingredients.MOZZARELLA:
                isCuttable = true;
                break;
            //case Ingredients.FORMAGGIO:
            //    isCuttable = true;
            //    break;
            case Ingredients.ARANCIA:
                isCuttable = true;
                break;
            case Ingredients.FUNGO:
                isCuttable = true;
                break;
            case Ingredients.SALSICCIA:
                isCuttable = true;
                break;
            case Ingredients.MELA:
                isCuttable = true;
                break;
            case Ingredients.ZUCCA:
                isCuttable = true;
                break;
            case Ingredients.CARNE:
                isCuttable = true;
                break;
            case Ingredients.BANANA:
                isCuttable = true;
                break;
            case Ingredients.PATATA:
                isCuttable = true;
                break;
            case Ingredients.CIOCCOLATO:
                isCuttable = true;
                break;
        }
    }
    
    public void IsInTheRecipe()
    {

        foreach (Ingredients ingredient in GameData.recipe.ingredients)
        {
            if (_nameIngredient == ingredient)
            {
                _isInTheRecipe = true;
                return;
            }
        }

        _isInTheRecipe = false;
    }

    public void OnMouseDown()
    {
        //IsInTheRecipe();

        if(_isInTheRecipe)
        {
            GameData.recipe.selectedIngredients.Add(this);
            if (isCuttable)
                GameData.recipe.cuttableIngredients.Add(this);
            GameData.recipe.selectedIngredients_enum.Add(_nameIngredient);
            gameObject.SetActive(false);
        }
        else
        {
            GameData.recipe.selectedIngredients_enum.Add(_nameIngredient);
        }
    }

    private void OnMouseEnter()
    {
        //Debug.Log(_nameIngredient);
        _boxText.SetActive(true);
    }

    private void OnMouseExit()
    {
        //Debug.Log("sono uscita da " + _nameIngredient);
        _boxText.SetActive(false);
    }

    public Sprite GetSprite()
    {
        return _sprite;
    }

}
