// World Factory 
namespace BossBattle.Core;

using BossBattle.Utilities;

public class WorldFactory
{

    public static World GenerateWorld(int width, int height, IRoomObserver gameEngineObserver)
    {
        var grid = new IRoom[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                string roomType = (x + y) % 2 == 0 ? "normal" : "hazard";

                grid[x, y] = RoomFactory.CreateRoom(roomType);

                grid[x, y].AddObserver(gameEngineObserver);
            }
        }

        CreateFountainRoom(grid);

        SetEntranceRoom(grid);

        return new World(grid);

    }

    private static void CreateFountainRoom(IRoom[,] grid)
    {
        grid[0, 2] = RoomFactory.CreateRoom("fountain");

    }

    private static void PositionFountainRandomly(IRoom[,] grid)
    {
        int roomWidth = grid.GetLength(0);
        int roomHeight = grid.GetLength(1);

        Random random = new Random();

        int fountainXPosition = random.Next(0, roomWidth - 1);
        int fountainYPosition = random.Next(0, roomHeight - 1);

        grid[fountainXPosition, fountainYPosition] = RoomFactory.CreateRoom("fountain");

    }

    private static void SetEntranceRoom(IRoom[,] grid)
    {
        grid[0, 0] = RoomFactory.CreateRoom("entrance");

    }






}
