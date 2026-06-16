using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("---- Audio Source ----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---- Audio Clip ----")]
    public AudioClip background;
    public AudioClip jump;
    public AudioClip prepareJump;
    public AudioClip fall;
    public AudioClip hurt;
    public AudioClip die;
    public AudioClip collectible;
    public AudioClip grapeAttack;


    public AudioClip soldieAttack;
    public AudioClip soldieHurt;
    public AudioClip soldieDie;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
