using TMPro;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    [SerializeField] private TMP_Text _text; // Assicurati di assegnare l'animator nel tuo inspector

    private void Awake()
    {
        if (_text == null)
        {
            Debug.LogError("Text component is not assigned in the inspector.");
        }
        else
        {
            _text.text = "Attenzione:\n" + Consts.GameData.recipe.nameRecipe + "\npotrebbe causare felicita' immediata!";
        }
    }
}
