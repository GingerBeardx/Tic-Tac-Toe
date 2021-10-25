﻿using System;

Console.Title = "Tic-Tac-Toe";
GameManager gameManager = new GameManager();
//gameManager.CreatePlayers();
//gameManager.SetActivePlayer();
gameManager.CreateBoard();
gameManager.DrawBoard();

//gameManager.DisplayActivePlayer();
Console.ReadKey();

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

        PlayerOne = new Player(playerOneName, 'X');
        PlayerTwo = new Player(playerTwoName, 'O');
    }

    public void CreateBoard()
    {
        bool isValid = false;
        int boardTiles = 0;
        while (!isValid)
        {
            Console.Write("Please enter the number of tiles for the game board (should be a perfect square): ");
            bool response = int.TryParse(Console.ReadLine(), out boardTiles);
            if (Math.Sqrt(boardTiles) % 1 == 0 && response && boardTiles > 0) 
            {
                Board = new GameBoard(boardTiles);
                isValid = true;
            } 
            else if (!response) Console.WriteLine("That entry was not a recognizable number.");
            else Console.WriteLine("That response is not a perfect square.");
            Console.WriteLine();
        }
    }

    public void DisplayActivePlayer()
    {
        Console.WriteLine($"{CurrentPlayer.FirstName}, it is your turn.");
    }

    public void DrawBoard()
    {
        Board.DrawGameBoard();
    }

    public void SetActivePlayer()
    {
        if (CurrentPlayer == null) CurrentPlayer = PlayerOne;
        else if (CurrentPlayer == PlayerOne) CurrentPlayer = PlayerTwo;
        else CurrentPlayer = PlayerOne;
    }
}

public class Player
{
    public string FirstName { get; set; }
    public int GamesWon { get; set; }
    public char PlayerToken { get; set; }

    public Player(string firstName, char playerToken)
    {
        FirstName = firstName;
        GamesWon = 0;
        PlayerToken = playerToken;
    }

    public void PlayerWon() => GamesWon++;
}

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

public class GameTile
{
    public bool IsTokenX { get; set; }
    public bool IsTokenO { get; set; }
    public bool IsTokenBlank { get; set; }

    public GameTile()
    {
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

    public string TileDisplay(int position)
    {
        
        if (IsTokenO) return " O ";
        if (IsTokenX) return " X ";
        if (position.ToString().Length > 1) return $" {position}";
        return $" {position} ";
    }
}