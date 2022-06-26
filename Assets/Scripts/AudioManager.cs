using UnityEngine;
public class AudioManager: MonoBehaviour
{
    /// <summary>
    /// Singleton instance of AudioManager
    /// </summary>
    public static AudioManager Instance;

    /// <summary>
    /// Music source
    /// </summary>
    [SerializeField] private AudioSource MusicSource;
    
    /// <summary>
    /// Sound effects source
    /// </summary>
    [SerializeField] private AudioSource EffectSource;

    /// <summary>
    /// Property computing the state of audio (muted or not)
    /// </summary>
    public bool IsMuted => MusicSource.mute;

    /// <summary>
    /// Create instance on Awake
    /// </summary>
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

    /// <summary>
    /// Play sound effect one time
    /// </summary>
    /// <param name="clip">Clip to be played</param>
    public void PlaySound(AudioClip clip)
    {
        EffectSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Toggle all sounds on/off
    /// </summary>
    public void Toggle()
    {
        EffectSource.mute = !EffectSource.mute;
        MusicSource.mute = !MusicSource.mute;
    }
}