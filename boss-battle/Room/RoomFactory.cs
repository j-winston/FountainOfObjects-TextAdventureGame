// Room Factory 
namespace BossBattle.Core;


public class RoomFactory
{
    public static IRoom CreateRoom(string roomType)
    {
        return roomType switch
        {
            "normal" => new NormalRoom("A Normal Room"),
            "hazard" => new HazardRoom("Hazardous Room"),
            "fountain" => new FountainRoom("The Fountain Room"),
            "entrance" => new EntranceRoom("The Cavern Entrance"),
            _ => throw new ArgumentException("Unknown room type")
        };

    }
}
