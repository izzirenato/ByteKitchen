using UnityEngine;
using Consts;

public class SlicableObject2 : MonoBehaviour
{
    [SerializeField] private GameObject unslicedObjectRight;
    [SerializeField] private GameObject unslicedObjectLeft;

    [SerializeField] private GameObject _sliceGuideLeft;
    [SerializeField] private GameObject _sliceGuideRight;

    [SerializeField] private float sliceOffsetX = 0.7f;

    [SerializeField] private AudioClip cutSound; // Clip audio da assegnare
    private AudioSource audioSource;

    private Vector3 originalLeftPos;
    private Vector3 originalRightPos;

    private void Start()
    {
        originalLeftPos = unslicedObjectLeft.transform.localPosition;
        originalRightPos = unslicedObjectRight.transform.localPosition;

        audioSource = GetComponent<AudioSource>();
    }

    public void Slice(string side)
    {
        // Riproduce l'audio di taglio
        if (cutSound != null && audioSource != null)
        {
            audioSource.volume = Consts.GameData.SFXvolume;
            audioSource.PlayOneShot(cutSound);
        }

        if (side == "left")
        {
            unslicedObjectLeft.transform.position += new Vector3(-sliceOffsetX, 0f, 0f);
            _sliceGuideLeft.SetActive(false);
            IngredienteInfo.hasBeenCutLeft = true;
        }
        else if (side == "right")
        {
            unslicedObjectRight.transform.position += new Vector3(sliceOffsetX, 0f, 0f);
            _sliceGuideRight.SetActive(false);
            IngredienteInfo.hasBeenCutRight = true;
        }

        if (IngredienteInfo.hasBeenCutLeft && IngredienteInfo.hasBeenCutRight)
        {
            Consts.GameData.recipe.selectedIngredients[IngredienteInfo.numIngredient].hasBeenCut = true;

            if (UI_CutButton.activeButton != null)
                UI_CutButton.activeButton.DisableButton();
        }
    }

    public void SpriteReset()
    {
        IngredienteInfo.hasBeenCutLeft = false;
        IngredienteInfo.hasBeenCutRight = false;

        unslicedObjectLeft.transform.localPosition = originalLeftPos;
        unslicedObjectRight.transform.localPosition = originalRightPos;

        _sliceGuideLeft.SetActive(true);
        _sliceGuideRight.SetActive(true);
    }

    public void ResetGuides()
    {
        _sliceGuideLeft.SetActive(false);
        _sliceGuideRight.SetActive(false);

        Invoke(nameof(ReactivateGuides), 0.01f);
    }

    private void ReactivateGuides()
    {
        _sliceGuideLeft.SetActive(true);
        _sliceGuideRight.SetActive(true);
    }
}
