using UnityEngine;

public class MapSaver : MonoBehaviour
{
    public static bool[,] vizited;
    public static bool[,] alreadyvizited;
    public static int[,] map;
    [SerializeField] GameObject generator;
    public static int[,] savezone;
    public static Vector2 traider1;
    public void Save(bool[,] viz, bool[,] alr, int[,] m, int[,] save, Vector2 traider)
    {
        vizited = viz;
        alreadyvizited = alr;
        map = m;
        savezone = save;
        traider1 = traider;

    }
}
