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

    public void Render()
    {
        var currentRoom = _world.GetRoomAt(_player.X, _player.Y);

        Console.WriteLine($"\nYou are in the room at y={_player.Y}, x={_player.X}");
        Console.WriteLine($"~~~{currentRoom?.RoomName}~~~\n");
        Console.WriteLine(currentRoom?.Description);


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


    public void ProcessInput(string input)
    {
        Direction direction = ParseInputForDirection(input);

        var nextPositionCoordinates = GetNextPosition(direction);

        if (nextPositionCoordinates.y >= 0 && nextPositionCoordinates.x >= 0)

            if (_world.DoesRoomExist(nextPositionCoordinates.y, nextPositionCoordinates.x))
            {
                _player?.Move(direction);
            }
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


    public (int y, int x) GetNextPosition(Direction direction)
    {
        // Get current position 
        int y = _player.Y;
        int x = _player.X;

        // Return coordinates of target position
        return direction switch
        {
            Direction.North => (y++, x),
            Direction.South => (y--, x),
            Direction.East => (y, x++),
            Direction.West => (y, x--),
            _ => throw new ArgumentException("unknown")
        };
    }

    public void UpdateState()
    {

    }

}






