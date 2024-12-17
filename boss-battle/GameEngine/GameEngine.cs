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
    private WorldManager _worldManager;

    private bool _isRunning;
    private bool _fountainEnabled;


    public void Initialize()
    {
        _isRunning = true;
        _fountainEnabled = false;

        Console.Clear();
        Console.WriteLine("Game started.");

    }

    public GameEngine()
    {
        _player = new Player();
        _display = new GameDisplay();
        _world = WorldFactory.GenerateWorld(_worldSize, this);
        _worldManager = new WorldManager(_world);

        SelectSizeMenu();

        _currentRoom = _worldManager.GetEntranceRoom();

    }

    public void Run()
    {
        while (_isRunning)
        {
            Render(_currentRoom);

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
            SetWorldSize(GetWorldSize(menuChoice));
        }

        else
        {
            Console.Clear();
            Console.WriteLine("I don't understand.");
        }

    }


    WorldSize GetWorldSize(string menuChoice)
    {
        switch (menuChoice)
        {
            case "0":
                return WorldSize.Small;

            case "1":
                return WorldSize.Medium;

            case "2":
                return WorldSize.Large;

            default:
                Console.WriteLine("Setting to default map size(4x4)");
                return WorldSize.Small;
        }

    }

    private void SetWorldSize(WorldSize size)
    {
        _worldSize = size;
    }


    public void Render(IRoom room)
    {
        Console.WriteLine();
        DisplayRoomName(room);

        _currentRoom.PlayerEntered();

    }


    public string GetInput()
    {
        _display.WriteNarrative("What do you want to do?");
        _display.ChangeInputColor(MessageTypes.UserCommand);
        string? input = Console.ReadLine();

        if (input is not null)
            return input;

        return String.Empty;

    }

    public void ProcessInput(string input)
    {
        // If user enters q, quit
        if (input == "q")
        {
            _isRunning = false;
            return;
        }

        // If can't make sense of input, do nothing 
        Direction direction = ParseInputForDirection(input);
        if (direction == Direction.Unknown)
        {
            Console.WriteLine("Unknown direction. Staying put.");
        }

        // Otherwise move to target location if it exists
        else
        {
            var targetPosition = GetTargetPosition(direction);
            if (RoomExistsAtCoordinates(targetPosition.x, targetPosition.y))
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
            _ => Direction.Unknown
        };
    }


    public (int y, int x) GetTargetPosition(Direction direction)
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

    private bool RoomExistsAtCoordinates(int x, int y)
    {

        if (ValidCoordinates(x, y))
            if (_worldManager.DoesRoomExist(x, y))
                return true;
        return false;

    }

    private bool ValidCoordinates(int x, int y) => (x >= 0 && y >= 0);

    public void UpdateState()
    {
        if (_isRunning)
        {
            var room = _worldManager.GetRoomAt(_player.Y, _player.X);

            if (room is not null)
                _currentRoom = room;
        }
    }

    // Managed by RoomObservers
    public void OnPlayerEnter(IRoom room)
    {

        switch (room.GetType().Name)
        {
            case "FountainRoom":
                {
                    if (!_fountainEnabled)
                    {
                        _display.WriteFountainMessage("Type enable fountain to activate fountain");
                        _display.ChangeInputColor(MessageTypes.UserCommand);

                        string? input = Console.ReadLine();

                        if (input is null)
                            input = "";

                        if (input == "enable fountain")
                        {
                            EnableFountain((FountainRoom)room);
                            _display.WriteFountainMessage("You've enabled the fountain!");
                            _display.WriteFountainMessage("Now get back to the entrance to escape.");
                        }
                    }
                }
                break;

            case "EntranceRoom":
                {
                    if (_fountainEnabled)
                    {
                        _display.WriteEntranceMessage("The Fountain of Objects has been reactivated, and you have escaped with your life!");
                        _display.WriteEntranceMessage("Type 'q' to quit or wander around more if you like.");
                        _isRunning = false;
                    }
                }
                break;

        }
    }

    private void DisplayRoomName(IRoom room)
    {
        _display.WriteRoomName($"======== {room.Name} ========");
    }
    private void EnableFountain(FountainRoom fountainRoom)
    {
        fountainRoom.FountainEnabled = true;
        _fountainEnabled = true;

    }
}






