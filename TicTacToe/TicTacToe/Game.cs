using System;

namespace TicTacToe
{
    class Game
    {
        Board _board;
        Player[] _players;

        Game ( Board board, Player[] players )
        {
            _board = board;
            _players = players;
        }

        public static Game init ()
        {
            Console.WriteLine( "Welcome to Tic-Tac-Toe" );
            Board board = new Board();
            Player[] players;

            int current = 0;
            bool finish = false;
            String name;
            int mark = -1;
            int gamemode = -1;

            Console.WriteLine( "Choose gamemode: " );
            Console.WriteLine( "(1) > 1 player" );
            Console.WriteLine( "(2) > 2 player" );
            do
            {
                try
                {
                    gamemode = Int32.Parse( Console.ReadKey( true ).KeyChar.ToString() );
                }
                catch ( FormatException ) { };
            } while ( gamemode < 1 || gamemode > 2 );

            Console.Clear();

            if ( gamemode == 1 )
            {
                players = new Player[] { new Human(), new AI() };

                do
                {
                    Console.WriteLine( "Player please enter your name:" );
                    name = Console.ReadLine();
                } while ( name.Trim().Length == 0 );

                do
                {
                    Console.WriteLine( "Player choose mark:\n \t1. X\n\t2. O\n\t3. Y\n\t4. T" );
                    try
                    {
                        mark = Int32.Parse( Console.ReadKey( true ).KeyChar.ToString() );
                    }
                    catch ( FormatException ) { }
                } while ( mark > 4 || mark < 1 );

                players[0].init( name, ( Marks ) mark );
                mark = mark != 4 ? mark + 1 : 2;
                players[1].init( "Bot", ( Marks ) mark );
            }
            else
            {
                players = new Player[] { new Human(), new Human() };

                do
                {
                    do
                    {
                        Console.WriteLine( "Player {0} please enter your name:", current + 1 );
                        name = Console.ReadLine();
                    } while ( name.Trim().Length == 0 );

                    do
                    {
                        Console.WriteLine( "Player {0} choose mark:\n \t1. X\n\t2. O\n\t3. Y\n\t4. T", current + 1 );
                        try
                        {
                            mark = Int32.Parse( Console.ReadKey( true ).KeyChar.ToString() );
                        }
                        catch ( FormatException ) { }
                    } while ( mark > 4 || mark < 1 || ( current == 1 ? ( players[0].playerMark == ( Marks )mark ? true : false ) : false ) );

                    players[current].init( name, ( Marks )mark );
                    current++;

                    if ( current == 2 )
                        finish = true;
                } while ( !finish );
            }

            return new Game( board, players );
        }

        public void Play ()
        {
            int current = -1;
            while ( true )
            {                
                do
                {
                    current = ( current + 1 ) % 2;
                    _players[current].Move( _board );
                } while ( !_board.isWin( _players[current] ) && !_board.isDraw() );

                Console.WriteLine( "Again? (Y/N)" );
                bool enter = false;
                do
                {
                    switch ( Console.ReadKey( true ).KeyChar )
                    {
                        case 'N':
                        case 'n':
                            return;
                        case 'Y':
                        case 'y':
                            _board = new Board();
                            _players[1].Reset();
                            enter = true;
                            break;
                        default:
                            break;
                    }
                } while ( !enter );
            }
        }

        //not in use
        public void PlayerInfo ()
        {
            for ( int i = 0; i < 2; ++i )
            {
                Console.WriteLine( "Player {0}:\n\tName: {1}\n\tMark:{2}", i + 1, _players[i].playerName, _players[i].playerMark );
            }
        }

        //not in use
        public void Print ()
        {
            _board.Print();
        }
    }
}
