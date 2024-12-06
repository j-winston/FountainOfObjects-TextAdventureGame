// World Factory 
namespace BossBattle.Core;

public class WorldFactory
{

    public static World GenerateWorld(int width, int height)
    {
        var grid = new IRoom[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                string roomType = (x + y) % 2 == 0 ? "normal" : "hazard";

                grid[x, y] = RoomFactory.CreateRoom(roomType);
            }
        }

        CreateFountainRoom(grid);

        return new World(grid);

    }

    private static void CreateFountainRoom(IRoom[,] grid)
    {
        int roomWidth = grid.GetLength(0);
        int roomHeight = grid.GetLength(1);

        Random random = new Random();

        int fountainXPosition = random.Next(0, roomWidth - 1);
        int fountainYPosition = random.Next(0, roomHeight - 1);

        grid[fountainXPosition, fountainYPosition] = RoomFactory.CreateRoom("fountain");

    }


}
