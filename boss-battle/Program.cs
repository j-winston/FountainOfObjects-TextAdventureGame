namespace BossBattle;
using BossBattle.Core;
public class Program
{
    public static void Main(string[] args)
    {
        GameEngine gameEngine = new();

        gameEngine.Initialize();

        gameEngine.Run();
    }
}
