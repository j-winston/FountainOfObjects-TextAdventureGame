// Game Engine 
// Responsibilites: Orchestrating the whole thing
using BossBattle.Utilities;

namespace BossBattle.Core;

public class GameEngine : IRoomObserver
{
    private readonly World _world;
    private readonly Player _player;
    private IRoom _currentRoom;
    private WorldSize _worldSize = WorldSize.Small;

    private GameDisplay _display;

    private bool _isRunning;
    private bool _fountainEnabled;


    public void Initialize()
    {
        _isRunning = true;

        Console.Clear();
        Console.WriteLine("Game started.");

    }

    public GameEngine()
    {
        _display = new GameDisplay();
        _player = new Player();
        _isRunning = false;
        _fountainEnabled = false;

        SelectSizeMenu();

        _world = WorldFactory.GenerateWorld(_worldSize, this);
        _currentRoom = _world.GetEntranceRoom();

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

    public void SelectSizeMenu()
    {
        Console.WriteLine("\nChoose a Map Size: ");
        Console.WriteLine("1. Small (4x4)");
        Console.WriteLine("2. Medium (6x6)");
        Console.WriteLine("3. Large (4x4)");

        var menuChoice = Console.ReadLine();

        if (menuChoice is not null)
        {
            _worldSize = GetWorldSize(menuChoice);
        }

        else
        {
            Console.Clear();
            Console.WriteLine("Invalid option.");
        }

    }


    WorldSize GetWorldSize(string menuChoice) => menuChoice switch
    {
        "1" => WorldSize.Small,
        "2" => WorldSize.Medium,
        "3" => WorldSize.Large

    };

    public void Render()
    {
        _currentRoom.PlayerEntered();

    }


    public string GetInput()
    {
        _display.WriteNarrative("What do you want to do?");
        string? input = Console.ReadLine();

        if (input is not null)
        {
            return input;
        }

        return String.Empty;

    }


    public void ProcessInput(string input)
    {
        if (input == "quit")
            return;

        Direction direction = ParseInputForDirection(input);

        var nextPositionCoordinates = GetNextPosition(direction);

        if (nextPositionCoordinates.y >= 0 && nextPositionCoordinates.x >= 0)
        {
            if (_world.DoesRoomExist(nextPositionCoordinates.y, nextPositionCoordinates.x))
            {
                _player?.Move(direction);

            }

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
            Direction.North => (++y, x),
            Direction.South => (--y, x),
            Direction.East => (y, ++x),
            Direction.West => (y, --x),
            _ => throw new ArgumentException("unknown")
        };
    }

    public void UpdateState()
    {
        if (_isRunning)
        {
            var room = _world.GetRoomAt(_player.Y, _player.X);

            if (room is not null)
                _currentRoom = room;
        }
    }

    // Managed by RoomObservers
    public void OnPlayerEnter(IRoom room)
    {
        if (room.Name == "The Fountain Room")
        {
            Console.WriteLine("Type enable fountain to activate fountain");
            string? input = Console.ReadLine();

            if (input is null)
            {
                input = "";
            }

            if (input == "enable fountain")
            {
                Console.WriteLine("You've enabled the fountain!");
                _fountainEnabled = true;
            }
        }

        if (room.Name == "The Cavern Entrance")
        {
            if (_fountainEnabled)
            {
                Console.WriteLine("The Fountain of Objects has been reactivated, and you have escaped with your life!");
                Console.WriteLine("You win!");

                _isRunning = false;
            }
        }
    }

}






