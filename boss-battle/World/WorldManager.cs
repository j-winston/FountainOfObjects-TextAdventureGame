// WorldManager:  
// Responsibilities: Manages room state and provides interfaces to GameEngine for room queries  

using BossBattle.Utilities;

namespace BossBattle.Core;

public class WorldManager
{
    private readonly World _world;

    public WorldManager(World world)
    {
        _world = world;
    }

    public IRoom? GetRoomAt(int x, int y)
    {
        if (DoesRoomExist(x, y))
        {
            if (_world.Grid[x, y] is not null)
            {
                return _world.Grid[x, y];
            }
        }

        return null;
    }

    public bool DoesRoomExist(int x, int y)
    {
        if (x >= 0 && y >= 0)
            return (x < _world.Grid.GetLength(0) && y < _world.Grid.GetLength(1));
        return false;
    }

    public EntranceRoom GetEntranceRoom()
    {
        return (EntranceRoom)_world.Grid[0, 0];
    }

    public (int width, int height) GetDimensions(WorldSize size)
    {
        return size switch
        {
            WorldSize.Small => (4, 4),
            WorldSize.Medium => (6, 6),
            WorldSize.Large => (8, 8),
            _ => (4, 4)

        };
    }


}
