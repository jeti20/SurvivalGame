using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Sound Effects Settings")]
    [SerializeField] private AudioSource effectsSource; // AudioSource do efektów dŸwiêkowych
    [SerializeField] private AudioSource walkSoundSource; // AudioSource do dŸwiêku chodzenia
    [SerializeField] private AudioSource birdsSoundSource; // AudioSource do dŸwiêku ptaków

    [Header("Sound Effects Clips")]
    public AudioClip axeSound; // Klip dŸwiêku siekiery
    public AudioClip pickaxeSound; // Klip dŸwiêku kilofa
    [SerializeField] private AudioClip birdsSound; // Klip dŸwiêku ptaków

    public static AudioManager instance; // Singleton

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Utrzymanie AudioManager przy zmianie scen
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Odtwarzanie dŸwiêku ptaków, jeœli przypisano klip
        if (birdsSoundSource != null && birdsSound != null)
        {
            birdsSoundSource.clip = birdsSound;
            birdsSoundSource.loop = true; // Zapêtlenie dŸwiêku ptaków
            birdsSoundSource.Play();
        }
    }

    /// Odtwarzanie jednorazowego dŸwiêku.
    public void PlaySound(AudioClip clip)
    {
        if (effectsSource != null && clip != null)
        {
            effectsSource.PlayOneShot(clip);
        }
    }

    /// Zatrzymanie odtwarzania dŸwiêku, jeœli jest aktywny.
    public void StopSound(AudioSource source)
    {
        if (source != null && source.isPlaying)
        {
            source.Stop();
        }
    }

    /// Odtwarzanie dŸwiêku chodzenia.
    public void PlayWalkSound()
    {
        if (walkSoundSource != null && !walkSoundSource.isPlaying)
        {
            walkSoundSource.Play();
        }
    }

    /// <summary>
    /// Zatrzymanie dŸwiêku chodzenia.
    /// </summary>
    public void StopWalkSound()
    {
        if (walkSoundSource != null && walkSoundSource.isPlaying)
        {
            walkSoundSource.Stop();
        }
    }
}
