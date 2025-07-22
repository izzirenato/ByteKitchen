using UnityEngine;
using UnityEngine.SceneManagement;


//� assegnato al bottone della cucina che viene mostrato nell'IngredientCanvas
public class UI_KitchenButton : MonoBehaviour
{
    public void OnKitchenButtonClicked()
    {
        SceneManager.LoadScene(Consts.SceneNames.Kitchen);
    }
}
