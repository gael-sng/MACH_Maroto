using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour {

    [Header("0 - Level music. 1 - Boss")]
    public AudioClip[] audioList;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }


    public void playBossTheme()
    {
        FadeOut(source, 1);
        source.clip = audioList[1];
        FadeIn(source, 1, 1);
    }

    public void playLevelTheme()
    {
        FadeOut(source, 1);
        source.clip = audioList[0];
        FadeIn(source, 1, 1);
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime, float volume)
    {

        audioSource.Play();

        while (audioSource.volume < volume && audioSource.volume < 1)
        {
            audioSource.volume += volume * Time.deltaTime / FadeTime;

            yield return null;
        }

        
     
    }
}
