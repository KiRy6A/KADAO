using UnityEngine;

public class Teleport : MonoBehaviour
{
    void Teleportator()

    {
        System.Random random = new System.Random();
        this.transform.position = new Vector3(random.Next(0,12), random.Next(0, 11), this.transform.position.z);
    }
}
