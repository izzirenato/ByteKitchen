using UnityEngine;
using UnityEngine.SceneManagement;
using Consts;
using System.Collections;


//è assegnato ai 4 bottoni di scelta del personaggio
public class UI_Characters : MonoBehaviour
{
    [SerializeField] private Characters _character;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnCharacterButtonClicked()
    {
        GameData.characterSelected = _character;
        StartCoroutine(LoadSceneAfterSound(SceneNames.Preferences));
        //SceneManager.LoadScene(SceneNames.Preferences);
    }

    IEnumerator LoadSceneAfterSound(string scene)
    {
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
        SceneManager.LoadScene(scene);
    }
}
