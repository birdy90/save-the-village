using UnityEngine;

public class AudioManager: MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource EffectSource;

    public bool IsMuted => MusicSource.mute;

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
    }

    public void PlaySound(AudioClip clip)
    {
        EffectSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void Toggle()
    {
        EffectSource.mute = !EffectSource.mute;
        MusicSource.mute = !MusicSource.mute;
    }
}