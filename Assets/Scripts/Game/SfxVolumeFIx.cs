using UnityEngine;
using Consts;

public class SfxVolumeFix : MonoBehaviour
{
    //[SerializeField] private AudioSource _audioSource;

    private AudioSource _audio;

    private void Awake()
    {
        _audio = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        _audio.volume = Consts.GameData.SFXvolume;
    }

    //public void SFXVolume()
    //{
    //    _audioSource.volume = Consts.GameData.SFXvolume;
    //}
}
