using System.Collections.Generic;
using System;


namespace BossBattle.Core;

public class GameDisplay
{
    private readonly WorldManager _worldManager;
    private readonly Dictionary<MessageTypes, ConsoleColor> _messageColors;

    private readonly World _world;
    private readonly (int width, int height) _mapDimensions;

    public GameDisplay(WorldManager worldManager)
    {
        _worldManager = worldManager;

        _world = _worldManager.GetWorld();

        _mapDimensions.width = _world.Grid.GetLength(0);
        _mapDimensions.height = _world.Grid.GetLength(1);

        _messageColors = new Dictionary<MessageTypes, ConsoleColor>
        {
         {MessageTypes.Narrative, ConsoleColor.Magenta },
         {MessageTypes.Descriptive , ConsoleColor.White},
         {MessageTypes.Input , ConsoleColor.Cyan},
         {MessageTypes.Entrance ,ConsoleColor.Yellow},
         {MessageTypes.Fountain , ConsoleColor.Blue},
         {MessageTypes.RoomName , ConsoleColor.Red},
         {MessageTypes.UserCommand, ConsoleColor.Magenta},
         {MessageTypes.MapDisplay, ConsoleColor.Blue},


        };

    }

    public void WriteNarrative(string message) => WriteMessage(message, MessageTypes.Narrative);

    public void WriteUserCommand(string message) => WriteMessage(message, MessageTypes.UserCommand);

    public void WriteRoomName(string message) => WriteMessage(message, MessageTypes.RoomName);

    public void WriteDescription(string message) => WriteMessage(message, MessageTypes.Descriptive);

    public void WriteFountainMessage(string message) => WriteMessage(message, MessageTypes.Fountain);

    public void WriteEntranceMessage(string message) => WriteMessage(message, MessageTypes.Entrance);

    public void WriteToMap(string message) => WriteMapCharacters(message, MessageTypes.MapDisplay);

    public void ChangeTextColor(MessageTypes messageType)
    {
        if (_messageColors.TryGetValue(messageType, out var color))
        {
            Console.ForegroundColor = color;
        }
    }


    private void WriteMapCharacters(string characters, MessageTypes messageType)
    {
        ChangeTextColor(messageType);
        Console.Write(characters);
    }

    private void WriteMessage(string message, MessageTypes messageType)
    {
        ChangeTextColor(messageType);
        Console.WriteLine(message);
    }

    public void ShowHazards(bool show) { }
    public void ShowFountain(bool show) { }
    public void ShowPlayerPosition(bool show) { }

    public void DrawMapToScreen(int playerPosX, int playerPosY)
    {
        for (int x = 0; x < _mapDimensions.width; x++)
        {
            for (int y = 0; y < _mapDimensions.height; y++)
            {
                (int gridPosX, int gridPosY) = TranslateCoordinates(x, y);

                (int translatedPlayerX, int translatedPlayerY) = TranslateCoordinates(playerPosX, playerPosY);

                var currentRoom = _worldManager.GetRoomAt(gridPosX, gridPosY);

                WriteToMap("[");

                if (playerPosY == gridPosY && playerPosX == gridPosX)
                {
                    MarkPlayerPosition();
                    WriteToMap("]");
                }

                else if (_worldManager.DoesRoomContainPit(currentRoom))
                {
                    MarkPits(currentRoom);
                    WriteToMap("]");
                }

                else if (_worldManager.ContainsFountain(currentRoom))
                {
                    MarkFountain(currentRoom);
                    WriteToMap("]");
                }


                else
                {
                    WriteToMap(" ]");
                }
            }

            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private void MarkFountain(IRoom currentRoom)
    {
        if (_worldManager.ContainsFountain(currentRoom))
            WriteToMap("^");
    }

    private void MarkPits(IRoom currentRoom)
    {
        if (_worldManager.DoesRoomContainPit(currentRoom))
            WriteToMap("x");
    }

    private void DrawEmptyGridSquare()
    {
        WriteToMap("[ ]");
    }

    private void MarkPlayerPosition()
    {
        WriteToMap("*");
    }

    private (int x, int y) TranslateCoordinates(int x, int y)
    {
        // Translate array based coordinates to cartisian axis from (0,0) -> (7,7)
        // so that (0,0) refers to bottom left corner of map.
        var newX = y;
        var newY = Math.Abs(x - (_mapDimensions.height - 1));

        return (newX, newY);

    }
}

public enum MessageTypes
{
    Narrative,
    Input,
    Error,
    Fountain,
    Descriptive,
    RoomName,
    MenuNumberOption,
    Entrance,
    UserCommand,
    MapDisplay
};

