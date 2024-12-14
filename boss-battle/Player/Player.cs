using BossBattle.Utilities;

namespace BossBattle.Core;

public class Player
{
    public int X { get; set; }
    public int Y { get; set; }


    public void Move(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                Y++;
                break;
            case Direction.East:
                X++;
                break;
            case Direction.South:
                if (Y >= 0)
                    Y--;
                break;
            case Direction.West:
                if (X >= 0)
                    X--;
                break;
        }



    }


}

