using UnityEngine;

public class Door : MonoBehaviour
{
    bool Oppened = false;
    bool Oppenedfirsttime = false;
    GameObject generator;
    

    private void FixedUpdate()
    {
        generator = GameObject.FindGameObjectWithTag("Generator");

        if (!Oppenedfirsttime && this.transform.position.x- GameObject.FindGameObjectWithTag("Player").transform.position.x<2 && this.transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x > -2 && this.transform.position.y - GameObject.FindGameObjectWithTag("Player").transform.position.y < 2 && this.transform.position.y - GameObject.FindGameObjectWithTag("Player").transform.position.y > -2)
        {
            Oppenedfirsttime=true;
            if (MapSaver.alreadyvizited[(int)this.transform.position.x - 1, (int)this.transform.position.y] == false)
                generator.GetComponent<NewMonoBehaviourScript>().FindRoom(MapSaver.vizited, MapSaver.map, MapSaver.alreadyvizited, (int)this.transform.position.x - 1, (int)this.transform.position.y);
            else if (MapSaver.alreadyvizited[(int)this.transform.position.x + 1, (int)this.transform.position.y] == false)
                generator.GetComponent<NewMonoBehaviourScript>().FindRoom(MapSaver.vizited, MapSaver.map, MapSaver.alreadyvizited, (int)this.transform.position.x + 1, (int)this.transform.position.y);
            else if (MapSaver.alreadyvizited[(int)this.transform.position.x, (int)this.transform.position.y-1] == false)
                generator.GetComponent<NewMonoBehaviourScript>().FindRoom(MapSaver.vizited, MapSaver.map, MapSaver.alreadyvizited, (int)this.transform.position.x, (int)this.transform.position.y-1);
            else
                generator.GetComponent<NewMonoBehaviourScript>().FindRoom(MapSaver.vizited, MapSaver.map, MapSaver.alreadyvizited, (int)this.transform.position.x, (int)this.transform.position.y + 1);
        }
    }

}//Input.GetKey(KeyCode.E)
