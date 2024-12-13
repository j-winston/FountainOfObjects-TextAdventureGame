using BossBattle.Utilities;

namespace BossBattle.Core;

public interface IRoom
{
    string? Name { get; }

    void AddObserver(IRoomObserver observer);
    void PlayerEntered();


}


