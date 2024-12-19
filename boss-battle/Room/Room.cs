using System.Collections.Generic;
using BossBattle.Utilities;

namespace BossBattle.Core;


public class NormalRoom : IRoom
{
    private readonly RoomObservers _roomObservers = new();

    public string Name { get; }
    public bool HasPit { get; set; } = false;

    public NormalRoom(string name)
    {
        Name = name;


    }

    public void AddObserver(IRoomObserver observer)
    {
        _roomObservers.AddObserver(observer);
    }

    public void PlayerEntered()
    {
        _roomObservers.NotifyObservers(this);
    }


}

public class HazardRoom : IRoom
{
    private readonly RoomObservers _roomObservers = new();
    public string Name { get; }
    public bool HasPit { get; set; } = false;

    public HazardRoom(string name)
    {
        Name = name;
    }

    public void AddObserver(IRoomObserver observer)
    {
        _roomObservers.AddObserver(observer);
    }

    public void PlayerEntered()
    {
        _roomObservers.NotifyObservers(this);

    }

}

public class FountainRoom : IRoom
{
    private readonly RoomObservers _roomObservers = new();

    public string Name { get; }
    public bool FountainEnabled { get; set; }
    public bool HasPit { get; set; } = false;

    public FountainRoom(string name)
    {
        Name = name;
    }

    public void AddObserver(IRoomObserver observer)
    {
        _roomObservers.AddObserver(observer);
    }

    public void PlayerEntered()
    {

        _roomObservers.NotifyObservers(this);
    }


}

public class EntranceRoom : IRoom
{
    private readonly RoomObservers _roomObservers = new();
    public bool HasPit { get; set; }

    public string Name { get; }

    public EntranceRoom(string name)
    {
        Name = name;
    }

    public void AddObserver(IRoomObserver observer)
    {
        _roomObservers.AddObserver(observer);
    }

    public void PlayerEntered()
    {
        _roomObservers.NotifyObservers(this);

    }
}


