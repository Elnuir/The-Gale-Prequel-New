using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SoundVolumeSlider : MonoBehaviour
{
    private MMSoundManager _manager;
    private Slider _slider;

    public enum SoundTypes
    {
        Master,
        Music,
        Sfx
    }

    public SoundTypes SoundType;

    private void Start()
    {
        _manager = FindObjectOfType<MMSoundManager>();
        _slider = GetComponent<Slider>();

        switch (SoundType)
        {
            case SoundTypes.Master:
                _slider.value = _manager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Master, false) ;
                _slider.onValueChanged.AddListener(SetMasterVolume);
                break;
            case SoundTypes.Music:
                _slider.value = _manager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Music, false) ;
                _slider.onValueChanged.AddListener(SetMusicVolume);
                break;
            case SoundTypes.Sfx:
                _slider.value = _manager.GetTrackVolume(MMSoundManager.MMSoundManagerTracks.Sfx, false) ;
                _slider.onValueChanged.AddListener(SetSfxVolume);
                break;
            default:
                Debug.LogError("Добавь ебаную ветку в свич");
                break;
        }
    }

    public void SetMasterVolume(float value)
    {
        _manager.SetVolumeMaster(value);
    }

    public void SetMusicVolume(float value)
    {
        _manager.SetVolumeMusic(value);
    }

    public void SetSfxVolume(float value)
    {
        _manager.SetVolumeSfx(value);
    }
}