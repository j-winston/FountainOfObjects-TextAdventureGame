// World
// Responsibilities: 
using BossBattle.Utilities;

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
        if (DoesRoomExist(x, y))
        {
            if (Grid[x, y] is not null)
            {
                return Grid[x, y];
            }

        }
        return null;
    }

    public bool DoesRoomExist(int x, int y)
    {
        if (x >= 0 && y >= 0)
            return (x < Grid.GetLength(0) && y < Grid.GetLength(1));
        return false;
    }

    public IRoom GetEntranceRoom()
    {
        return Grid[0, 0];

    }


}
