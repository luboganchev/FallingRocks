using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class DrawOnTheConsole
{
    //Print given string on the console.
    public static void PrintOnTheConsole(int x, int y, string str, ConsoleColor color)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(str);
    }

    //Remove char from the console by given column and row.
    public static void RemoveFromConsole(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(" ");
    }

    //Draw rocks on the console.
    public static void DrawRocks(int x, int y, char ch, ConsoleColor color)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(ch);
    }

    //Draw game over message.
    public static void PrintGameOver()
    {
        PrintOnTheConsole(FallingRocks.playingScreen + 5, 15, "Game Over!", ConsoleColor.DarkRed);
        PrintOnTheConsole(FallingRocks.playingScreen + 5, 17, "Do you wanna", ConsoleColor.Yellow);
        PrintOnTheConsole(FallingRocks.playingScreen + 5, 18, "play again?", ConsoleColor.Yellow);
        PrintOnTheConsole(FallingRocks.playingScreen + 2, 20, "Press 'Y' for YES", ConsoleColor.Green);
        PrintOnTheConsole(FallingRocks.playingScreen + 3, 21, "and 'N' for NO!", ConsoleColor.Red);
    }

    //Draw current score on the console.
    public static void PrintScore(int passedRocks)
    {
        FallingRocks.score += passedRocks;
        PrintOnTheConsole(FallingRocks.playingScreen + 12, 5, FallingRocks.score.ToString(), ConsoleColor.Green);
    }

    //Draw right part of the screen 'Menu'
    public static void DrawMenu()
    {
        int height = 0;
        while (height < Console.WindowHeight)
        {
            PrintOnTheConsole(FallingRocks.playingScreen + 1, height, "|", ConsoleColor.White);
            height++;
        }
        PrintOnTheConsole(FallingRocks.playingScreen + 5, 5, string.Format("Score: {0}", FallingRocks.score), ConsoleColor.White);
    }
}