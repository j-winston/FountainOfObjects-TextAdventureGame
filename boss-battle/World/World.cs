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

        return Grid[x, y] ?? null;

    }



}
