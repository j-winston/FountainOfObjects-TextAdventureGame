// Room Factory 
namespace BossBattle.Core;


public class RoomFactory
{
    public static IRoom CreateRoom(string roomType, GameDisplay roomTextDisplay)
    {
        return roomType switch
        {
            "normal" => new NormalRoom("A Normal Room", roomTextDisplay),
            "hazard" => new HazardRoom("Hazardous Room", roomTextDisplay),
            "fountain" => new FountainRoom("The Fountain Room", roomTextDisplay),
            "entrance" => new EntranceRoom("The Cavern Entrance", roomTextDisplay),
            _ => throw new ArgumentException("Unknown room type")
        };

    }
}
