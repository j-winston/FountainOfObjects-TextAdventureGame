// Game Engine 
// Responsibilites: Orchestrating the whole thing
//
namespace BossBattle.Core;

public class GameEngine
{
    private World? _world;
    private Player? _player;
    private bool _isRunning;


    public void Initialize()
    {
        _world = WorldFactory.GenerateWorld(5, 5);
        _player = new Player();
        _isRunning = true;

    }

    public void Run()
    {
        while (_isRunning)
        {
            Render();

            GetInput();

            ProcessInput();

            UpdateState();


        }


    }

    public void Render()
    {

        IRoom? currentRoom = _world?.GetRoomAt(_player.X, _player.Y);

        if (currentRoom is not null)
        {
            Console.WriteLine(currentRoom?.Description);
        }

    }


    public void GetInput()
    {

    }

    public void ProcessInput()
    {

    }

    public void UpdateState()
    {

    }




}


