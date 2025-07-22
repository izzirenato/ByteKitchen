using UnityEngine;

public class CheckButon : MonoBehaviour
{
    [SerializeField] private GameObject _button;

    void Awake()
    {
        if (_button != null)
        {
            _button.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Consts.GameData.recipe.AllIngredientsCut())
        {
            if (_button != null)
            {
                _button.SetActive(true);
            }
        }
        else
        {
            if (_button != null)
            {
                _button.SetActive(false);
            }
        }
    }
}
