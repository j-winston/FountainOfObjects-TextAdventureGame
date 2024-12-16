// World Factory 
namespace BossBattle.Core;

using BossBattle.Utilities;

public class WorldFactory
{

    public static World GenerateWorld(WorldSize worldSize, IRoomObserver gameEngineObserver)
    {
        (int width, int height) = GetDimensions(worldSize);

        var grid = new IRoom[width, height];
        GameDisplay consoleDisplay = new GameDisplay();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                string roomType = (x + y) % 2 == 0 ? "normal" : "hazard";

                grid[x, y] = RoomFactory.CreateRoom(roomType, consoleDisplay);
                grid[x, y].AddObserver(gameEngineObserver);
            }

            // Special Rooms
            grid[0, 0] = RoomFactory.CreateRoom("entrance", consoleDisplay);
            grid[0, 0].AddObserver(gameEngineObserver);

            grid[0, 2] = RoomFactory.CreateRoom("fountain", consoleDisplay);
            grid[0, 2].AddObserver(gameEngineObserver);

        }

        return new World(grid);

    }

    private static (int width, int height) GetDimensions(WorldSize size)
    {
        return size switch
        {
            WorldSize.Small => (4, 4),
            WorldSize.Medium => (6, 6),
            WorldSize.Large => (8, 8),

        };
    }


    private static void CreateFountainRoom(IRoom[,] grid, GameDisplay consoleDisplay)
    {
        grid[0, 2] = RoomFactory.CreateRoom("fountain", consoleDisplay);

    }

    // Only for random positioning option
    private static void PositionFountainRandomly(IRoom[,] grid, GameDisplay consoleDisplay)
    {
        int roomWidth = grid.GetLength(0);
        int roomHeight = grid.GetLength(1);

        Random random = new Random();

        int fountainXPosition = random.Next(0, roomWidth - 1);
        int fountainYPosition = random.Next(0, roomHeight - 1);

        grid[fountainXPosition, fountainYPosition] = RoomFactory.CreateRoom("fountain", consoleDisplay);

    }

    private static void SetEntranceRoom(IRoom[,] grid, GameDisplay consoleDisplay)
    {
        grid[0, 0] = RoomFactory.CreateRoom("entrance", consoleDisplay);

    }






}
