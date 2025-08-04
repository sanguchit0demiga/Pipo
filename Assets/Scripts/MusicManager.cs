using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;
    private AudioSource audioSource;
    public AudioClip bgm;
    public bool notMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance=this;
            audioSource = GetComponent<AudioSource>();
            if (notMenu)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if(bgm != null)
        {
            PlayBGM(false, bgm);
        }
    }

    private void PlayBGM(bool resetSong,AudioClip audioClip=null)
    {
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
        }
        else if (audioSource.clip != null)
        {
            if (resetSong)
            {
                audioSource.Stop();
            }
            audioSource.Play();
        }
    }
}
