// WorldManager:  
// Responsibilities: Manages room state and provides interfaces to GameEngine for room queries  
using System.Collections.Generic;
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

    public bool DoesRoomContainPit(int x, int y)
    {
        if (DoesRoomExist(x, y))
            return _world.Grid[x, y].HasPit;
        return false;
    }

    public bool IsAdjacentToPit(int x, int y)
    {
        var rooms = GetAdjacentRooms(x, y);

        foreach (var room in rooms)
        {
            if (room.HasPit == true)
                return true;
        }

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

    public List<IRoom> GetAdjacentRooms(int roomX, int roomY)
    {
        List<IRoom> adjacentRooms = new List<IRoom>();

        if (roomX == 0 & roomY == 0)
        {
            // Orthogonal from 0,0
            adjacentRooms.Add(_world.Grid[roomX + 1, roomY]);
            adjacentRooms.Add(_world.Grid[roomX, roomY + 1]);

            // Diagonal from 0,0 
            adjacentRooms.Add(_world.Grid[roomX, roomY + 1]);
        }
        else
        {
            // Orthogonal 
            adjacentRooms.Add(_world.Grid[roomX - 1, roomY]);
            adjacentRooms.Add(_world.Grid[roomX + 1, roomY]);
            adjacentRooms.Add(_world.Grid[roomX, roomY - 1]);
            adjacentRooms.Add(_world.Grid[roomX, roomY + 1]);

            // Diagonal 
            adjacentRooms.Add(_world.Grid[roomX - 1, roomY - 1]);
            adjacentRooms.Add(_world.Grid[roomX + 1, roomY + 1]);
            adjacentRooms.Add(_world.Grid[roomX - 1, roomY + 1]);
            adjacentRooms.Add(_world.Grid[roomX + 1, roomY - 1]);
        }

        return adjacentRooms;






    }


}
