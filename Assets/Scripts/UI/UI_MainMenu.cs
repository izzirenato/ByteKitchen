using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//è assegnato ai bottoni del MainMenuCanvas
public class UI_MainMenu : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void OnPlayButtonClicked()
    {
        // SceneManager.LoadScene(Consts.SceneNames.Intro);
        StartCoroutine(LoadSceneAfterSound(Consts.SceneNames.Intro));
    }

    public void OnQuitButtonClicked()
    {
        StartCoroutine(ApplicationQuit());
    }

    IEnumerator LoadSceneAfterSound(string scene)
    {
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
        SceneManager.LoadScene(scene);
    }

    IEnumerator ApplicationQuit()
    {
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
        Application.Quit();
    }
}
