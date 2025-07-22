
using Consts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//è assegnato ai bottoni nella scena di taglio e di mix
public class UI_Buttons : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    public void OnMixButtonClicked() //porta alla scena di mix
    {
        //se tutti gli oggetti sono stati tagliati
        if(GameData.recipe.AllIngredientsCut())
        {

            if (GameData.recipe.nameRecipe == Recipes.TAGLIATA || GameData.recipe.nameRecipe == Recipes.PATATE_FORNO)
            {
                Consts.GameData.robotIsCooking = true;
                //SceneManager.LoadScene(Consts.SceneNames.Kitchen); //se è la tagliata o le patate al forno, non faccio mix
                StartCoroutine(LoadSceneAfterSound(Consts.SceneNames.Kitchen));
            }
            else
            {
                //SceneManager.LoadScene(Consts.SceneNames.Mix);
                StartCoroutine(LoadSceneAfterSound(Consts.SceneNames.Mix));
            }
                
        }
    }

    public void OnKitchenButtonClicked() //porta alla scena di cucina
    {
        //se hai mescolato per abbastanza tempo
        Consts.GameData.robotIsCooking = true;
        //SceneManager.LoadScene(Consts.SceneNames.Kitchen);
        StartCoroutine(LoadSceneAfterSound(Consts.SceneNames.Kitchen));
    }

    public void OnLivingButtonClicked() //porta alla scena di living
    {
        //SceneManager.LoadScene(Consts.SceneNames.Living);
        StartCoroutine(LoadSceneAfterSound(Consts.SceneNames.Living));
    }

    public void OnEndButtonClick()
    {
        //SceneManager.LoadScene(Consts.SceneNames.End);
        StartCoroutine(LoadSceneAfterSound(Consts.SceneNames.End));
    }

    public void OnHomeButtonClick()
    {
        //SceneManager.LoadScene(Consts.SceneNames.Home);
        GameData.Reset();
        StartCoroutine(LoadSceneAfterSound(Consts.SceneNames.MainMenu));
    }

    IEnumerator LoadSceneAfterSound(string scene)
    {
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
        SceneManager.LoadScene(scene);
    }
}
