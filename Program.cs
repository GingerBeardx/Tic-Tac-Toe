using System;

Console.Title = "Tic-Tac-Toe";

public class GameBoard
{
    public GameTile[] Board { get; set; }

    public GameBoard()
    {
        Board = new GameTile[9]
        {
            new GameTile(0, 0), // Position 1
            new GameTile(0, 1), // Position 2
            new GameTile(0, 2), // Position 3
            new GameTile(1, 0), // Position 4
            new GameTile(1, 1), // Position 5
            new GameTile(1, 2), // Position 6
            new GameTile(2, 0), // Position 7
            new GameTile(2, 1), // Position 8
            new GameTile(2, 2) // Position 9
        };
    }
}

public class GameTile
{
    public int LocationX { get; set; }
    public int LocationY { get; set; }
    public bool IsTokenX { get; set; }
    public bool IsTokenO { get; set; }
    public bool IsTokenBlank { get; set; }

    public GameTile(int locationX, int locationY)
    {
        LocationX = locationX;
        LocationY = locationY;
        IsTokenX = false;
        IsTokenO = false;
        IsTokenBlank = true;
    }

    public void SetTokenX()
    {
        IsTokenBlank = false;
        IsTokenX = true;
    }

    public void SetTokenO()
    {
        IsTokenBlank = false;
        IsTokenO = true;
    }

    public void ResetTile()
    {
        IsTokenX = false;
        IsTokenO = false;
        IsTokenBlank = true;
    }
}
