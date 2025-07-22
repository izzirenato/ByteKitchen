using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//è assegnato alla PreferencesWindows del PreferencesCanvas
public class UI_Preferences : MonoBehaviour
{
    private bool moodButtonPressed = false;
    private bool tasteButtonPressed = false;

    [SerializeField] private Button _startButton;
    [SerializeField] private GameObject _message;

    //[SerializeField] private Color _saturatedColor = new Color(1f, 0f, 0f);
    //[SerializeField] private Color _desaturatedColor = new Color(1f, 0f, 0f, 0.7f);
    private AudioSource _audioSource;

    private void Awake()
    {
        if (_startButton == null)
        {
            Debug.Log("Bottone start non collegato");
        }

        if (_message == null)
        {
            Debug.Log("Messaggio non collegato");
        }
        else
        {
            _message.SetActive(false); //inizialmente il messaggio è nascosto
        }

        _startButton.interactable = false;
        //SetButtonNormalColor(false);

        _audioSource = GetComponent<AudioSource>();
    }

    public void OnHappyButtonClicked()
    {
        Consts.GameData.isHappy = true;
        moodButtonPressed = true;
        TryEnableStart();
    }
    
    public void OnSadButtonClicked()
    {
        Consts.GameData.isHappy = false;
        moodButtonPressed = true;
        TryEnableStart();
    }
    
    public void OnSweetButtonClicked()
    {
        Consts.GameData.isSweet = true;
        tasteButtonPressed = true;
        TryEnableStart();
    }
    
    public void OnSaltyButtonClicked()
    {
        Consts.GameData.isSweet = false;
        tasteButtonPressed = true;
        TryEnableStart();
    }

    private void TryEnableStart()
    {
        bool ready = moodButtonPressed && tasteButtonPressed;

        _message.SetActive(ready);
        _startButton.interactable = ready;
        //SetButtonNormalColor(ready);
    }

    private void SetButtonNormalColor(bool fullySaturated)
    {
        ColorBlock cb = _startButton.colors;
        //cb.normalColor = fullySaturated ? _saturatedColor : _desaturatedColor;
        _startButton.colors = cb;
    }

    public void OnStartButtonClicked()
    {
        if(moodButtonPressed && tasteButtonPressed)
        {
            Consts.GameData.recipe = new Recipe(Consts.GameData.characterSelected, Consts.GameData.isHappy, Consts.GameData.isSweet);
            Consts.GameData.recipe.PrintRecipe();
            //SceneManager.LoadScene(Consts.SceneNames.Kitchen);
            StartCoroutine(LoadSceneAfterSound(Consts.SceneNames.Kitchen));
        }
            
    }

    IEnumerator LoadSceneAfterSound(string scene)
    {
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
        SceneManager.LoadScene(scene);
    }
}
