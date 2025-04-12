using UnityEngine;

public class MainMenuMusicControl : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] AudioClip[] musicPlaying;
    System.Random random = new System.Random();
    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicvolume"))
        {
            PlayerPrefs.SetFloat("musicvolume", 1);
            PlayerPrefs.Save();
            music.PlayOneShot(musicPlaying[random.Next(0, musicPlaying.Length)], PlayerPrefs.GetFloat("musicvolume"));
        }
    }
    private void FixedUpdate()
    {
            if (music.volume != PlayerPrefs.GetFloat("musicvolume"))
                music.volume = PlayerPrefs.GetFloat("musicvolume");
            if(!music.isPlaying)
            {
                music.PlayOneShot(musicPlaying[random.Next(0, musicPlaying.Length)], PlayerPrefs.GetFloat("musicvolume"));
            }
        
    }
}
