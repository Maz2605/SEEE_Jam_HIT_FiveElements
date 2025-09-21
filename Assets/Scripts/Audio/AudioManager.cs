using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    public AudioSource MusicSource => _musicSource;
    [SerializeField] private AudioSource _soundSource;
    public AudioSource SoundSource => _soundSource;

    [Header("Music")]
    [SerializeField] private AudioClip _musicStartGame;
    [SerializeField] private AudioClip _musicSelectLevel;
    [SerializeField] private AudioClip _musicInGame;
    [SerializeField] private AudioClip _musicMaxBar;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _selectLevel;
    [SerializeField] private AudioClip _attack;
    [SerializeField] private AudioClip _skill1;
    [SerializeField] private AudioClip _skill2;
    [SerializeField] private AudioClip _skill3;
    [SerializeField] private AudioClip _ultimate;
    [SerializeField] private AudioClip _enemyDeath;
    [SerializeField] private AudioClip _winWave;
    [SerializeField] private AudioClip _winGame;
    [SerializeField] private AudioClip _loseGame;
    [SerializeField] private AudioClip _useCoin;
    [SerializeField] private AudioClip _collectCoin;

    private const float DefaultMusicFade = 0.5f;

    protected override void Awake()
    {
        base.KeepActive(true);
        base.Awake();

        if (_musicSource == null || _soundSource == null)
        {
            Debug.LogError("AudioSource not assigned in AudioManager!");
            
        }
        
        PlayMusicStartGame();
    }

    private void Start()
    {
        
    }

    #region Volume & Save
    public void SetMusicVolume(float volume)
    {
        if (_musicSource != null)
        {
            _musicSource.volume = Mathf.Clamp01(volume);
            DataManager.Instance.MusicVolume = _musicSource.volume;
            DataManager.Instance.SaveMusicVolume(DataManager.Instance.MusicVolume);
            _musicSource.volume = DataManager.Instance.MusicVolume;
        }
    }

    public void SetSoundVolume(float volume)
    {
        if (_soundSource != null)
        {
            _soundSource.volume = Mathf.Clamp01(volume);
            DataManager.Instance.SfxVolume = _soundSource.volume;
            DataManager.Instance.SaveSfxVolume(DataManager.Instance.SfxVolume);
            _soundSource.volume = DataManager.Instance.SfxVolume;
        }
    }

    public void ResetDefault()
    {
        float def = 0.5f;
        if (_musicSource != null) _musicSource.volume = def;
        if (_soundSource != null) _soundSource.volume = def;
        SetMusicVolume(def);
        SetSoundVolume(def);
    }

    public void SaveVolumes()
    {
        if (DataManager.Instance != null)
        {
            
        }
    }
    #endregion

    #region Music Play / Stop (with fade)
    public void PlayMusicStartGame()
    {
        if (_musicSource != null && _musicStartGame != null)
            PlayMusicGame(_musicStartGame);
    }

    public void PlayMusicSelectLevel()
    {
        if (_musicSource != null && _musicSelectLevel != null)
            PlayMusicGame(_musicSelectLevel);
    }

    public void PlayMusicInGame()
    {
        if (_musicSource != null && _musicInGame != null)
            PlayMusicGame(_musicInGame);
    }

    public void PlayMusicMaxBar()
    {
        if (_musicSource != null && _musicMaxBar != null)
            PlayMusicGame(_musicMaxBar);
    }

    public void PlayMusicGame(AudioClip clip, float fadeDuration = DefaultMusicFade)
    {
        if (_musicSource == null || clip == null) return;

        if (_musicSource.isPlaying && _musicSource.clip == clip) return;

        _musicSource.loop = true;
        _musicSource.clip = clip;
        _musicSource.volume = 0f;
        _musicSource.Play();

        float targetVol = DataManager.Instance.MusicVolume;
        

        _musicSource.DOFade(targetVol, fadeDuration).SetUpdate(true);
    }

    public void StopMusic(float fadeDuration = DefaultMusicFade)
    {
        if (_musicSource != null && _musicSource.isPlaying)
        {
            _musicSource.DOFade(0f, fadeDuration).OnComplete(() =>
            {
                _musicSource.Stop();
            }).SetUpdate(true);
        }
    }

    public void PauseMusic()
    {
        if (_musicSource != null && _musicSource.isPlaying)
            _musicSource.Pause();
    }

    public void ResumeMusic()
    {
        if (_musicSource != null && !_musicSource.isPlaying && _musicSource.clip != null)
            _musicSource.UnPause();
    }
    #endregion

    #region SFX Play / Stop
    public void PlaySFX(AudioClip sound, bool repeat = false)
    {
        if (sound == null || _soundSource == null) return;

        if (repeat)
        {
            _soundSource.loop = true;
            _soundSource.clip = sound;
            if (!_soundSource.isPlaying)
                _soundSource.Play();
        }
        else
        {
            if (_soundSource.loop)
            {
                _soundSource.loop = false;
            }
            _soundSource.PlayOneShot(sound, _soundSource.volume);
        }
    }

    public void StopSFX()
    {
        if (_soundSource != null && _soundSource.isPlaying)
        {
            _soundSource.Stop();
            _soundSource.loop = false;
            _soundSource.clip = null;
        }
    }

    public void PauseSFX()
    {
        if (_soundSource != null && _soundSource.isPlaying)
            _soundSource.Pause();
    }

    public void ResumeSFX()
    {
        if (_soundSource != null && !_soundSource.isPlaying && _soundSource.clip != null)
            _soundSource.UnPause();
    }
    #endregion

    #region Convenience wrappers (named clips)
    public void PlaySoundClickButton() { PlaySFX(_buttonClick); }
    public void PlaySelectLevel() { PlaySFX(_selectLevel); }
    public void PlayAttack() { PlaySFX(_attack); }
    public void PlaySkill1() { PlaySFX(_skill1); }
    public void PlaySkill2() { PlaySFX(_skill2); }
    public void PlaySkill3() { PlaySFX(_skill3); }
    public void PlayUltimate() { PlaySFX(_ultimate); }
    public void PlayEnemyDeath() { PlaySFX(_enemyDeath); }
    public void PlayWinWave() { PlaySFX(_winWave); }
    public void PlayWinGame() { PlaySFX(_winGame); }
    public void PlayLoseGame() { PlaySFX(_loseGame); }
    public void PlayUseCoin() { PlaySFX(_useCoin); }
    public void PlayCollectCoin() { PlaySFX(_collectCoin); }
    #endregion
}
