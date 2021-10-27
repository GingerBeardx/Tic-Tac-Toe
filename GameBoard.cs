using System;

public class GameBoard
{
    public GameTile[] Board { get; set; }
    private int BoardSquare { get => (int)Math.Sqrt(Board.Length);  }

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

    public bool CheckRowsForWin(Player currentPlayer)
    {
        bool foundWinner = false;
        int startingPosition = 0;
        int consecutive = 0;

        for (int i = 0; i < Board.Length; i++)
        {
            if (foundWinner) break;
            for (int j = startingPosition; j < startingPosition + BoardSquare; j++)
            {
                if (j >= Board.Length) break;
                if (Board[j].GetToken() == currentPlayer.PlayerToken) consecutive++;
                else consecutive = 0;
            }
            if (consecutive == BoardSquare) return true;
            else startingPosition += BoardSquare;
        }
        return false;
    }

    public bool CheckColumnsForWin(Player currentPlayer)
    {
        int consecutive = 0;
        for (int i = 0; i < BoardSquare; i++)
        {
            int checkSpot = 0 + i;
            for (int j = 0; j < BoardSquare; j++)
            {
                if (Board[checkSpot].GetToken() == currentPlayer.PlayerToken) consecutive++;
                else consecutive = 0;
                checkSpot += BoardSquare;
            }
            if (consecutive == BoardSquare) return true;
        }
        return false;
    }
}
