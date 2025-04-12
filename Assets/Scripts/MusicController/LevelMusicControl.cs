using UnityEngine;

public class LevelMusicControl : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] AudioClip[] musicPlaying;

    private void Start()
    {
        music.PlayOneShot(musicPlaying[PlayerPrefs.GetInt("floorcounter")-1], PlayerPrefs.GetFloat("musicvolume"));
    }

    private void FixedUpdate()
    {
        if(!music.isPlaying && PlayerPrefs.GetInt("floorcounter") < musicPlaying.Length)
        {
            music.PlayOneShot(musicPlaying[PlayerPrefs.GetInt("floorcounter") - 1], PlayerPrefs.GetFloat("musicvolume"));
        }
    }
}
