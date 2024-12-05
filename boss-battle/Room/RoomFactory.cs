// Room Factory 
namespace Room;


public class RoomFactory
{
    public static IRoom CreateRoom(string roomType)
    {
        return roomType switch
        {
            "normal" => new NormalRoom(),
            "hazard" => new HazardRoom(),
            "fountain" => new FountainRoom(),
            _ => throw new ArgumentException("Unknown room type")
        };

    }
}
