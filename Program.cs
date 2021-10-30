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

            while (true)
            {
                gameManager.CreateBoard();

                while (gameManager.IsGameRunning)
                {
                    gameManager.DrawBoard();
                    gameManager.DisplayActivePlayer();
                    gameManager.GetTilePosition();
                    gameManager.DrawBoard();
                    gameManager.CheckForWinner();
                    if (gameManager.IsGameRunning) gameManager.CheckForTie();
                    if (!gameManager.IsGameRunning) gameManager.PlaySameBoardAgainPrompt();
                    else gameManager.SetActivePlayer();
                }
                gameManager.PlaySamePlayersAgainPrompt();
                if (gameManager.PlaySamePlayers) continue;
                gameManager.ContinuePlaying();
                if (gameManager.IsGameOver) break;
                gameManager.CreatePlayers();
            }

            gameManager.DisplayFinalScores();
            Console.WriteLine("Goodbye!");
            Console.ReadKey();
        }
    }
}
