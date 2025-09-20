using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [Header("UI Sliders")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    
    private float _currentMusicVolume;
    private float _currentSfxVolume;

    private void Start()
    {
        _currentMusicVolume = DataManager.Instance.MusicVolume;
        _currentSfxVolume = DataManager.Instance.SfxVolume;
        
        musicSlider.value = _currentMusicVolume;
        sfxSlider.value = _currentSfxVolume;
    }
    
    

    public void UpdateVolume()
    {
        _currentMusicVolume = musicSlider.value;
        _currentSfxVolume = sfxSlider.value;
        
        DataManager.Instance.MusicVolume = _currentMusicVolume;
        DataManager.Instance.SfxVolume = _currentSfxVolume;
        
        DataManager.Instance.SaveMusicVolume(DataManager.Instance.MusicVolume);
        DataManager.Instance.SaveSfxVolume(DataManager.Instance.SfxVolume);
        
        Au
    }
}
