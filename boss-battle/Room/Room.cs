using System.Collections.Generic;
using BossBattle.Utilities;

namespace BossBattle.Core;


public class NormalRoom : IRoom
{
    private readonly RoomObservers _roomObservers = new();
    private readonly GameDisplay _roomDisplay;

    public string Name { get; }

    public NormalRoom(string name, GameDisplay display)
    {
        Name = name;
        _roomDisplay = display;


    }

    public void AddObserver(IRoomObserver observer)
    {
        _roomObservers.AddObserver(observer);
    }

    public void PlayerEntered()
    {
        _roomDisplay.WriteDescription("A normal room. Nothing to see here.");
        _roomObservers.NotifyObservers(this);
    }


}

public class HazardRoom : IRoom
{
    private readonly RoomObservers _roomObservers = new();
    private readonly GameDisplay _roomDisplay = new();
    public string Name { get; }

    public HazardRoom(string name, GameDisplay display)
    {
        Name = name;
        _roomDisplay = display;
    }

    public void AddObserver(IRoomObserver observer)
    {
        _roomObservers.AddObserver(observer);
    }

    public void PlayerEntered()
    {
        _roomDisplay.WriteDescription("This room feels threatening. I wouldn't stay here long.");
        _roomObservers.NotifyObservers(this);

    }

}

public class FountainRoom : IRoom
{
    private readonly RoomObservers _roomObservers = new();
    private readonly GameDisplay _roomDisplay;

    public string Name { get; }

    public FountainRoom(string name, GameDisplay display)
    {
        Name = name;
        _roomDisplay = display;
    }

    public void AddObserver(IRoomObserver observer)
    {
        _roomObservers.AddObserver(observer);
    }

    public void PlayerEntered()
    {
        _roomDisplay.WriteFountainMessage("You hear water dripping in this room. The Fountain of Objects is here!");
        _roomObservers.NotifyObservers(this);
    }


}

public class EntranceRoom : IRoom
{
    private readonly RoomObservers _roomObservers = new();
    private readonly GameDisplay _roomDisplay;

    public string Name { get; }

    public EntranceRoom(string name, GameDisplay display)
    {
        Name = name;
        _roomDisplay = display;
    }

    public void AddObserver(IRoomObserver observer)
    {
        _roomObservers.AddObserver(observer);
    }

    public void PlayerEntered()
    {
        _roomDisplay.WriteEntranceMessage("You see light coming from outside the cavern. This is the entrance.");
        _roomObservers.NotifyObservers(this);

    }
}


