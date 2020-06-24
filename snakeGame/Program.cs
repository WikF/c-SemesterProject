using System;
namespace snakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Do you want to play Snake? Press y for yes, n for no.");
            Console.WriteLine("Nawigate with W - up, S - down, D - right, A - left. Enjoy!");

            if (Console.ReadLine() == "y")
            {
                Snake snake = new Snake();
                while (true)
                {
                    snake.WriteBoard();
                    snake.Input();
                    snake.Logic();
                }
                Console.ReadKey();
            }
            
        }
    }
}
