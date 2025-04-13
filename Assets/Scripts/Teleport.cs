using UnityEngine;

public class Teleport : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Teleportator()

    {
        System.Random random = new System.Random();
        this.transform.position = new Vector3(random.Next(0,12), random.Next(0, 11), this.transform.position.z);
    }
}
