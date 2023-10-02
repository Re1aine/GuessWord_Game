using UnityEngine;

public interface IAudioService
{
    void PlayBackGroundMusic(AudioClip clip);
    void PlayShortSound(AudioClip clip);
    void OneShotPlaySoundEffect(AudioClip clip);
    void StopBackGroundMusic();
    void StopShortSoundEffects();
    void SetGeneralVolume(float volume);
    void SetVolumeBackGroundMusic(float volume);
    void SetVolumeShortSoundEffect(float volume);
}