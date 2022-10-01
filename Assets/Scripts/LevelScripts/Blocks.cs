using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    Block[,] grid;

    void Start()
    {
        grid = new Block[3, 3];

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (x == 2 && y == 2)
                {
                    grid[2, 2] = null;
                    break;
                }
                grid[x, y] = transform.GetChild(x * 3 + y).gameObject.GetComponent<Block>();
                grid[x, y].setPos(x - 1, y - 1);
                print(x + "," + y + " " + grid[x, y].index);
            }
        }
    }

    public void Click(int x, int y)
    {
        if (x != 2 && grid[x + 1, y] == null)
        {
            grid[x, y].movePos(1, 0);
            grid[x + 1, y] = grid[x, y];
            grid[x, y] = null;
        }
        else if (x != 0 && grid[x - 1, y] == null)
        {
            grid[x, y].movePos(-1, 0);
            grid[x - 1, y] = grid[x, y];
            grid[x, y] = null;
        }
        else if (y != 2 && grid[x, y + 1] == null)
        {
            grid[x, y].movePos(0, 1);
            grid[x, y + 1] = grid[x, y];
            grid[x, y] = null;
        }
        else if (y != 0 && grid[x, y - 1] == null)
        {
            grid[x, y].movePos(0, -1);
            grid[x, y - 1] = grid[x, y];
            grid[x, y] = null;
        }
        else
        {
            print("can't move there");
        }
        print(DidWin());
    }

    bool DidWin()
    {
        return grid[0, 0] != null && grid[0, 0].index == 5 &&
               grid[1, 0] != null && grid[1, 0].index == 6 &&
               grid[2, 0] != null && grid[2, 0].index == 7 &&
               grid[0, 1] != null && grid[0, 1].index == 2 &&
               grid[1, 1] != null && grid[1, 1].index == 3 &&
               grid[0, 2] != null && grid[0, 2].index == 0 &&
               grid[1, 2] != null && grid[1, 2].index == 1;
    }
}