using System;

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
        int loops = Convert.ToInt32(Math.Sqrt(Board.Board.Length));
        int consecutive = 0;
        int startingPosition = 0;
        bool foundWinner = false;
        //Todo: Move win checks to GameBoard class
        //Check Rows
        for (int i = 0; i < Board.Board.Length; i++)
        {
            if (foundWinner) break;
            for (int j = startingPosition; j < startingPosition + loops; j++)
            {
                if (j >= Board.Board.Length) break;
                if (Board.Board[j].GetToken() == CurrentPlayer.PlayerToken) consecutive++;
                else consecutive = 0;        
            }
            if (consecutive == loops) foundWinner = true;
            else startingPosition += loops;
        }

        //Check columns
        consecutive = 0;
        for (int i = 0; i < loops; i++)
        {
            int checkSpot = 0 + i;
            for (int j = 0; j < loops; j++)
            {
                if (Board.Board[checkSpot].GetToken() == CurrentPlayer.PlayerToken) consecutive++;
                else consecutive = 0;
                checkSpot += loops;
            }
            if (consecutive == loops) foundWinner = true;
            
        }
        //Check diagonals
        int highValue = loops + 1;
        int lowValue = loops - 1;
        int checkDiagonal = 0;
        consecutive = 0;
        
        if (!foundWinner)
        {
            for (int i = 0; i < loops; i++) //left diagonal
            {
                if (Board.Board[checkDiagonal].GetToken() == CurrentPlayer.PlayerToken) consecutive++;
                else break;
                checkDiagonal += highValue;
            }
            if (consecutive == loops) foundWinner = true;
        }

        if (!foundWinner) //right diagonal
        {
            consecutive = 0;
            checkDiagonal = lowValue;
            for (int i = 0; i < loops; i++)
            {
                if (Board.Board[checkDiagonal].GetToken() == CurrentPlayer.PlayerToken) consecutive++;
                else break;
                checkDiagonal += lowValue;
            }
            if (consecutive == loops) foundWinner = true;
        }

        if (foundWinner)
        {
            Console.WriteLine($"{CurrentPlayer.FirstName} wins!!!");
            CurrentPlayer.PlayerWon();
            //Todo: Move play again prompt to own method
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
}
