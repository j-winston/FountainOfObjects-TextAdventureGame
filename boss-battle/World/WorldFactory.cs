// World Factory 
namespace BossBattle.Core;

using BossBattle.Utilities;

public static class WorldFactory
{
    private static (int width, int height) _gridDimensions;

    public static World GenerateWorld(WorldSize worldSize, IRoomObserver gameEngineObserver)
    {
        SetGridDimensions(worldSize);

        var grid = new IRoom[_gridDimensions.width, _gridDimensions.height];

        // Add Normal and Hazard Rooms 
        AddRooms(grid, gameEngineObserver);

        // Add Entrance Room 
        AddEntranceRoom(grid, gameEngineObserver);

        // Add Fountain Room 
        AddFountainRoom(grid, gameEngineObserver);

        // Add a Pit Room 
        AddPitRoom(grid);

        // Add observers to each room
        AttachRoomObservers(grid, gameEngineObserver);

        return new World(grid);
    }

    private static void SetGridDimensions(WorldSize worldSize)
    {
        _gridDimensions = worldSize switch
        {
            WorldSize.Small => (4, 4),
            WorldSize.Medium => (6, 6),
            WorldSize.Large => (8, 8),
            _ => (4, 4),
        };

    }

    private static void AttachRoomObservers(IRoom[,] grid, IRoomObserver observer)
    {
        (int width, int height) = _gridDimensions;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y].AddObserver(observer);
            }
        }
    }

    private static void AddEntranceRoom(IRoom[,] grid, IRoomObserver observer)
    {
        grid[0, 0] = RoomFactory.CreateRoom("entrance");

    }

    // Generate basic filler rooms 
    private static void AddRooms(IRoom[,] grid, IRoomObserver observer)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Skip Entrance Room at (0,0)
                if (grid[x, y] is not EntranceRoom && grid[x, y] is not FountainRoom)
                {
                    // Generate Cavern Rooms 
                    string roomType = (x + y) % 2 == 0 ? "normal" : "hazard";
                    grid[x, y] = RoomFactory.CreateRoom(roomType);
                }
            }
        }
    }


    private static void AddFountainRoom(IRoom[,] grid, IRoomObserver observer)
    {
        // Create the Fountain Room in a random location and pass in observer 
        (int fountainX, int fountainY) = GetRandomCoordinate(grid);

        // If the coordinates are 0,0 (Entrance Room) get another set
        if (fountainX == 0 && fountainY == 0)
            (fountainX, fountainY) = GetRandomCoordinate(grid);

        grid[fountainX, fountainY] = RoomFactory.CreateRoom("fountain");
        grid[fountainX, fountainY].AddObserver(observer);
    }

    private static void AddPitRoom(IRoom[,] grid)
    {
        // Add pits based on map size 
        var numberOfPits = (_gridDimensions.width / 4 == 1) ? 1 : _gridDimensions.width / 4;

        // Continue looping over each room in the map until 
        // numberOfPits are placed. 
        for (int i = 0; i < numberOfPits; i++)
        {
            (int x, int y) = GetRandomCoordinate(grid);

            // Add a pit if it's a hazard room 
            if ((grid[x, y] is HazardRoom))
            {
                grid[x, y].HasPit = true;
            }
            // Otherwise, loop through again 
            else
                i--;
        }
    }


    private static void CreateFountainRoom(IRoom[,] grid)
    {
        grid[0, 2] = RoomFactory.CreateRoom("fountain");

    }

    private static (int x, int y) GetRandomCoordinate(IRoom[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        Random random = new Random();

        int x = random.Next(0, width - 1);
        int y = random.Next(0, height - 1);

        return (x, y);

    }

    private static void SetEntranceRoom(IRoom[,] grid)
    {
        grid[0, 0] = RoomFactory.CreateRoom("entrance");

    }






}
