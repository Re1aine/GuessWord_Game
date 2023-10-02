using System;
using UnityEngine;

public class AudioService : IAudioService
{
    private readonly IGameFactory _gameFactory;
    private AudioPlayer _audioPlayer;

    public AudioService(IGameFactory gameFactory)
    {
        _gameFactory = gameFactory;
        
        Initialize();
    }

    private void Initialize()
    {
        _audioPlayer = _gameFactory.CreateAudioPlayer();
        _gameFactory.Init(this);
    }

    public void PlayBackGroundMusic(AudioClip clip) => 
        _audioPlayer.PlayOnBackGroundPlayer(clip);

    public void PlayShortSound(AudioClip clip) => 
        _audioPlayer.PlayOnShortSoundEffectPlayer(clip);

    public void OneShotPlaySoundEffect(AudioClip clip) => 
        _audioPlayer.OneShotPlaySoundEffect(clip);

    public void StopBackGroundMusic() => 
        _audioPlayer.StopBackGroundMusic();

    public void StopShortSoundEffects() => 
        _audioPlayer.StopShortSoundEffects();

    public void SetGeneralVolume(float volume) => 
        _audioPlayer.SetGeneralVolume(volume);

    public void SetVolumeBackGroundMusic(float volume) => 
        _audioPlayer.SetVolumeBackGroundMusic(volume);

    public void SetVolumeShortSoundEffect(float volume) => 
        _audioPlayer.SetVolumeShortSoundEffect(volume);
}