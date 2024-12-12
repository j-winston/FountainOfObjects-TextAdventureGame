// Room
namespace BossBattle.Core;

public class NormalRoom : IRoom
{

    public string Description => "Nothing to see here.";
    public string RoomName => "Normal Room";
}

public class HazardRoom : IRoom
{
    public string Description => "This room gives me a the creeps.";
    public string RoomName => "Hazard Room";
}

public class FountainRoom : IRoom
{
    public string Description => "The Fountain is in this room!";
    public string RoomName => "Fountain Room";
}

public class EntranceRoom : IRoom
{
    public string Description => "You see light coming from outside the cavern. This is the entrance.";
    public string RoomName => "Cavern Entrance";
}


