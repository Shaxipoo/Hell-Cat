using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip mainTheme;

    public void PlayMusic(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void PlayNormalMusic()
    {
        audioSource.clip = mainTheme;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
