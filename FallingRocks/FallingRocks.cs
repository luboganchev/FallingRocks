using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Collections;

class FallingRocks
{
    /* Implement the "Falling Rocks" game in the text console. 
     * A small dwarf stays at the bottom of the screen and can move left and right (by the arrows keys). 
     * A number of rocks of different sizes and forms constantly fall down and you need to avoid a crash.
     Rocks are the symbols ^, @, *, &, +, %, $, #, !, ., ;, - distributed with appropriate density.
     * The dwarf is (O). Ensure a constant game speed by Thread.Sleep(150).
     * Implement collision detection and scoring system.*/
    static int left = 0;
    static int right = 1;

    public static int playingScreen = 0;
    static int dwarfDirection = 0;

    static int dwarfBeginPositionColumn = 0;
    static int dwarfBeginPositionRow = 0;

    static Random randomNumber = new Random();
    public static int score = 0;
    static int maxScore = 0;

    public static int autonumber = 0;

    static Dictionary<int, Rock> rocksCollection = new Dictionary<int, Rock>();

    // Set the console screen size and also the playing screen.
    static void SetGameField()
    {
        Console.WindowHeight = 30;
        Console.BufferHeight = 30;

        Console.WindowWidth = 70;
        Console.BufferWidth = 70;

        playingScreen = (Console.WindowWidth - 20);

        dwarfBeginPositionColumn = playingScreen / 2;
        dwarfBeginPositionRow = Console.WindowHeight - 1;
    }

    //Starting method
    static void Main()
    {
        PlayGame();
    }

    //The main logic of the game.
    static void PlayGame()
    {
        SetGameField();
        DrawOnTheConsole.DrawMenu();
        DrawOnTheConsole.PrintOnTheConsole(dwarfBeginPositionColumn, dwarfBeginPositionRow, "(0)", ConsoleColor.Green);
        while (true)
        {
            GenerateRocks();
            bool isGamePlay = ChangeRockPosition();
            if (isGamePlay == false)
            {
                break;
            }
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                ChangePlayerDirection(key);
                MoveDwarf();
                DrawOnTheConsole.PrintOnTheConsole(dwarfBeginPositionColumn, dwarfBeginPositionRow, "(0)", ConsoleColor.Green);
            }
            Thread.Sleep(150);
        }
        DrawOnTheConsole.PrintGameOver();
        IsGameContinue();
    }

    //Restart the game and print Max Scores
    static void ResetGame()
    {
        Console.Clear();
        rocksCollection.Clear();
        autonumber = 0;
        if (score > maxScore)
        {
            maxScore = score;
        }
        if (maxScore > 0)
        {
            DrawOnTheConsole.PrintOnTheConsole(playingScreen + 4, 7, string.Format("Max score: {0}", maxScore), ConsoleColor.DarkGreen);
        }
        score = 0;
        PlayGame();
    }

    //Waiting of the player to press Y or N.
    static void IsGameContinue()
    {
        ConsoleKeyInfo key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Y)
        {
            ResetGame();
        }
        if (key.Key == ConsoleKey.N)
        {
            Environment.Exit(0);
        }
        else IsGameContinue();
    }

    //Change the direction of the player (Left or Right);
    static void ChangePlayerDirection(ConsoleKeyInfo key)
    {
        if (key.Key == ConsoleKey.LeftArrow)
        {
            dwarfDirection = left;
        }
        if (key.Key == ConsoleKey.RightArrow)
        {
            dwarfDirection = right;
        }
    }

    //Change the position of the dwarf on the screen.
    static void MoveDwarf()
    {
        if (IsOutOfBoundry() == false)
        {
            if (dwarfDirection == right)
            {
                DrawOnTheConsole.RemoveFromConsole(dwarfBeginPositionColumn, dwarfBeginPositionRow);
                dwarfBeginPositionColumn++;
            }
            if (dwarfDirection == left)
            {
                DrawOnTheConsole.RemoveFromConsole(dwarfBeginPositionColumn + 2, dwarfBeginPositionRow);
                dwarfBeginPositionColumn--;
            }
        }
    }

    //Check is the dwarf out of the playing screen.
    static bool IsOutOfBoundry()
    {
        if (dwarfDirection == left && dwarfBeginPositionColumn > 0)
        {
            return false;
        }
        if (dwarfDirection == right && dwarfBeginPositionColumn < playingScreen - 2)
        {
            return false;
        }
        return true;
    }

    //Create new random rocks and push them into the collection.
    static void GenerateRocks()
    {
        int rocksInTheRow = randomNumber.Next(4);
        for (int i = 0; i <= rocksInTheRow; i++)
        {
            Rock rockObject = new Rock();
            rocksCollection.Add(autonumber, rockObject);
            autonumber++;
        }
    }

    //Change the position of the rocks and return if the game is over.
    static bool ChangeRockPosition()
    {
        List<int> removingRocks = new List<int>();
        for (int i = 0; i < rocksCollection.Count; i++)
        {
            var itemValue = rocksCollection.ElementAt(i).Value;

            if (itemValue.PositionRow > 0)
            {
                DrawOnTheConsole.RemoveFromConsole(itemValue.PositionColumn, itemValue.PositionRow);
            }
            if (itemValue.PositionRow < Console.WindowHeight - 1)
            {
                itemValue.PositionRow++;
                bool isGameOver = CollisionDetect(itemValue.PositionColumn, itemValue.PositionRow);
                if (isGameOver == true)
                {
                    return false;
                }
                DrawOnTheConsole.DrawRocks(itemValue.PositionColumn, itemValue.PositionRow, itemValue.Character, itemValue.Color);
            }
            else
            {
                removingRocks.Add(rocksCollection.ElementAt(i).Key);
            }
        }
        DrawOnTheConsole.PrintScore(removingRocks.Count);
        foreach (var rock in removingRocks)
        {
            rocksCollection.Remove(rock);
        }
        return true;
    }

    //Detect is there collision between some rock and the dwarf.
    static bool CollisionDetect(int x, int y)
    {
        if (dwarfBeginPositionRow == y)
        {
            if (dwarfBeginPositionColumn == x || dwarfBeginPositionColumn == (x - 1) || dwarfBeginPositionColumn == (x - 2))
            {
                return true;
            }
        }
        return false;
    }
}