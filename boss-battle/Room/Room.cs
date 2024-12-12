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
    // Later, each room could have sets of objects with interaction triggers
    public string Description => "You hear water dripping in this room. The Fountain of Objects is here!";
    public string FountainEnabledMessage => "You hear rushing waters from the Fountain of Objects. It has been reactivated!";
    public string RoomName => "Fountain Room";
}

public class EntranceRoom : IRoom
{
    public string Description => "You see light coming from outside the cavern. This is the entrance.";
    public string RoomName => "Cavern Entrance";
}


