using xFunc.Maths;

namespace Calculator;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Title = "Calculator";
        Console.CursorVisible = false;
        Program program = new();
        program.ShowButtons();
    }

    public static string[,] buttons = {{"(", ")", "C", "<"},
                                        {"7", "8", "9", "/"},
                                        {"4", "5", "6", "*"},
                                        {"1", "2", "3", "-"},
                                        {".", "0", "=", "+"}};

    public string Text = "";

    public int X = 0;
    public int Y = 0;

    public void ShowButtons()
    {
    Start:

        Console.Clear();

        Console.WriteLine("+------------------+");
        Console.Write("|");

        Console.WriteLine(Text);

        Console.SetCursorPosition(19, 1);
        Console.Write("|");
        Console.SetCursorPosition(0,2);

        Console.WriteLine("+------------------+");

        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                if(X == j && Y == i)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[ " + buttons[i,j] + " ]");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("[ " + buttons[i,j] + " ]");
                }
            }
            Console.Write("\n");
        }

        SelectButton();

        goto Start;
    }

    public void SelectButton()
    {
        ConsoleKeyInfo keyPressed = Console.ReadKey();

        if(keyPressed.Key == ConsoleKey.UpArrow)
        {
            Y--;

            if(Y < 0){ Y = 4;}
        }

        else if(keyPressed.Key == ConsoleKey.DownArrow)
        {
            Y++;

            if(Y > 4){ Y = 0;}
        }

        else if(keyPressed.Key == ConsoleKey.LeftArrow)
        {
            X--;

            if(X < 0){ X = 3;}
        }

        else if(keyPressed.Key == ConsoleKey.RightArrow)
        {
            X++;

            if(X > 3){ X = 0;}
        }
        
        else if(keyPressed.Key == ConsoleKey.Enter)
        {
            ButtonFunction();
        }
    }

    public void ButtonFunction()
    {
        if(X == 2 && Y == 4) //Button Equal
        {
           Calculate();
        }
        else if(X == 2 && Y == 0) //Button Clear
        {
            Text = "";
        }
        else if(X == 3 && Y == 0) //Button Backspace
        {
            if(Text.Length > 0)
            {
                Text = Text.Remove(Text.Length - 1);
            }
            else
            {
                Text = "";
            }
        }
        else
        {
            if(Text.Length < 18)
            {
                Text += buttons[Y,X];
            }
        }
    }

    public void Calculate()
    {
        var processor = new Processor();

        try
        {
            string result = Convert.ToString(processor.Solve(Text)) ?? "0";

            if(result.Length > 18)
            {
                Text = result.Remove(18);
            }
            else
            {
                Text = result;
            }
        }
        catch(Exception)
        {
            Text = "";
        }
    }
}