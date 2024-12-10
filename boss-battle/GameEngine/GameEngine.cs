// Game Engine 
// Responsibilites: Orchestrating the whole thing
using BossBattle.Utilities;

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

            var input = GetInput();

            ProcessInput(input);

            UpdateState();


        }


    }

    // Get current room and display on screen 
    public void Render()
    {
        if (_world is not null && _player is not null)
        {
            IRoom? currentRoom = GetCurrentRoom();

            if (currentRoom is not null)
            {

                //TODO: prettify the title of the room 
                Console.WriteLine($"~~~{currentRoom?.RoomName}~~~\n");
                Console.WriteLine(currentRoom?.Description);

            }
            else
            {
                Console.WriteLine("You've hit a wall");
            }


        }

    }

    public IRoom? GetCurrentRoom()
    {
        if (_world is not null && _player is not null)
        {
            return _world.GetRoomAt(_player.X, _player.Y);
        }

        return null;
    }

    public string GetInput()
    {
        Console.WriteLine("What do you want to do?");
        string? input = Console.ReadLine();

        if (input is not null)
        {
            return input;
        }

        return String.Empty;

    }

    public bool IsValidCommand(string input)
    {
        return input.Split(" ").GetLength(0) > 1;
    }

    // This can later be modified Maniac Mansion style 
    public void ProcessInput(string input)
    {
        if (IsValidCommand(input))
        {
            Direction direction = ParseInputForDirection(input);

            if (RoomDoesExist(direction))
            {
                _player?.Move(direction);
            }

        }
    }

    public bool RoomDoesExist(Direction direction)
    {
        return _world.DoesRoomExist(direction);
    }


    public Direction ParseInputForDirection(string input)
    {
        return input.ToLowerInvariant() switch
        {
            "move north" or "north" or "n" => Direction.North,
            "move south" or "south" or "s" => Direction.South,
            "move east" or "east" or "e" => Direction.East,
            "move west" or "west" or "w" => Direction.West,
            _ => throw new ArgumentException("unknown")
        };
    }




    public void UpdateState()
    {

    }

}






