// Room
namespace BossBattle.Core;

public class NormalRoom : IRoom
{

    public string Description => "Nothing to see here.";

}

public class HazardRoom : IRoom
{
    public string Description => "This room gives me a the creeps. Something bad is in here or happened in hear.";
}

public class FountainRoom : IRoom
{
    public string Description => "The Fountain is in this room!";
}


