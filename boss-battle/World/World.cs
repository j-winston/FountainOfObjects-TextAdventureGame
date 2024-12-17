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

}
