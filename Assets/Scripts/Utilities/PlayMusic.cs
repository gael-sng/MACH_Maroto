using UnityEngine;
using System.Collections;

//Changes music playing. Used for boss theme transition
public class PlayMusic : MonoBehaviour {

    [Header("0 - Level music. 1 - Boss")]
    public AudioClip[] audioList;
    private AudioSource source;
    private string status;
    [Header("Number between 0 and 1")]
    public float maxVolume = 1;
    [Header("Duration (in seconds) of fade")]
    public float fadeSpeed = 2f;
    private int playing = 0;

    void Start()
    {
        source = GetComponent<AudioSource>();
        status = "playing";
    }

    void Update()
    {
        if (status.Equals("fadingOut"))
        {
            if (source.volume > 0)
            {
               source.volume-=Time.deltaTime/fadeSpeed;
            }else
            {
                source.volume = 0;
                playing = (playing + 1) % 2;
                source.clip = audioList[playing];
                source.Play();
                status = "fadingIn";
            }

        }else
        {
            if (status.Equals("fadingIn"))
            {
                if (source.volume < maxVolume)
                {
                    source.volume += Time.deltaTime / fadeSpeed;
                }
                else
                {
                    source.volume = maxVolume;
                    status = "playing";
                }
            }
        }
    }

    //Call this when boss appears and when it dies
    public void changeMusic()
    {
        status = "fadingOut";
    }



   
}
