using System;

namespace Tic_Tac_Toe
{
    public class GameBoard
    {
        public GameTile[] Board { get; set; }
        public int BoardLength { get => Board.Length; }
        public bool IsTileBlank (int tile)
        {
            if (Board[tile - 1].IsTokenBlank) return true;
            else return false;
        }
        private int BoardSquare { get => (int)Math.Sqrt(Board.Length); }


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
            Console.WriteLine();
            for (int i = 1; i <= Board.Length; i++)
            {
                if (i % BoardSquare == 0)
                {
                    Console.Write($"{Board[i - 1].TileDisplay(i)}");
                    Console.WriteLine();
                    for (int j = 1; j <= BoardSquare && i < Board.Length - BoardSquare / 2; j++)
                    {
                        if (j % BoardSquare != 0) Console.Write("---|");
                        else Console.Write("---");
                    }
                    Console.WriteLine();
                }
                else Console.Write($"{Board[i - 1].TileDisplay(i)}|");
            }
        }

        public bool CheckForTie()
        {
            foreach (GameTile tile in Board)
            {
                if (tile.IsTokenBlank) return false;
            }
            return true;
        }

        public void PlacePlayerToken(Player player, int chosenTile)
        {
            if (player.PlayerToken == 'X') Board[chosenTile - 1].SetTokenX();
            else if (player.PlayerToken == 'O') Board[chosenTile - 1].SetTokenO();
        }

        #region Win Conditions
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

        public bool CheckdiagonalsForWin(Player currentPlayer)
        {
            int highValue = BoardSquare + 1;
            int lowValue = BoardSquare - 1;
            int checkDiagonal = 0;
            int consecutive = 0;

            for (int i = 0; i < BoardSquare; i++) //left diagonal
            {
                if (Board[checkDiagonal].GetToken() == currentPlayer.PlayerToken) consecutive++;
                else break;
                checkDiagonal += highValue;
            }
            if (consecutive == BoardSquare) return true;

            consecutive = 0;
            checkDiagonal = lowValue;
            for (int i = 0; i < BoardSquare; i++) //right diagonal
            {
                if (Board[checkDiagonal].GetToken() == currentPlayer.PlayerToken) consecutive++;
                else break;
                checkDiagonal += lowValue;
            }
            if (consecutive == BoardSquare) return true;

            return false;
        }
        #endregion
    }
}