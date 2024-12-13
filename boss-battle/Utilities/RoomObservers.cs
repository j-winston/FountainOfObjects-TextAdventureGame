// Handles obeserver management
using BossBattle.Core;
using System.Collections.Generic;

namespace BossBattle.Utilities;

public class RoomObservers
{
    private readonly List<IRoomObserver> _observers = new();

    public void AddObserver(IRoomObserver observer)
    {
        _observers.Add(observer);

    }

    public void NotifyObservers(IRoom room)
    {
        foreach (var observer in _observers)
        {
            observer.OnPlayerEnter(room);
        }
    }

}
