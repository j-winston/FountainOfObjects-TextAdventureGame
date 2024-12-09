// World
// Responsibilities: 

namespace BossBattle.Core;

public class World
{
    public readonly IRoom[,] Grid;


    public World(IRoom[,] grid)
    {
        Grid = grid;

    }

    public IRoom? GetRoomAt(int x, int y)
    {
        if (x < Grid.GetLength(0) && y < Grid.GetLength(1))
        {
            return Grid[x, y];
        }
        else
        {
            return null;

        }


    }



}
