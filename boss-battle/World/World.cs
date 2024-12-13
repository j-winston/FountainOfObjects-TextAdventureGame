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
            return Grid[x, y];
        }

        else
        {
            return null;
        }

    }

    public bool DoesRoomExist(int x, int y)
    {
        return (x < Grid.GetLength(0) && y < Grid.GetLength(1));
    }




}
