using System;
using System.Threading;


public class Snake
{

    int Height = 40; // wysokość
    int Width = 60; //szerokość planszy

    int[] X = new int[60];
    int[] Y = new int[60];

    //inicjowanie współrzędnych owocu 
    int fruitX; 
    int fruitY;
    
    
    //incjowanie długości węża
    int parts = 3;


    private ConsoleKeyInfo keyInfo;
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

    public void WriteBoard() // tworzenie tablicy do gry 

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
    public void Input() //wprowadzanie klawiszy 
    {
        if (Console.KeyAvailable)
        {
            keyInfo = Console.ReadKey(true); //pobiera klawisz z klawiatury
            key = keyInfo.KeyChar; 
        }
    }

    public void WritePoint(int x, int y)  //rysuje człown węża
    {

        Console.SetCursorPosition(x, y); 
        Console.Write("#");


    }

    public void Logic() //logika gry 
    {
        if (X[0] == fruitX)
        {
            if (Y[0] == fruitY)
            {
                parts++;
                fruitX = rnd.Next(maxValue:58, minValue:10);
                fruitY = rnd.Next(maxValue:10, minValue:2);
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
            if (X[i] > Width | X[i] < 0 | Y[i] > Height) //koniec gry gdy wyjdzie się poza linie 
            {
                throw new System.ArgumentOutOfRangeException();
            }
        }

        Thread.Sleep(200);
    }
}



