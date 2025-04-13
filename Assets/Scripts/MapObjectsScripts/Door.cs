using UnityEngine;

public class Door : MonoBehaviour
{
    bool Oppened = false;
    [SerializeField] Sprite Oppenedsp;
    [SerializeField] Sprite Closedsp;
    bool Oppenedfirsttime = false;
    GameObject generator;



    private void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = Closedsp;
    }
    private void Update()
    {
        Debug.Log(this.GetComponent<BoxCollider2D>().enabled);
        generator = GameObject.FindGameObjectWithTag("Generator");

        if (Input.GetKeyDown(KeyCode.E))
            if (this.transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x < 2 && this.transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x > -2 && this.transform.position.y - GameObject.FindGameObjectWithTag("Player").transform.position.y < 2 && this.transform.position.y - GameObject.FindGameObjectWithTag("Player").transform.position.y > -2)
            {
                if (!Oppened)
                {
                    Oppened = true;
                    this.GetComponent<SpriteRenderer>().sprite = Oppenedsp;
                    this.GetComponent<BoxCollider2D>().enabled = false;
                    if (!Oppenedfirsttime)
                    {
                        Oppenedfirsttime = true;
                        if (MapSaver.alreadyvizited[(int)this.transform.position.x - 1, (int)this.transform.position.y] == false)
                            generator.GetComponent<NewMonoBehaviourScript>().FindRoom(MapSaver.vizited, MapSaver.map, MapSaver.alreadyvizited, (int)this.transform.position.x - 1, (int)this.transform.position.y, MapSaver.savezone, MapSaver.traider1);
                        else if (MapSaver.alreadyvizited[(int)this.transform.position.x + 1, (int)this.transform.position.y] == false)
                            generator.GetComponent<NewMonoBehaviourScript>().FindRoom(MapSaver.vizited, MapSaver.map, MapSaver.alreadyvizited, (int)this.transform.position.x + 1, (int)this.transform.position.y, MapSaver.savezone, MapSaver.traider1);
                        else if (MapSaver.alreadyvizited[(int)this.transform.position.x, (int)this.transform.position.y - 1] == false)
                            generator.GetComponent<NewMonoBehaviourScript>().FindRoom(MapSaver.vizited, MapSaver.map, MapSaver.alreadyvizited, (int)this.transform.position.x, (int)this.transform.position.y - 1, MapSaver.savezone, MapSaver.traider1);
                        else
                            generator.GetComponent<NewMonoBehaviourScript>().FindRoom(MapSaver.vizited, MapSaver.map, MapSaver.alreadyvizited, (int)this.transform.position.x, (int)this.transform.position.y + 1, MapSaver.savezone, MapSaver.traider1);
                    }
                }
                else
                {
                    Oppened = false;
                    this.GetComponent<SpriteRenderer>().sprite = Closedsp;
                    this.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
    }
}

