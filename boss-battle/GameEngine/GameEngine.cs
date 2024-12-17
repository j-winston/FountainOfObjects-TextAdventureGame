// Game Engine 
// Responsibilites: Orchestrating the whole thing
using BossBattle.Utilities;

namespace BossBattle.Core;

public class GameEngine : IRoomObserver
{
    private readonly World _world;
    private readonly Player _player;
    private IRoom _currentRoom;
    private WorldSize _worldSize;

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

        _worldSize = GetMapSizeChoice();

        _world = WorldFactory.GenerateWorld(_worldSize, this);
        _worldManager = new WorldManager(_world);

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

    public WorldSize GetMapSizeChoice()
    {
        Console.WriteLine("\nChoose a Map Size: ");
        Console.WriteLine("1. Small (4x4)");
        Console.WriteLine("2. Medium (6x6)");
        Console.WriteLine("3. Large (8x8)");

        var input = Console.ReadLine();

        if (int.TryParse(input, out int menuItem))
        {
            return menuItem switch
            {
                1 => WorldSize.Small,
                2 => WorldSize.Medium,
                3 => WorldSize.Large,
                _ => WorldSize.Small
            };
        }

        Console.WriteLine("I don't understand. Setting to default.");
        return WorldSize.Small;
    }

    private void SetWorldSize(int menuChoice)
    {
        switch (menuChoice)
        {
            case 1:
                _worldSize = WorldSize.Small;
                break;

            case 2:
                _worldSize = WorldSize.Medium;
                break;

            case 3:
                _worldSize = WorldSize.Large;
                break;

            default:
                Console.WriteLine("Setting to default map size(4x4)");
                _worldSize = WorldSize.Small;
                break;
        }
    }


    public void Render(IRoom room)
    {
        Console.WriteLine();

        DisplayRoomName(room);

        // Notify all observers of PlayerEntered event
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
            else
                Console.WriteLine("I can't move there.");
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


    public (int x, int y) GetTargetPosition(Direction direction)
    {
        // Get current position 
        int x = _player.X;
        int y = _player.Y;

        // Return coordinates of target position
        return direction switch
        {
            Direction.North => (x, ++y),
            Direction.South => (x, --y),
            Direction.East => (++x, y),
            Direction.West => (--x, y),
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
            var room = _worldManager.GetRoomAt(_player.X, _player.Y);

            if (room is not null)
            {
                _currentRoom = room;
                Console.WriteLine($"Current room is {_player.X}, {_player.Y}");
            }
        }
    }

    // Managed by RoomObservers
    public void OnPlayerEnter(IRoom room)
    {
        switch (room)
        {
            case FountainRoom fountainRoom:
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
                            EnableFountain(fountainRoom);
                            _display.WriteFountainMessage("You've enabled the fountain!");
                            _display.WriteFountainMessage("Now get back to the entrance to escape.");
                        }
                    }
                }
                break;

            case EntranceRoom entranceRoom:
                {
                    if (_fountainEnabled)
                    {
                        _display.WriteEntranceMessage("The Fountain of Objects has been reactivated, and you have escaped with your life!");
                        _display.WriteEntranceMessage("Type 'q' to quit or wander around more if you like.");
                        _isRunning = false;
                    }
                }
                break;

            case HazardRoom hazardRoom:
                {
                    if (_worldManager.IsAdjacentToPit(_player.X, _player.Y))
                        _display.WriteDescription("You feel a draft. There's a pit in a nearby room..");
                    if (_worldManager.DoesRoomContainPit(_player.X, _player.Y))
                        _display.WriteNarrative("You died in the pit. I am sorry.");
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






