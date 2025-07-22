using Consts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//è assegnato all'optionPanel
public class UI_OptionPanel : MonoBehaviour
{

    [SerializeField] private Slider _sliderSFX;
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private AudioSource _audioSource;


    public void Awake()
    {
        if (_sliderSFX == null || _sliderMusic == null)
        {
            Debug.Log("Riferimenti agli slider non trovati");
        }
        else
        {
            _sliderSFX.value = Consts.GameData.SFXvolume;
            _sliderMusic.value = Consts.GameData.musicVolume;
        }
    }

    public void OnHomeButtonClicked()
    {
        GameData.Reset();
        SceneManager.LoadScene(Consts.SceneNames.MainMenu);
    }

    // 0 in inspector is a placeholder
    public void OnSFXChanged(float value)
    {

        // ignore the 0 value in the inspector
        if (value == 0f && _sliderSFX.value != 0f)
        {
            Debug.Log("Ignorato valore placeholder 0");
            return;
        }

        Consts.GameData.SFXvolume = value;
        Debug.Log("SFX Volume = " + value);
    }

    public void OnSFXChangeEnd(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip, Consts.GameData.SFXvolume);
    }
        

    public void OnMusicChanged(float value)
    {
        Debug.Log("Music Volume = " + value);
        Consts.GameData.musicVolume = value;
        MusicManager.Instance.SetVolume(value);
    }   
}
