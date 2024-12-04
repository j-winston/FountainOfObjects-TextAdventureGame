namespace Player;

public class Player
{
    public int X { get; set; }
    public int Y { get; set; }

    public void MoveNorth()
    {
        Y++;
    }
    public void MoveSouth()
    {
        Y--;
    }
    public void MoveWest()
    {
        X--;
    }
    public void MoveEast()
    {
        X++;
    }


}

