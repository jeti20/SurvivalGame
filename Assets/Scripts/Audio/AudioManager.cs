using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Sound Effects Settings")]
    [SerializeField] private AudioSource effectsSource; // AudioSource do efekt�w d�wi�kowych
    [SerializeField] private AudioSource walkSoundSource; // AudioSource do d�wi�ku chodzenia
    [SerializeField] private AudioSource birdsSoundSource; // AudioSource do d�wi�ku ptak�w

    [Header("Sound Effects Clips")]
    public AudioClip axeSound; // Klip d�wi�ku siekiery
    public AudioClip pickaxeSound; // Klip d�wi�ku kilofa
    [SerializeField] private AudioClip birdsSound; // Klip d�wi�ku ptak�w

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
        // Odtwarzanie d�wi�ku ptak�w, je�li przypisano klip
        if (birdsSoundSource != null && birdsSound != null)
        {
            birdsSoundSource.clip = birdsSound;
            birdsSoundSource.loop = true; // Zap�tlenie d�wi�ku ptak�w
            birdsSoundSource.Play();
        }
    }

    /// Odtwarzanie jednorazowego d�wi�ku.
    public void PlaySound(AudioClip clip)
    {
        if (effectsSource != null && clip != null)
        {
            effectsSource.PlayOneShot(clip);
        }
    }

    /// Zatrzymanie odtwarzania d�wi�ku, je�li jest aktywny.
    public void StopSound(AudioSource source)
    {
        if (source != null && source.isPlaying)
        {
            source.Stop();
        }
    }

    /// Odtwarzanie d�wi�ku chodzenia.
    public void PlayWalkSound()
    {
        if (walkSoundSource != null && !walkSoundSource.isPlaying)
        {
            walkSoundSource.Play();
        }
    }

    /// <summary>
    /// Zatrzymanie d�wi�ku chodzenia.
    /// </summary>
    public void StopWalkSound()
    {
        if (walkSoundSource != null && walkSoundSource.isPlaying)
        {
            walkSoundSource.Stop();
        }
    }
}
