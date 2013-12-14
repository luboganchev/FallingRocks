using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Rock
{
    Random random = new Random();

    //Constructor of the class Rock
    public Rock()
    {
        this.Character = rocksCharacters[random.Next(11)];
        this.Color = rocksColors[random.Next(8)];
        this.PositionColumn = random.Next(FallingRocks.playingScreen);
    }
    
    //Constant values of all rocks characters.
    char[] rocksCharacters = { '^', '@', '*', '&', '+', '%', '$', '#', '!', '.', ';' };

    //Constant values of all collors.
    static ConsoleColor[] rocksColors = 
    {  
        ConsoleColor.Blue, 
        ConsoleColor.Cyan, 
        ConsoleColor.DarkCyan, 
        ConsoleColor.Green,
        ConsoleColor.Red,
        ConsoleColor.White,
        ConsoleColor.Yellow,
        ConsoleColor.Magenta
    };

    //Get and set the color of the rock.
    public ConsoleColor Color
    {
        get;
        set;
    }

    //Get and set the character of the rock.
    public char Character
    {
        get;
        set;
    }

    //Get and set column position of the rock.
    public int PositionColumn
    {
        get;
        set;
    }

    //Get and set row position of the rock.
    public int PositionRow
    {
        get;
        set;
    }
}