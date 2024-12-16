using System.Collections.Generic;


namespace BossBattle.Core;

public class GameDisplay
{
    private readonly Dictionary<MessageTypes, ConsoleColor> _messageColors;

    public GameDisplay()
    {
        _messageColors = new Dictionary<MessageTypes, ConsoleColor>
        {
         {MessageTypes.Narrative, ConsoleColor.Magenta },
         {MessageTypes.Descriptive , ConsoleColor.White},
         {MessageTypes.Input , ConsoleColor.Cyan},
         {MessageTypes.Entrance ,ConsoleColor.Yellow},
         {MessageTypes.Fountain , ConsoleColor.Blue},
         {MessageTypes.RoomName , ConsoleColor.Red},
         {MessageTypes.UserCommand, ConsoleColor.Green}

        };
    }

    public void WriteNarrative(string message)
    {
        WriteMessage(message, MessageTypes.Narrative);
    }

    public void WriteUserCommand(string message)
    {
        WriteMessage(message, MessageTypes.UserCommand);
    }

    public void WriteRoomName(string message)
    {
        WriteMessage(message, MessageTypes.RoomName);
    }

    public void WriteDescription(string message)
    {
        WriteMessage(message, MessageTypes.Descriptive);
    }

    public void WriteFountainMessage(string message)
    {
        WriteMessage(message, MessageTypes.Fountain);
    }

    public void WriteEntranceMessage(string message)
    {
        WriteMessage(message, MessageTypes.Entrance);
    }

    public void ChangeInputColor(MessageTypes messageType)
    {
        if (_messageColors.TryGetValue(messageType, out var color))
        {
            Console.ForegroundColor = color;
        }
    }


    private void WriteMessage(string message, MessageTypes messageType)
    {
        if (_messageColors.TryGetValue(messageType, out var color))
        {
            Console.ForegroundColor = color;
        }

        Console.WriteLine(message);
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
    UserCommand
};

