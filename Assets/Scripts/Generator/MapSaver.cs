using UnityEngine;

public class MapSaver : MonoBehaviour
{
    public static bool[,] vizited;
    public static bool[,] alreadyvizited;
    public static int[,] map;
    [SerializeField] GameObject generator;
    public void Save(bool[,] viz, bool[,] alr, int[,] m)
    {
        vizited = viz;
        alreadyvizited = alr;
        map = m;
    }
}
