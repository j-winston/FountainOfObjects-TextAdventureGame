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
                // Generate Entrance Room at 0,0
                if (x == 0 && y == 0)
                {
                    grid[x, y] = RoomFactory.CreateRoom("entrance", consoleDisplay);
                }

                // Generate Cavern Rooms 
                else
                {
                    string roomType = (x + y) % 2 == 0 ? "normal" : "hazard";
                    grid[x, y] = RoomFactory.CreateRoom(roomType, consoleDisplay);
                }

                // Pass an observer and text display to each generated room
                grid[x, y].AddObserver(gameEngineObserver);
            }
        }

        // Create the Fountain Room in a random location and pass in observer 
        (int xPos, int yPos) = GetRandomCoordinate(grid.GetLength(0), grid.GetLength(1));

        // If the coordinates are 0,0(entrance) get another set
        if (xPos == 0 && yPos == 0)
            (xPos, yPos) = GetRandomCoordinate(grid.GetLength(0), grid.GetLength(1));

        grid[xPos, yPos] = RoomFactory.CreateRoom("fountain", consoleDisplay);
        grid[xPos, yPos].AddObserver(gameEngineObserver);

        return new World(grid);

    }

    private static (int width, int height) GetDimensions(WorldSize size)
    {
        return size switch
        {
            WorldSize.Small => (4, 4),
            WorldSize.Medium => (6, 6),
            WorldSize.Large => (8, 8),
            _ => (4, 4)

        };
    }


    private static void CreateFountainRoom(IRoom[,] grid, GameDisplay consoleDisplay)
    {
        grid[0, 2] = RoomFactory.CreateRoom("fountain", consoleDisplay);

    }

    private static (int x, int y) GetRandomCoordinate(int width, int height)
    {

        Random random = new Random();

        int randomX = random.Next(0, width - 1);
        int randomY = random.Next(0, height - 1);

        return (randomX, randomY);

    }

    private static void SetEntranceRoom(IRoom[,] grid, GameDisplay consoleDisplay)
    {
        grid[0, 0] = RoomFactory.CreateRoom("entrance", consoleDisplay);

    }






}
