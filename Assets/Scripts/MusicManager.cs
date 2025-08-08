using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance; 
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource missSFX;
    [SerializeField] private AudioSource hitSFX;

    [SerializeField] private AudioMixer mixer;

    public static MusicManager Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetMusicVolume(float volume)
    {
        if (volume <= 0f)
            mixer.SetFloat("Music", -80f);
        else
            mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void PlayMissSFX()
    {
        if (missSFX != null)
        {
            missSFX.Play();
        }
    }

    public void PlayHitSFX()
    {
        if (hitSFX != null)
        {
            hitSFX.Play();
        }
    }

    public void SetSFXVolume(float volume)
    {
        mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}
