using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour
{
    float radius = 1.5f;
    void Update() {
        if (Input.GetKeyDown(KeyCode.E))
            if (this.transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x < radius && this.transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x > -radius && this.transform.position.y - GameObject.FindGameObjectWithTag("Player").transform.position.y < radius && this.transform.position.y - GameObject.FindGameObjectWithTag("Player").transform.position.y > -radius)
            {
                if (SceneManager.GetActiveScene().name == "LevelScene")
                {
                    SceneManager.LoadScene("Betweenlevels");
                    PlayerPrefs.SetInt("floorcounter", PlayerPrefs.GetInt("floorcounter") + 1);
                    PlayerPrefs.Save();
                }
                else
                {
                    if (PlayerPrefs.GetInt("floorcounter")<6)

                    SceneManager.LoadScene("LevelScene");

                    else
                        SceneManager.LoadScene("BossFight");
                }
            }
}
}
