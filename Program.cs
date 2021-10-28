using System;
using Tic_Tac_Toe;

namespace TicTacToe
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Tic-Tac-Tum (ver 0.75)";
            GameManager gameManager = new();
            GameManager.DisplayGreeting();
            gameManager.CreatePlayers();
            gameManager.SetActivePlayer();
            gameManager.CreateBoard();

            while (gameManager.IsGameRunning)
            {
                //Todo: Display games won dashboard
                gameManager.DrawBoard();
                gameManager.DisplayActivePlayer();
                gameManager.GetTilePosition();
                gameManager.DrawBoard();
                gameManager.CheckForWinner();
                if (gameManager.IsGameRunning) gameManager.CheckForTie();
                //Todo: Modify play again prompt to ask if they want to play again with same players and board size.
                if (!gameManager.IsGameRunning) gameManager.PlayAgainPrompt();
                else gameManager.SetActivePlayer();
            }
            //Todo: Add loop to change players/board size or quit game.
            Console.WriteLine("Goodbye!");
            Console.ReadKey();
        }
    }
}
