using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    public AudioClip axeSound;
    public AudioClip pickaxeSound;
    [SerializeField] private AudioSource effectsSource; // Dodatkowe Ÿród³o dŸwiêku na efekty
    //[Header("Audio clip")]
    public AudioClip background;
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySound(AudioClip clip)
    {
        if (effectsSource != null && clip != null)
        {
            effectsSource.PlayOneShot(clip);
        }
    }

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

}
