using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text Text;
    void Start()
    {

        if (SceneManager.GetActiveScene().name == "Betweenlevels")
            Text.SetText("Амулет зарядится через: "+ (PlayerPrefs.GetInt("timer")/3000).ToString() + " : "+((PlayerPrefs.GetInt("timer")/50%60).ToString()));
            
    }
    void Update()

    {
        if (SceneManager.GetActiveScene().name != "Betweenlevels")
            
        Text.SetText("Амулет зарядится через: " + (PlayerPrefs.GetInt("timer") / 3000).ToString() + " : " + ((PlayerPrefs.GetInt("timer") / 50 % 60).ToString()));
    }
}
