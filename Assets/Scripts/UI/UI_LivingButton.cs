
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LivingButton : MonoBehaviour
{
    public void OnLivingButtonClicked()
    {
        SceneManager.LoadScene(Consts.SceneNames.Living);
    }
}
