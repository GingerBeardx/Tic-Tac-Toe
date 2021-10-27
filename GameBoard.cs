using System;

public class GameBoard
{
    public GameTile[] Board { get; set; }

    public GameBoard(int tiles)
    {
        Board = new GameTile[tiles];
        for (int i = 0; i < tiles; i++)
        {
            Board[i] = new GameTile();
        }
    }

    public void DrawGameBoard()
    {
        double boardSquareRoot = Math.Sqrt(Board.Length);
        Console.WriteLine();
        for (int i = 1; i <= Board.Length; i++)
        {
            if (i % boardSquareRoot == 0) 
            {
                Console.Write($"{Board[i - 1].TileDisplay(i)}");
                Console.WriteLine();
                for (int j = 1; j <= boardSquareRoot && i < Board.Length - (boardSquareRoot / 2); j++)
                {
                    if (j % boardSquareRoot != 0) Console.Write("---|");
                    else Console.Write("---");
                }
                Console.WriteLine();
            }
            else Console.Write($"{Board[i - 1].TileDisplay(i)}|");
        }
    }
}
