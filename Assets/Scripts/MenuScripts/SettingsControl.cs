using UnityEngine;
using UnityEngine.UI;

public class SettingsControl : MonoBehaviour
{
    public void FullScreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log(Screen.fullScreen);
    }

    [SerializeField] Slider sliderformusic;
    [SerializeField] Slider sliderforsounds;
    private float oldVolumeformusic;
    private float oldVolumeforsounds;

    private void FixedUpdate()
    {
        if (oldVolumeformusic != sliderformusic.value)
        {
            oldVolumeformusic = sliderformusic.value;
            PlayerPrefs.SetFloat("musicvolume" , sliderformusic.value);
            PlayerPrefs.Save();
        }




        if (oldVolumeforsounds != sliderforsounds.value)
        {
            oldVolumeforsounds = sliderforsounds.value;
            PlayerPrefs.SetFloat("soundsvolume", sliderforsounds.value);
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {

        if (!PlayerPrefs.HasKey("musicvolume"))
        {
            sliderformusic.value = 1;
            PlayerPrefs.SetFloat("musicvolume", sliderformusic.value);
            PlayerPrefs.Save();
        }
        else
            sliderformusic.value = PlayerPrefs.GetFloat("musicvolume");
        oldVolumeformusic = sliderformusic.value;




        if (!PlayerPrefs.HasKey("soundsvolume"))
        {
            sliderforsounds.value = 1;
            PlayerPrefs.SetFloat("soundsvolume", sliderforsounds.value);
            PlayerPrefs.Save();
        }
        else
            sliderforsounds.value = PlayerPrefs.GetFloat("soundsvolume");
        oldVolumeforsounds = sliderforsounds.value;
    }
}
