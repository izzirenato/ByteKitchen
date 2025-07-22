using Consts;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_CutButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _colliders;
    [SerializeField] private GameObject _warning;
    [SerializeField] private SlicableObject2 slicableObject; // Assegna da Inspector

    [SerializeField] private AudioClip warningSound; // Clip audio per il warning
    private AudioSource audioSource;

    private float _timer = 0.0f;
    private bool _isWarningMessageActive = false;

    public static UI_CutButton activeButton;
    public static SlicableObject2 activeSlicableObject;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_isWarningMessageActive)
        {
            _warning.gameObject.SetActive(true);
            if (_warning.activeInHierarchy)
            {
                _timer += Time.deltaTime;
                if (_timer >= 3f)
                {
                    _warning.gameObject.SetActive(false);
                    _isWarningMessageActive = false;
                    _timer = 0f;
                }
            }
        }
    }

    public void OnButtonClicked()
    {
        int index = 0;

        if (_button == null || _button.image == null || GameData.recipe.selectedIngredients == null)
        {
            Debug.Log("Manca un riferimento essenziale");
            return;
        }

        foreach (Ingredient i in GameData.recipe.selectedIngredients.ToList())
        {
            if (_button.image.sprite == i.GetSprite())
            {
                if (i.isCuttable)
                {
                    Debug.Log($"Ingrediente {i.GetSpriteName()} può essere tagliato");

                    IngredienteInfo.referenceName = i.GetSpriteName();
                    IngredienteInfo.numIngredient = index;
                    _colliders.SetActive(true);
                    _warning.SetActive(false);
                    _isWarningMessageActive = false;
                    _timer = 0f;

                    if (activeSlicableObject != null && activeSlicableObject != slicableObject)
                    {
                        activeSlicableObject.SpriteReset();
                    }

                    slicableObject.ResetGuides();

                    activeButton = this;
                    activeSlicableObject = slicableObject;
                }
                else
                {
                    _button.interactable = false;
                    IngredienteInfo.referenceName = null;
                    _colliders.SetActive(false);
                    _isWarningMessageActive = true;
                    _timer = 0f;

                    // Riproduce il suono di warning
                    if (warningSound != null && audioSource != null)
                        audioSource.PlayOneShot(warningSound);

                    Debug.Log("L'ingrediente NON può essere tagliato");
                }
            }

            index++;
        }
    }

    public void DisableButton()
    {
        _button.interactable = false;
    }
}
