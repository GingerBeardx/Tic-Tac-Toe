using System;

namespace TicTacToe
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Tic-Tac-Toe";
            GameManager gameManager = new();
            //Todo: Create a game greeting
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
                //Todo: Check for tie condition
                if (!gameManager.IsGameRunning) gameManager.PlayAgainPrompt();
                else gameManager.SetActivePlayer();
            }
            Console.WriteLine("Goodbye!");
            Console.ReadKey();
        }
    }
}
