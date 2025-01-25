using UnityEngine;
    using AYellowpaper.SerializedCollections;

public class SoundManager : Singleton<SoundManager>
{ 
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;  
    [SerializeField] private AudioSource sfxSource;    

    [Header("Music Clips")]
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip gameMusic;
    [Header("SFX Groups")]
    public SerializedDictionary<SFXType,SFXGroup> sfxGroups;  

 
    private float lastMusicVolume; 
    private float lastSFXVolume; 

    public void PlayMainMenuMusic()
    {
        PlayMusic(mainMenuMusic);
    }
 
    public void PlayGameMusic()
    {
        PlayMusic(gameMusic);
    }

    public void Mute(){
        lastMusicVolume = musicSource.volume;
        lastSFXVolume = sfxSource.volume;
        musicSource.volume = 0;
        sfxSource.volume = 0;
    }

    public void UnMute(){
        musicSource.volume = lastMusicVolume;
        sfxSource.volume = lastSFXVolume;
    }
 
    private void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }
 
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
 
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
     public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
 
    public void PlayRandomSFX(SFXType sfxType)
    {
        SFXGroup group = sfxGroups[sfxType];
        if (group != null && group.clips.Length > 0)
        {
            AudioClip clip = group.clips[Random.Range(0, group.clips.Length)];
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"'{sfxType}' için ses efekti bulunamadı!");
        }
    }
    
}
