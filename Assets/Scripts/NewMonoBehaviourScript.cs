using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject tile;
    [SerializeField] GameObject itemtile;
    [SerializeField] GameObject starttile;
    [SerializeField] GameObject finishtile;
    [SerializeField] int sizer;
    [SerializeField] int sized;
    [SerializeField] short numbr;

    [SerializeField] GameObject MapGenerator;

    System.Random random = new System.Random();
    public static void RandomizeArray<T>(T[] array)
    {
        System.Random random = new System.Random();

        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = random.Next(0, i + 1);
            T temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
    public static bool ContainsOrNot(int[] array, int h)
    {
        foreach (int piece in array)
        {
            if (piece == h)
                return true;
        }
        return false;
    }
    public int[,] GenerateRooms(int downsize,int rightsize, short number)
    {
        int[,] matrix;
        int x = downsize + 1;
        int y = rightsize + 1;
        short num = number;
        int[,] rooms = new int[num, 4];
        matrix = new int[x, y];
        short enter = 255;
        short exit = 254;
        short door = 253;
        short numbergraphs = 0;
        for (short k = 1; k <= num; k++)
        {
            int x1 = random.Next(2, x - 4);
            int y1 = random.Next(2, y - 4);
            if (matrix[x1 - 1, y1 - 1] == 0 && matrix[x1 + 3, y1 - 1] == 0 && matrix[x1 - 1, y1 + 3] == 0 && matrix[x1 + 3, y1 + 3] == 0)
            {
                for (int i = x1 - 1; i <= x1 + 3; i++)
                    for (int j = y1 - 1; j <= y1 + 3; j++)
                        matrix[i, j] = k;
            }
            else
                k--;
        }
        for (short g = 0; g < Math.Max(x, y) - 3; g++)
            for (short k = 1; k <= num; k++)
            {
                int a = -1, b = -1, c = -1, d = -1;
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        if (matrix[i, j] == k)
                        {
                            a = i;
                            b = j;
                            for (int z = i + 1; z < x; z++)
                            {
                                if (z >= x || matrix[z, j] != k)
                                {
                                    c = z - 1;
                                    break;
                                }
                            }
                            for (int t = j + 1; t < y; t++)
                            {
                                if (t >= y || matrix[i, t] != k)
                                {
                                    d = t - 1;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    if ((a != -1) || (b != -1) || (c != -1) || (d != -1))
                        break;
                }
                short[] order = new short[4] { 1, 2, 3, 4, };
                RandomizeArray(order);
                for (int l = 0; l < 4; l++)
                {
                    if (order[l] == 1 && a - 1 >= 0)
                    {
                        int count = 0;
                        for (int t = b; t <= d; t++)
                        {
                            if (matrix[a - 1, t] != 0)
                            {
                                count += 1;
                                break;
                            }
                        }
                        if (count == 0)
                        {
                            a -= 1;
                            for (int t = b; t <= d; t++)
                                matrix[a, t] = k;
                        }
                    }

                    if (order[l] == 2 && b - 1 >= 0)
                    {
                        int count = 0;
                        for (int t = a; t <= c; t++)
                        {
                            if (matrix[t, b - 1] != 0)
                            {
                                count += 1;
                                break;
                            }
                        }
                        if (count == 0)
                        {
                            b -= 1;
                            for (int t = a; t <= c; t++)
                                matrix[t, b] = k;
                        }
                    }

                    if (order[l] == 3 && c + 2 < x)
                    {
                        int count = 0;
                        for (int t = b; t <= d; t++)
                        {
                            if (matrix[c + 1, t] != 0)
                            {
                                count += 1;
                                break;
                            }
                        }
                        if (count == 0)
                        {
                            c += 1;
                            for (int t = b; t <= d; t++)
                                matrix[c, t] = k;
                        }
                    }

                    if (order[l] == 4 && d + 2 < y)
                    {
                        int count = 0;
                        for (int t = a; t <= c; t++)
                        {
                            if (matrix[t, d + 1] != 0)
                            {
                                count += 1;
                                break;
                            }
                        }
                        if (count == 0)
                        {
                            d += 1;
                            for (int t = a; t <= c; t++)
                                matrix[t, d] = k;
                        }
                    }
                }
                rooms[k - 1, 0] = a;
                rooms[k - 1, 1] = b;
                rooms[k - 1, 2] = c;
                rooms[k - 1, 3] = d;
            }

        for (short k = 0; k < num; k++)
        {
            if (rooms[k, 0] > 1)
                for (int i = rooms[k, 1] + 1; i < rooms[k, 3]; i++)
                {
                    if (matrix[rooms[k, 0] - 1, i] != 0 && (matrix[rooms[k, 0] - 1, i] == matrix[rooms[k, 0] - 1, i + 1] || matrix[rooms[k, 0] - 1, i + 1] == enter) && (matrix[rooms[k, 0] - 1, i] == matrix[rooms[k, 0] - 1, i - 1] || matrix[rooms[k, 0] - 1, i - 1] == enter))
                    {
                        matrix[rooms[k, 0] - 1, i] = enter;
                        matrix[rooms[k, 0], i] = exit;
                    }
                }
            if (rooms[k, 1] > 1)
                for (int i = rooms[k, 0] + 1; i < rooms[k, 2]; i++)
                {
                    if (matrix[i, rooms[k, 1] - 1] != 0 && (matrix[i, rooms[k, 1] - 1] == matrix[i + 1, rooms[k, 1] - 1] || matrix[i + 1, rooms[k, 1] - 1] == enter) && (matrix[i, rooms[k, 1] - 1] == matrix[i - 1, rooms[k, 1] - 1] || matrix[i - 1, rooms[k, 1] - 1] == enter))
                    {
                        matrix[i, rooms[k, 1] - 1] = enter;
                        matrix[i, rooms[k, 1]] = exit;
                    }
                }
        }

        for (int i = 0; i < x - 1; i++)
            for (int j = 0; j < y - 1; j++)
            {
                if (matrix[i, j] == enter)
                {
                    numbergraphs++;
                    if (matrix[i + 1, j] == enter)
                    {
                        int newi;
                        int count = 1;
                        while (matrix[i + count, j] == enter)
                            count += 1;
                        newi = random.Next(i, i + count);
                        matrix[newi, j] = door;
                        matrix[newi, j + 1] = door;

                        for (int m = i; m < i + count; m++)
                            if (matrix[m, j] == enter)
                            {
                                matrix[m, j] = matrix[m, j - 1];
                                matrix[m, j + 1] = matrix[m, j + 2];

                            }
                    }
                    else if (matrix[i, j + 1] == enter)
                    {
                        int newj;
                        int count = 1;
                        while (matrix[i, j + count] == enter)
                            count += 1;
                        newj = random.Next(j, j + count);
                        matrix[i, newj] = door;
                        matrix[i + 1, newj] = door;

                        for (int m = j; m < j + count; m++)
                            if (matrix[i, m] == enter)
                            {
                                matrix[i, m] = matrix[i - 1, m];
                                matrix[i + 1, m] = matrix[i + 2, m];
                            }
                    }
                    else
                    {
                        matrix[i, j] = door;
                        if (matrix[i, j + 1] == exit)
                            matrix[i, j + 1] = door;
                        else if (matrix[i + 1, j] == exit)
                            matrix[i + 1, j] = door;
                    }
                }
            }
        int[,] graph = new int[numbergraphs, 3];
        for (int i = 0; i < x - 1; i++)
            for (int j = 0; j < y - 1; j++)
            {
                if (matrix[i, j] == door)
                {
                    if (matrix[i + 1, j] == door)
                    {
                        bool check = false;
                        for (int k = 0; k < numbergraphs; k++)
                            if (graph[k, 0] == Math.Max(matrix[i - 1, j], matrix[i + 2, j]) && graph[k, 1] == Math.Min(matrix[i - 1, j], matrix[i + 2, j]))
                            {
                                check = true;
                                break;
                            }
                        if (check != true)
                        {
                            for (int k = 0; k < numbergraphs; k++)
                                if (graph[k, 0] == 0)
                                {
                                    graph[k, 0] = Math.Max(matrix[i - 1, j], matrix[i + 2, j]);
                                    graph[k, 1] = Math.Min(matrix[i - 1, j], matrix[i + 2, j]);
                                    graph[k, 2] = Math.Abs(rooms[graph[k, 0] - 1, 0] - rooms[graph[k, 1] - 1, 0]) + Math.Abs(rooms[graph[k, 0] - 1, 1] - rooms[graph[k, 1] - 1, 1]);
                                    break;
                                }
                        }

                    }
                    else if (matrix[i, j + 1] == door)
                    {
                        bool check = false;
                        for (int k = 0; k < numbergraphs; k++)
                            if (graph[k, 0] == Math.Max(matrix[i, j - 1], matrix[i, j + 2]) && graph[k, 1] == Math.Min(matrix[i, j - 1], matrix[i, j + 2]))
                            {
                                check = true;
                                break;
                            }
                        if (check != true)
                        {
                            for (int k = 0; k < numbergraphs; k++)
                                if (graph[k, 0] == 0)
                                {
                                    graph[k, 0] = Math.Max(matrix[i, j - 1], matrix[i, j + 2]);
                                    graph[k, 1] = Math.Min(matrix[i, j - 1], matrix[i, j + 2]);
                                    graph[k, 2] = Math.Abs(rooms[graph[k, 0] - 1, 0] - rooms[graph[k, 1] - 1, 0]) + Math.Abs(rooms[graph[k, 0] - 1, 1] - rooms[graph[k, 1] - 1, 1]);
                                    break;
                                }
                        }
                    }
                }
            }

        int[,] newgraph = new int[numbergraphs, 2];
        int[] stack = new int[num];
        stack[0] = random.Next(1, num + 1);
        int stacknum = 1;
        while (stack[num - 1] == 0)
        {
            int[] stackgraph = new int[3];
            stackgraph[2] = 999;
            for (int k = 0; k < numbergraphs; k++)
            {
                if (((ContainsOrNot(stack, graph[k, 0]) && !ContainsOrNot(stack, graph[k, 1])) || (!ContainsOrNot(stack, graph[k, 0]) && ContainsOrNot(stack, graph[k, 1]))) && graph[k, 2] < stackgraph[2])
                {
                    stackgraph[0] = graph[k, 0];
                    stackgraph[1] = graph[k, 1];
                    stackgraph[2] = graph[k, 2];
                }
            }
            if (ContainsOrNot(stack, stackgraph[0]))
                stack[stacknum] = stackgraph[1];
            else
                stack[stacknum] = stackgraph[0];
            newgraph[stacknum - 1, 0] = stackgraph[0];
            newgraph[stacknum - 1, 1] = stackgraph[1];
            stacknum++;
        }



        for (int i = 0; i < x - 1; i++)
            for (int j = 0; j < y - 1; j++)
            {
                if (matrix[i, j] == door && matrix[i + 1, j] == door)
                {
                    bool check = false;
                    for (int k = 0; k < stacknum - 1; k++)
                    {
                        if ((newgraph[k, 0] == matrix[i - 1, j] && newgraph[k, 1] == matrix[i + 2, j]) || (newgraph[k, 1] == matrix[i - 1, j] && newgraph[k, 0] == matrix[i + 2, j]))
                        {
                            check = true;
                            break;
                        }
                    }
                    if (check == false)
                    {
                        matrix[i, j] = matrix[i - 1, j];
                        matrix[i + 1, j] = matrix[i + 2, j];
                    }
                }
                if (matrix[i, j] == door && matrix[i, j + 1] == door)
                {
                    bool check = false;
                    for (int k = 0; k < stacknum - 1; k++)
                    {
                        if ((newgraph[k, 0] == matrix[i, j - 1] && newgraph[k, 1] == matrix[i, j + 2]) || (newgraph[k, 1] == matrix[i, j - 1] && newgraph[k, 0] == matrix[i, j + 2]))
                        {
                            check = true;
                            break;
                        }
                    }
                    if (check == false)
                    {
                        matrix[i, j] = matrix[i, j - 1];
                        matrix[i, j + 1] = matrix[i, j + 2];
                    }
                }
            }
        for (short k = 0; k < num; k++)
            for (int i = rooms[k, 0]; i <= rooms[k, 2]; i++)
                for (int j = rooms[k, 1]; j <= rooms[k, 3]; j++)
                {
                    if (matrix[i, j] == door)
                        matrix[i, j] = 3;
                    else if (i == rooms[k, 0] || i == rooms[k, 2] || j == rooms[k, 1] || j == rooms[k, 3])
                        matrix[i, j] = 1;
                    else
                        matrix[i, j] = 2;

                }
        for (int i = 1; i < x - 2; i++)
            for (int j = 1; j < y - 2; j++)
            {
                if ((matrix[i, j] == 1 || matrix[i, j] == 3) && (matrix[i - 1, j] == 2 || matrix[i, j - 1] == 2) && (matrix[i + 1, j] != 2 || matrix[i, j + 1] != 2))
                    matrix[i, j] = 4;
                else if ((matrix[i, j] == 1 || matrix[i, j] == 3) && matrix[i - 1, j] == 4 && matrix[i, j - 1] == 4)
                    matrix[i, j] = 4;
                if (matrix[i, j] == 0 && (matrix[i - 1, j] == 4 || matrix[i, j - 1] == 4))
                    matrix[i, j] = 1;


            }
        for (int i = 1; i < x - 2; i++)
            for (int j = 1; j < y - 2; j++)
            {
                if (matrix[i, j] == 4)
                    matrix[i, j] = 2;
                if (matrix[i, j] == 0)
                {
                    if (matrix[i + 1, j] != 0 && matrix[i, j + 1] != 0 && matrix[i - 1, j] != 0 && matrix[i, j - 1] != 0)
                    {
                        if (i + 1 == x - 1 || i - 1 == 0)
                        {
                            matrix[i, j + 1] = 3;
                            matrix[i, j] = 2;
                            matrix[i, j - 1] = 3;
                        }
                        else if (j + 1 == y - 1 || j - 1 == 0)
                        {
                            matrix[i + 1, j] = 3;
                            matrix[i, j] = 2;
                            matrix[i - 1, j] = 3;
                        }
                        else
                        {
                            int ind = random.Next(0, 2);
                            if (ind == 0)
                            {
                                matrix[i + 1, j] = 3;
                                matrix[i, j] = 2;
                                matrix[i - 1, j] = 3;
                            }
                            else
                            {
                                matrix[i, j + 1] = 3;
                                matrix[i, j] = 2;
                                matrix[i, j - 1] = 3;
                            }
                        }
                    }
                    if (matrix[i + 1, j] == 0 && matrix[i, j + 1] == 0)
                    {
                        int right = 1;
                        int down = 1;
                        while (true)
                        {
                            if (matrix[i + down, j] == 0)
                                down += 1;
                            else
                                break;
                        }
                        while (true)
                        {
                            if (matrix[i, j + right] == 0)
                                right += 1;
                            else
                                break;
                        }
                        int wall = random.Next(1, 5);
                        if (wall == 1)
                        {
                            if (i - 1 != 0)
                                for(int c=j; c<j+right; c++)
                                    matrix[i - 1, c] = 2;
                            else
                                for (int c = j; c < j + right; c++)
                                    matrix[i + down, c] = 2;
                        }
                        else if (wall == 2)
                        {
                            if (j + right != y)
                                for (int c = i; c < i + down; c++)
                                    matrix[c, j+right] = 2;
                            else
                                for (int c = i; c < i + down; c++)
                                    matrix[c, j-1] = 2;
                        }
                        else if (wall == 3)
                        {
                            if (i + down == x)
                                for (int c = j; c < j + right; c++)
                                    matrix[i - 1, c] = 2;
                            else
                                for (int c = j; c < j + right; c++)
                                    matrix[i + down, c] = 2;
                        }
                        else
                        {
                            if (j - 1 == 0)
                                for (int c = i; c < i + down; c++)
                                    matrix[c, j + right] = 2;
                            else
                                for (int c = i; c < i + down; c++)
                                    matrix[c, j - 1] = 2;
                        }
                        for (int i1 = i; i1 < i + down; i1++)
                            for (int j1 = j; j1 < j + right; j1++)
                                matrix[i1, j1] = 2;
                    }

                    if (matrix[i, j + 1] == 0)
                    {
                        matrix[i, j - 1] = 3;
                        matrix[i, j] = 2;
                        int ind = 1;
                        while (matrix[i, j + ind] == 0)
                        {
                            matrix[i, j + ind] = 2;
                            ind++;
                        }
                        if (j + ind != y - 2)
                            matrix[i, j + ind] = 3;
                    }
                    else if (matrix[i + 1, j] == 0)
                    {
                        matrix[i - 1, j] = 3;
                        matrix[i, j] = 2;
                        int ind = 1;
                        while (matrix[i + ind, j] == 0)
                        {
                            matrix[i + ind, j] = 2;
                            ind++;
                        }
                        if (i + ind != x - 2)
                            matrix[i + ind, j] = 3;
                    }
                }
            }
        for (int i = 1; i < x - 2; i++)
            for (int j = 1; j < y - 2; j++)
            { if (matrix[i, j] == 1 && matrix[i + 1, j] != 2 && matrix[i, j + 1] != 2 && matrix[i + 1, j + 1] != 2)
                {
                    if (matrix[i - 1, j - 1] == 2 && matrix[i - 1, j] == 2 && matrix[i, j - 1] == 2)
                        matrix[i, j] = 2;
                    else if (matrix[i - 1, j] != 2 && matrix[i - 1, j + 1] != 2 && matrix[i, j - 1] == 2 && matrix[i + 1, j - 1] == 2)
                        matrix[i, j] = 2;
                    else if (matrix[i - 1, j] == 2 && matrix[i - 1, j + 1] == 2 && matrix[i, j - 1] != 2 && matrix[i + 1, j - 1] != 2)
                        matrix[i, j] = 2;
                    else if (matrix[i - 1, j - 1] == 2 && matrix[i - 1, j + 1] != 2 && matrix[i + 1, j - 1] != 2 && matrix[i, j - 1] != matrix[i - 1, j])
                        matrix[i, j] = 2;
                }
            }
        return matrix;
    }

    public Vector3[] GenerateStartAndFinish(int[,] matrix, int downsize, int rightsize)
    {
        int x = downsize + 1;
        int y = rightsize + 1;
        int startx = 1;
        int starty = 1;
        int finishx = 1;
        int finishy = 1;
        short[,] matrix1 = new short[x, y];
        short step = 1;
        for (int i = 0; i < x - 1; i++)
        {
            for (int j = 0; j < y - 1; j++)
            {
                matrix1[i, j] = 0;
            }

        }
        while (true)
        {
            startx = random.Next(1, x - 2);
            starty = random.Next(1, y - 2);
            if (matrix[startx, starty] == 2)
                break;
        }
        matrix1[startx, starty] = 1;
        bool nextstep = false;
        while (true)
        {
            nextstep = false;
            step += 1;
            for (int i = 0; i < x - 1; i++)
            {
                for (int j = 0; j < y - 1; j++)
                {
                    if (matrix[i, j] != 1 && matrix1[i, j] == 0 && (matrix1[i - 1, j] == step - 1 || matrix1[i, j - 1] == step - 1 || matrix1[i + 1, j] == step - 1 || matrix1[i, j + 1] == step - 1))
                    {
                        matrix1[i, j] = step;
                        finishx = i;
                        finishy = j;
                        nextstep = true;
                    }

                }
            }
            if (!nextstep)
                break;
        }
        step = 1;
        for (int i = 0; i < x - 1; i++)
        {
            for (int j = 0; j < y - 1; j++)
            {
                matrix1[i, j] = 0;
            }

        }
        matrix1[finishx, finishy] = 1;
        nextstep = false;
        while (true)
        {
            nextstep = false;
            step += 1;
            for (int i = 0; i < x - 1; i++)
            {
                for (int j = 0; j < y - 1; j++)
                {
                    if (matrix[i, j] != 1 && matrix1[i, j] == 0 && (matrix1[i - 1, j] == step - 1 || matrix1[i, j - 1] == step - 1 || matrix1[i + 1, j] == step - 1 || matrix1[i, j + 1] == step - 1))
                    {
                        matrix1[i, j] = step;
                        startx = i;
                        starty = j;
                        nextstep = true;
                    }

                }
            }
            if (!nextstep)
                break;
        }
            return new UnityEngine.Vector3[]

        {
            new UnityEngine.Vector3(finishx, finishy, 1f),
            new UnityEngine.Vector3(startx, starty, 1f)
        };
    }

    public int[,] MakePointBigger(int[,] matrix, Vector3 point1, int FinishOrStart)
    {
        Vector3 point = point1;
        int corners = 0;
        if (matrix[(int)point.x-1, (int)point.y] != 2)
            corners++;
        if (matrix[(int)point.x, (int)point.y-1] != 2)
            corners++;
        if (matrix[(int)point.x+1, (int)point.y] != 2)
            corners++;
        if (matrix[(int)point.x, (int)point.y+1] != 2)
            corners++;

        if (corners > 2)
        {
            matrix[(int)point.x, (int)point.y] = 1;
            if (matrix[(int)point.x - 1, (int)point.y] != 2)
                point.x = (int)point.x - 1;
            else if (matrix[(int)point.x, (int)point.y - 1] != 2)
                point.y = (int)point.y - 1;
            else if (matrix[(int)point.x + 1, (int)point.y] != 2)
                point.x = (int)point.x + 1;
            else 
                point.y = (int)point.y + 1;
        }
        if (matrix[(int)point.x - 1, (int)point.y] == 2)
                if (matrix[(int)point.x, (int)point.y - 1] == 2)
                {
                    matrix[(int)point.x, (int)point.y] = FinishOrStart;
                    matrix[(int)point.x, (int)point.y - 1] = FinishOrStart;
                    matrix[(int)point.x - 1, (int)point.y] = FinishOrStart;
                    matrix[(int)point.x -1 , (int)point.y - 1 ] = FinishOrStart;
                }
                else
                {
                    matrix[(int)point.x, (int)point.y] = FinishOrStart;
                    matrix[(int)point.x, (int)point.y + 1] = FinishOrStart;
                    matrix[(int)point.x - 1, (int)point.y] = FinishOrStart;
                    matrix[(int)point.x - 1, (int)point.y + 1] = FinishOrStart;
                }
            else
            {
                if (matrix[(int)point.x, (int)point.y - 1] == 2)
                {
                    matrix[(int)point.x, (int)point.y] = FinishOrStart;
                    matrix[(int)point.x, (int)point.y - 1] = FinishOrStart;
                    matrix[(int)point.x + 1, (int)point.y] = FinishOrStart;
                    matrix[(int)point.x + 1, (int)point.y - 1] = FinishOrStart;
                }
                else
                {
                    matrix[(int)point.x, (int)point.y] = FinishOrStart;
                    matrix[(int)point.x, (int)point.y + 1] = FinishOrStart;
                    matrix[(int)point.x + 1, (int)point.y] = FinishOrStart;
                    matrix[(int)point.x + 1, (int)point.y + 1] = FinishOrStart;
                }
            }
        if(corners>2)
            matrix[(int)point1.x, (int)point1.y] = 2;
        return matrix;
    }
    public class RoomProcessor
    {
        public void ConnectDoors(int[,] map)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);
            bool[,] visited = new bool[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (map[i, j] == 2 && !visited[i, j])
                    {
                        var roomCells = new HashSet<Tuple<int, int>>();
                        var doors = new HashSet<Tuple<int, int>>();
                        BFS(map, visited, i, j, roomCells, doors);

                        ProcessRoom(map, roomCells, doors);
                    }
                }
            }
        }

        private void BFS(int[,] map, bool[,] visited, int startX, int startY, HashSet<Tuple<int, int>> roomCells, HashSet<Tuple<int, int>> doors)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);
            var queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(Tuple.Create(startX, startY));
            visited[startX, startY] = true;
            roomCells.Add(Tuple.Create(startX, startY));

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                int x = current.Item1;
                int y = current.Item2;

                for (int d = 0; d < 4; d++)
                {
                    int nx = x + dx[d];
                    int ny = y + dy[d];

                    if (nx >= 0 && nx < rows && ny >= 0 && ny < cols)
                    {
                        if (map[nx, ny] == 2 && !visited[nx, ny])
                        {
                            visited[nx, ny] = true;
                            roomCells.Add(Tuple.Create(nx, ny));
                            queue.Enqueue(Tuple.Create(nx, ny));
                        }
                        else if (map[nx, ny] == 3 || map[nx, ny] == -1  || map[nx, ny] == -2)
                        {
                            doors.Add(Tuple.Create(nx, ny));
                        }
                    }
                }
            }
        }

        private void ProcessRoom(int[,] map, HashSet<Tuple<int, int>> roomCells, HashSet<Tuple<int, int>> doors)
        {
            if (doors.Count == 0) return;

            var doorList = doors.ToList();

            if (doorList.Count == 1)
            {
                var entry = FindEntryPoint(roomCells, doorList[0]);
                if (entry == null) return;

                var center = FindCenter(roomCells);
                if (center == null) return;

                var path = FindShortestPath(map, entry.Item1, entry.Item2, center.Item1, center.Item2, roomCells);
                if (path != null)
                {
                    foreach (var point in path)
                    {
                        map[point.Item1, point.Item2] = 4;
                    }
                }
            }
            else
            {
                var entryPoints = new List<Tuple<int, int>>();
                foreach (var door in doorList)
                {
                    var entry = FindEntryPoint(roomCells, door);
                    if (entry != null) entryPoints.Add(entry);
                }

                if (entryPoints.Count < 2) return;

                var edges = new List<Edge>();
                for (int i = 0; i < entryPoints.Count; i++)
                {
                    for (int j = i + 1; j < entryPoints.Count; j++)
                    {
                        var a = entryPoints[i];
                        var b = entryPoints[j];
                        var path = FindShortestPath(map, a.Item1, a.Item2, b.Item1, b.Item2, roomCells);
                        if (path != null)
                        {
                            edges.Add(new Edge { U = i, V = j, Weight = path.Count, Path = path });
                        }
                    }
                }

                edges.Sort((e1, e2) => e1.Weight.CompareTo(e2.Weight));
                var uf = new UnionFind(entryPoints.Count);
                foreach (var edge in edges)
                {
                    if (uf.Find(edge.U) != uf.Find(edge.V))
                    {
                        uf.Union(edge.U, edge.V);
                        foreach (var point in edge.Path)
                        {
                            if (map[point.Item1, point.Item2] == 2)
                                map[point.Item1, point.Item2] = 4;
                        }
                    }
                }
            }
        }

        private Tuple<int, int> FindEntryPoint(HashSet<Tuple<int, int>> roomCells, Tuple<int, int> door)
        {
            int x = door.Item1;
            int y = door.Item2;
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            for (int d = 0; d < 4; d++)
            {
                int nx = x + dx[d];
                int ny = y + dy[d];
                var cell = Tuple.Create(nx, ny);
                if (roomCells.Contains(cell)) return cell;
            }
            return null;
        }

        private Tuple<int, int> FindCenter(HashSet<Tuple<int, int>> roomCells)
        {
            double avgX = roomCells.Average(c => c.Item1);
            double avgY = roomCells.Average(c => c.Item2);

            var center = roomCells.OrderBy(c => Math.Pow(c.Item1 - avgX, 2) + Math.Pow(c.Item2 - avgY, 2)).FirstOrDefault();
            return center;
        }

        private List<Tuple<int, int>> FindShortestPath(int[,] map, int startX, int startY, int endX, int endY, HashSet<Tuple<int, int>> roomCells)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);
            var visited = new bool[rows, cols];
            var parent = new Tuple<int, int>[rows, cols];
            var queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(Tuple.Create(startX, startY));
            visited[startX, startY] = true;

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                int x = current.Item1;
                int y = current.Item2;

                if (x == endX && y == endY)
                {
                    var path = new List<Tuple<int, int>>();
                    while (current != null)
                    {
                        path.Add(current);
                        current = parent[current.Item1, current.Item2];
                    }
                    path.Reverse();
                    return path;
                }

                for (int d = 0; d < 4; d++)
                {
                    int nx = x + dx[d];
                    int ny = y + dy[d];

                    if (nx >= 0 && nx < rows && ny >= 0 && ny < cols && (map[nx, ny] == 2 || map[nx, ny] == 4) && roomCells.Contains(Tuple.Create(nx, ny)) && !visited[nx, ny])
                    {
                        visited[nx, ny] = true;
                        parent[nx, ny] = current;
                        queue.Enqueue(Tuple.Create(nx, ny));
                    }
                }
            }
            return null;
        }

        private class Edge
        {
            public int U { get; set; }
            public int V { get; set; }
            public int Weight { get; set; }
            public List<Tuple<int, int>> Path { get; set; }
        }

        private class UnionFind
        {
            private int[] parent;

            public UnionFind(int size)
            {
                parent = new int[size];
                for (int i = 0; i < size; i++)
                    parent[i] = i;
            }

            public int Find(int x)
            {
                if (parent[x] != x)
                    parent[x] = Find(parent[x]);
                return parent[x];
            }

            public void Union(int x, int y)
            {
                int rootX = Find(x);
                int rootY = Find(y);
                if (rootX != rootY)
                    parent[rootY] = rootX;
            }
        }
    }

    private void Start()
    {
        int[,] map = GenerateRooms(sizer, sized, numbr);
        Vector3[] startandfinish = GenerateStartAndFinish(map, sized, sizer);
        map = MakePointBigger(map, startandfinish[0], -2);
        map = MakePointBigger(map, startandfinish[1], -1);

        int[,] mapwithitems = (int[,])map.Clone();
        
        var processor = new RoomProcessor();
        processor.ConnectDoors(mapwithitems);

        for (int i = 0; i < sized + 1; i++)
            for (int j = 0; j < sizer + 1; j++)
                if (mapwithitems[i, j] != 2)
                    mapwithitems[i, j] = 1;
                else
                    mapwithitems[i, j] = 0;

        for (int i=0;i<sized; i++)
            for(int j=0;j<sizer; j++)
            {
                Vector3 pos = new Vector3(i,j, 1f);
                if (mapwithitems[i,j] != 2)
                {
                    if (map[i, j] ==1)
                        Instantiate(tile, pos, UnityEngine.Quaternion.identity);
                    else if (map[i, j] == 4)
                        Instantiate(itemtile, pos, UnityEngine.Quaternion.identity);
                    else if (map[i, j] == -1)
                        Instantiate(starttile, pos, UnityEngine.Quaternion.identity);
                    else if (map[i, j] == -2)
                        Instantiate(finishtile, pos, UnityEngine.Quaternion.identity);
                }
            }
        MapGenerator.GetComponent<MiniMapGenerator>().GenerateMiniMap(map);
    }

}
