using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void PlayPressed()
    {
        if (!PlayerPrefs.HasKey("floorcounter"))
        {
            PlayerPrefs.SetInt("floorcounter", 1);
            PlayerPrefs.Save();
        }
        else if(PlayerPrefs.GetInt("floorcounter")>5)
        {
            PlayerPrefs.SetInt("floorcounter", 1);
            PlayerPrefs.Save();
        }
        PlayerPrefs.SetInt("floorcounter", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("LevelScene");
    }
    public void ExitPressed()
    {
        Application.Quit();
    }
}
