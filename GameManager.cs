using System;

namespace Tic_Tac_Toe
{
    public class GameManager
    {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public Player CurrentPlayer { get; set; }
        public GameBoard Board { get; set; }
        public bool IsGameRunning { get; set; }

        public void CreatePlayers()
        {
            Console.Write("Please enter a name for player one: ");
            string playerOneName = Console.ReadLine();
            Console.Write("Please enter a name for player two: ");
            string playerTwoName = Console.ReadLine();
            //Todo: Add name validation

            PlayerOne = new Player(playerOneName, 'X');
            PlayerTwo = new Player(playerTwoName, 'O');
        }

        public void CheckForWinner()
        {
            bool foundWinner = Board.CheckRowsForWin(CurrentPlayer);
            if (!foundWinner) foundWinner = Board.CheckColumnsForWin(CurrentPlayer);
            if (!foundWinner) foundWinner = Board.CheckdiagonalsForWin(CurrentPlayer);

            if (foundWinner)
            {
                Console.WriteLine($"{CurrentPlayer.FirstName} wins!!!");
                CurrentPlayer.PlayerWon();
                IsGameRunning = false;
            }
        }

        public void CreateBoard()
        {
            bool isValid = false;
            while (!isValid)
            {
                Console.Write("Please enter the number of tiles for the game board (should be a perfect square): ");
                bool response = int.TryParse(Console.ReadLine(), out int boardTiles);
                if (Math.Sqrt(boardTiles) % 1 == 0 && response && boardTiles > 0)
                {
                    Board = new GameBoard(boardTiles);
                    isValid = true;
                }
                else if (!response) Console.WriteLine("That entry was not a recognizable number.");
                else Console.WriteLine("That response is not a perfect square.");
                Console.WriteLine();
                IsGameRunning = true;
            }
        }

        public void GetTilePosition()
        {
            bool accptedResponse = false;
            while (!accptedResponse)
            {
                Console.Write("Please choose a tile to place your token: ");
                bool isValidNumber = int.TryParse(Console.ReadLine(), out int chosenTile);
                if (isValidNumber && chosenTile > 0 && chosenTile <= Board.Board.Length)
                {
                    if (Board.Board[chosenTile - 1].IsTokenBlank)
                    {
                        accptedResponse = true;
                        if (CurrentPlayer.PlayerToken == 'X') Board.Board[chosenTile - 1].SetTokenX();
                        else if (CurrentPlayer.PlayerToken == 'O') Board.Board[chosenTile - 1].SetTokenO();
                    }
                    else Console.WriteLine("That position is taken.");
                }
                else Console.WriteLine("That position is not valid.");
            }
        }

        public void DisplayActivePlayer()
        {
            Console.WriteLine($"{CurrentPlayer.FirstName}, it is your turn.");
        }

        public void DrawBoard()
        {
            Console.Clear();
            Board.DrawGameBoard();
        }

        public void SetActivePlayer()
        {
            if (CurrentPlayer == null) CurrentPlayer = PlayerOne;
            else if (CurrentPlayer == PlayerOne) CurrentPlayer = PlayerTwo;
            else CurrentPlayer = PlayerOne;
        }

        public void PlayAgainPrompt()
        {
            Console.WriteLine();
            Console.Write("Would you like to play again (Y/N)? ");
            string response = Console.ReadLine();
            if (response.ToLower() == "y")
            {
                IsGameRunning = true;
                foreach (GameTile tile in Board.Board)
                {
                    tile.ResetTile();
                }
            }
            else IsGameRunning = false;
        }
    }
}