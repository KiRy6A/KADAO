using UnityEngine;

public class Door : MonoBehaviour
{
    bool Oppened = false;
    bool Oppenedfirsttime = false;
    GameObject generator;
    

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E) && this.transform.position.x- GameObject.FindGameObjectWithTag("Player").transform.position.x<2 && this.transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x > -2 && this.transform.position.y - GameObject.FindGameObjectWithTag("Player").transform.position.y < 2 && this.transform.position.y - GameObject.FindGameObjectWithTag("Player").transform.position.y > -2)
        {
         //   generator = GameObject.FindGameObjectWithTag("Generator");
        //    generator.GetComponent<NewMonoBehaviourScript>().FindRoom(generator.GetComponent<NewMonoBehaviourScript>().);
        }
    }

}
