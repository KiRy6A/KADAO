using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;


public class MiniMapGenerator : MonoBehaviour
    {
        [SerializeField] GameObject panel;
        [SerializeField] List<Image> images = new List<Image>();
    public void GenerateMiniMap(int[,] map, bool[,] visited)
    {



        Image newimage;
        for (int i = 0; i < map.GetLength(0)-1; i++)
            for (int j = 0; j < map.GetLength(1)-1; j++)
                if (visited[i,j])
            {
                if (i == 0 || j == 0 || i == map.GetLength(0) - 2 || i == map.GetLength(1) - 2)
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            newimage = images[3];
                        }
                        else if (j == map.GetLength(1) - 2)
                        {
                            newimage = images[4];
                        }
                        else if (map[i+1,j]==1 || map[i + 1, j] == 3)
                            newimage = images[8];
                        else 
                            newimage = images[12];
                    }
                    else if(i== map.GetLength(0) - 2)
                        if (j == 0)
                        {
                            newimage = images[6];
                        }
                        else if (j == map.GetLength(1) - 2)
                        {
                            newimage = images[5];
                        }
                        else if (map[i - 1, j] == 1 || map[i - 1, j] == 3)
                            newimage = images[9];
                        else
                            newimage = images[12];
                    else if(j==0)
                    {
                        if (map[i, j+1] == 1 || map[i, j + 1] == 3)
                            newimage = images[7];
                        else
                            newimage = images[11];
                    }
                    else
                    {
                        if (map[i, j - 1] == 1 || map[i, j - 1] == 3)
                            newimage = images[9];
                        else
                            newimage = images[11];
                    }
                else if(map[i, j]==2)
                    newimage = images[2];
                else if(map[i, j]==-1)
                    newimage = images[14];
                else if (map[i, j] == -2)
                    newimage = images[15];
                else if (map[i, j] == 3)
                    if (map[i-1, j] == 1)
                        newimage = images[0];
                    else
                        newimage = images[1];
                else
                    {
                    int[] dx = { -1, 1, 0, 0 };
                    int[] dy = { 0, 0, -1, 1 };
                    int counter = 0;
                    for (int d = 0; d < 4; d++)
                    {
                        if (map[i + dx[d], j+dy[d]] == 1 || map[i + dx[d], j + dy[d]] == 3)
                            counter++;
                    }
                    if (counter == 4)
                        newimage = images[13];
                    else if(counter ==3)
                        if (map[i-1,j]==2)
                            newimage = images[8];
                        else if (map[i, j-1] == 2)
                            newimage = images[7];
                        else if (map[i+1, j] == 2)
                            newimage = images[9];
                        else
                            newimage = images[10];
                    else
                    {
                        if ((map[i-1,j]==1 || map[i - 1, j]==3) && (map[i + 1, j] == 1 || map[i + 1, j] == 3))
                            newimage = images[11];
                        else if((map[i , j-1] == 1 || map[i, j-1] == 3) && (map[i, j+1] == 1 || map[i , j+1] == 3))
                            newimage = images[12];
                        else if (map[i-1 , j]==1 || map[i-1, j]==3)
                            if(map[i , j-1]==1 ||  map[i, j-1]==3)
                                newimage = images[5];
                            else
                                newimage = images[6];
                        else 
                        { 
                            if (map[i, j - 1] == 1 || map[i, j - 1] == 3)
                                newimage = images[4];
                            else
                                newimage = images[3];
                        }
                    }
                }
                Instantiate(newimage, new Vector3(panel.transform.position.x + i * 8 - 118, panel.transform.position.y + j * 8 - 118, 1f), Quaternion.identity, panel.transform);
            }
    }
    }

