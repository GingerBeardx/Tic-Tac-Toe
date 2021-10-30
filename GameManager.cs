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
        public bool PlaySamePlayers { get; set; }
        public bool IsGameOver { get; set; }

        public void CreatePlayers()
        {
            Console.Write("Please enter a name for player one: ");
            string playerOneName = Console.ReadLine();
            Console.Write("Please enter a name for player two: ");
            string playerTwoName = Console.ReadLine();

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
                if (isValidNumber && chosenTile > 0 && chosenTile <= Board.BoardLength)
                {
                    if (Board.IsTileBlank(chosenTile))
                    {
                        accptedResponse = true;
                        Board.PlacePlayerToken(CurrentPlayer, chosenTile);
                    }
                    else Console.WriteLine("That position is taken.");
                }
                else Console.WriteLine("That position is not valid.");
            }
        }

        public void DisplayActivePlayer()
        {
            Console.WriteLine($"{CurrentPlayer.FirstName}, it is your turn to place your -{CurrentPlayer.PlayerToken}-.");
        }

        public void DrawBoard()
        {
            Console.Clear();
            DisplayTitle();
            Board.DrawGameBoard();
            DisplayScoreboard();
        }

        public void SetActivePlayer()
        {
            if (CurrentPlayer == null) CurrentPlayer = PlayerOne;
            else if (CurrentPlayer == PlayerOne) CurrentPlayer = PlayerTwo;
            else CurrentPlayer = PlayerOne;
        }

        public void PlaySameBoardAgainPrompt()
        {
            Console.WriteLine();
            Console.Write("Would you like to play again on the same size board (Y/N)? ");
            IsGameRunning = GetYesNoResponse(Console.ReadLine());
        }

        public void PlaySamePlayersAgainPrompt()
        {
            Console.WriteLine();
            Console.Write("Would you like to play with the same players (Y/N)? ");
            PlaySamePlayers = GetYesNoResponse(Console.ReadLine());
        }

        public void ContinuePlaying()
        {
            Console.WriteLine();
            Console.Write("Do you want to quit the game and see the winner (Y/N)?");
            IsGameOver = GetYesNoResponse(Console.ReadLine());
        }

        private bool GetYesNoResponse(string response)
        {
            if (response.ToLower() == "y")
            {
                foreach (GameTile tile in Board.Board)
                {
                    tile.ResetTile();
                }
                return true;
            }
            else return false;
        }

        public void CheckForTie()
        {
            bool isTie = Board.CheckForTie();
            if(isTie)
            {
                IsGameRunning = false;
                Console.WriteLine("Nicely played! There is no victor for this round!");
            }
        }

        public static void DisplayGreeting()
        {
            DisplayTitle();
            Console.WriteLine("Welcome to Tic-Tac-Tum!");
            Console.WriteLine("Where you can play classic tic-tac-toe on whatever size grid you like!");
            Console.WriteLine("As long as it's a square, you're good to go!");
            Console.WriteLine();
        }

        public static void DisplayTitle()
        {
            Console.WriteLine();
            Console.WriteLine("▀█▀ █ █▀▀ ▄▄ ▀█▀ ▄▀█ █▀▀ ▄▄ ▀█▀ █░█ █▀▄▀█");
            Console.WriteLine("░█░ █ █▄▄ ░░ ░█░ █▀█ █▄▄ ░░ ░█░ █▄█ █░▀░█");
            Console.WriteLine();
        }

        private void DisplayScoreboard()
        {
            string scores = $"{PlayerOne.FirstName}: {PlayerOne.GamesWon} - {PlayerTwo.FirstName}: {PlayerTwo.GamesWon}";
            
            Console.WriteLine(DisplaySeperator(scores));
            Console.WriteLine("Games Won");
            Console.WriteLine($"{scores}");
            Console.WriteLine(DisplaySeperator(scores));
        }

        public void DisplayFinalScores()
        {
            string playerOneScore = $"Final Score for {PlayerOne.FirstName}: {PlayerOne.GamesWon}";
            string playerTwoScore = $"Final Score for {PlayerTwo.FirstName}: {PlayerTwo.GamesWon}";
            
            Player winner = null;
            bool hasWinner = PlayerOne.GamesWon != PlayerTwo.GamesWon;
            if (hasWinner) winner = PlayerOne.GamesWon > PlayerTwo.GamesWon ? PlayerOne : PlayerTwo;
            
            string longerScoreString;
            if (playerOneScore.Length > playerTwoScore.Length) longerScoreString = playerOneScore;
            else longerScoreString = playerTwoScore;

            Console.WriteLine(DisplaySeperator(longerScoreString));
            Console.WriteLine(playerOneScore);
            Console.WriteLine(playerTwoScore);
            if (!hasWinner) Console.WriteLine("The game was a draw!");
            else Console.WriteLine($"{winner.FirstName} wins! Congratulations!!!");
            Console.WriteLine(DisplaySeperator(longerScoreString));
        }

        private static string DisplaySeperator(string middlestring)
        {
            string retString = "";
            for (int i = 0; i < middlestring.Length + 6; i++)
            {
                retString += "-";
            }
            return retString;
        }
    }
}