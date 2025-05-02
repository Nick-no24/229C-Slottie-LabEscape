using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip bgMusic;
    public AudioClip consoleOpen;
    public AudioClip shoot;
    public AudioClip lightning;
    public AudioClip hurt;
    public AudioClip lavaDeath;
    public AudioClip itemPickup;
    public AudioClip buttonClick;
    public AudioClip jumpShroom;
    public AudioClip startgame;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayMusic(bgMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
