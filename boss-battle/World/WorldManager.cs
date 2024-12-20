// WorldManager:  
// Responsibilities: Manages room state and provides interfaces to GameEngine for room queries  
using System.Collections.Generic;
using BossBattle.Utilities;

namespace BossBattle.Core;

public class WorldManager
{
    private readonly World _world;
    private int _worldWidth;
    private int _worldHeight;

    public WorldManager(World world)
    {
        _world = world;
        _worldWidth = world.Grid.GetLength(0);
        _worldHeight = world.Grid.GetLength(1);
    }

    public World GetWorld()
    {
        return _world;
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

        if (x < _worldWidth && y < _worldHeight)
            if (_world.Grid[x, y] is not null)
                return true;
        return false;
    }

    public bool DoesRoomContainPit(IRoom room)
    {
        return room.HasPit;
    }

    public bool IsAdjacentToPit(int x, int y)
    {
        if (DoesRoomExist(x, y))
        {
            var rooms = GetAdjacentRooms(x, y);
            foreach (var room in rooms)
            {
                if (room.HasPit == true)
                    Console.WriteLine("has a pit");
                return true;
            }
        }


        return false;
    }

    public bool ContainsFountain(IRoom room)
    {
        return room is FountainRoom;
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

    private List<IRoom> GetAdjacentRooms(int roomX, int roomY)
    {

        List<IRoom> adjacentRooms = new List<IRoom>();

        if (roomX == 0 && roomY == 0)
        {
            // Orthogonal from 0,0
            adjacentRooms.Add(_world.Grid[roomX + 1, roomY]);
            adjacentRooms.Add(_world.Grid[roomX, roomY + 1]);

            // Diagonal from 0,0 
            adjacentRooms.Add(_world.Grid[roomX, roomY + 1]);
        }

        // Check for outOfBounds error
        else if ((roomX > 1 && roomX < _worldWidth - 1) && (roomY > 1 && roomY < _worldHeight - 1))
        {
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
