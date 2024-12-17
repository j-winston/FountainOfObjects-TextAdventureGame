using BossBattle.Utilities;

namespace BossBattle.Core;

public interface IRoom
{
    string? Name { get; }
    bool HasPit { get; set; }

    void AddObserver(IRoomObserver observer);
    void PlayerEntered();


}


