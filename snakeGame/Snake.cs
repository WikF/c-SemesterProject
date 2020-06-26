using System;
using System.Threading;


public class Snake
{

    int Height = 20; // wysokość
    int Width = 30; //szerokość planszy

    int[] X = new int[50];
    int[] Y = new int[50];

    int fruitX;
    int fruitY;

    int parts = 3;


    ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
    char key = 'W';

    Random rnd = new Random();

    public Snake()
    {
        X[0] = 5;
        Y[0] = 5;
        Console.CursorVisible = false;
        fruitX = 10;
        fruitY = 10;
    }

    public void WriteBoard()
    {
        Console.Clear();
        for (int i = 1; i <= (Width ); i++)
        {
            Console.SetCursorPosition(i, 1);
            Console.Write("*");

        }
        for (int i = 1; i <= (Width ); i++)
        {
            Console.SetCursorPosition(i, (Height + 2));
            Console.Write("*");

        }
        for (int i = 1; i <= (Height ); i++)
        {
            Console.SetCursorPosition(1, i);
            Console.Write("*");

        }
        for (int i = 1; i <= (Height ); i++)
        {
            Console.SetCursorPosition((Width), i);
            Console.Write("*");

        }

    }
    public void Input()
    {
        if (Console.KeyAvailable)
        {
            keyInfo = Console.ReadKey(true); //pobiera klawisz z klawiatury
            key = keyInfo.KeyChar; //zamiena klawisz na wartość char i przypisuje do mniennej  
        }
    }

    public void WritePoint(int x, int y)//rysuje człown węża
    {

        Console.SetCursorPosition(x, y); //ustawia kursor
        Console.Write("#");


    }

    public void Logic()
    {
        if (X[0] == fruitX)
        {
            if (Y[0] == fruitY)
            {
                parts++;
                fruitX = rnd.Next(maxValue:29, minValue:2);
                fruitY = rnd.Next(maxValue:29, minValue:2);
            }
        }

        for (int i = parts; i > 1; i--) //logika kolejnych członów 
        {
            X[i - 1] = X[i - 2];
            Y[i - 1] = Y[i - 2];
            

        }
        switch (key)
        {
            case 'w':
                Y[0]--;
                break;
            case 's':
                Y[0]++;
                break;
            case 'd':
                X[0]++;
                break;
            case 'a':
                X[0]--;
                break;

        }
        
        for (int i = 0; i <= (parts - 1); i++) //rysuje węża
        {
            WritePoint(X[i], Y[i]);
            WritePoint(fruitX, fruitY);
            if (X[i] > Width | X[i] < 0)
            {
                Console.WriteLine("KONIEC GRY");
            }
        }

        Thread.Sleep(200);
    }
}

