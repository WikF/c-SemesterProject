using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    abstract class Player
    {
        public string playerName { get; protected set; }
        public Marks playerMark { get; protected set; }

        public abstract void Move ( Board board );
        public abstract void init ( string Name, Marks mark );
        public virtual void Reset () {}
    }

    class Human : Player
    {
        public override void init ( string Name, Marks Mark )
        {
            playerName = Name;
            playerMark = Mark;
        }

        public override void Move ( Board _board )
        {
            int row, col;
            do
            {
                row = -1;
                col = -1;
                Console.Clear();
                Console.WriteLine( "Player {0} your turn", playerName );
                _board.Print();
                do
                {
                    Console.WriteLine( "Row: " );
                    try
                    {
                        row = Int32.Parse( Console.ReadKey().KeyChar.ToString() );
                        Console.WriteLine();
                    }
                    catch ( FormatException ) { }
                } while ( row < 0 || row > _board.GetSize() );
                do
                {
                    Console.WriteLine( "Col: " );
                    try
                    {
                        col = Int32.Parse( Console.ReadKey().KeyChar.ToString() );
                        Console.WriteLine();
                    }
                    catch ( FormatException ) { }
                } while ( col < 0 || col > _board.GetSize() );
            } while ( !_board.SetMark( 3 * row + col, playerMark ) );
        }
    }

    class AI : Player
    {
        int[] Coefficient = new int[] {
             1,   0,   1,
             0,   100, 0,
             1,   0,   1
        };

        int AICoefficient = int.MinValue;
        int HumanCoefficient = int.MaxValue;

        public override void init ( string Name, Marks mark )
        {
            playerName = Name;
            playerMark = mark;
        }

        public override void Move ( Board board )
        {
            if( board.History.Count > 0 )
            {
                _HumanCoefficient( board.History.Last() );
                _possibleWin();
            }               
            int position = _MaxValue( board.FreeCell );
            Coefficient[position] = AICoefficient;
            _AICoefficient( position );

            board.SetMark( position, playerMark );
        }

        void print ()
        {
            Console.WriteLine();
            Console.WriteLine( "{0}  0   1   2 ", new string( ' ', 3 ) );
            for ( int i = 0; i < 3; ++i )
            {
                Console.WriteLine( "{0}+---+---+---+", new string( ' ', 3 ) );
                Console.Write( new string( ' ', 3 ) );
                for ( int j = 3 * i; j < 3 * i + 3; ++j )
                {
                    if ( Coefficient[j] == AICoefficient )
                        Console.Write( "| A " );
                    else if ( Coefficient[j] == HumanCoefficient )
                        Console.Write( "| H " );
                    else
                        Console.Write( "| {0} ", Coefficient[j] );
                }
                Console.WriteLine( "| {0}", i );
            }
            Console.WriteLine( "{0}+---+---+---+", new string( ' ', 3 ) );
            Console.WriteLine();
        }

        void _HumanCoefficient ( int pos )
        {
            int position;
            Coefficient[pos] = HumanCoefficient;

            //  Rows change coefficient
            for ( position = pos % 3; position < 9; position = position + 3 )
            {
                if ( Coefficient[position] != HumanCoefficient && Coefficient[position] != AICoefficient )
                    Coefficient[position] -= 10;
            }

            //  Columns change coefficient
            int start = pos - pos % 3;
            for ( position = start; position < start + 3; ++position )
            {
                if ( Coefficient[position] != HumanCoefficient && Coefficient[position] != AICoefficient )
                    Coefficient[position] -= 10;
            }

            // Diagonal change coefficient
            if ( pos == 0 || pos == 8 )
            {
                for ( position = 0; position < 9; position = position + 4 )
                {
                    if ( Coefficient[position] != HumanCoefficient && Coefficient[position] != AICoefficient )
                        Coefficient[position] -= 10;
                }
            }
            else if ( pos == 4 )
            {
                //  Rows change coefficient
                for ( position = pos % 3; position < 9; position = position + 3 )
                {
                    if ( Coefficient[position] != HumanCoefficient && Coefficient[position] != AICoefficient )
                        Coefficient[position] -= 10;
                }

                //  Columns change coefficient
                start = pos - pos % 3;
                for ( position = start; position < start + 3; ++position )
                {
                    if ( Coefficient[position] != HumanCoefficient && Coefficient[position] != AICoefficient )
                        Coefficient[position] -= 10;
                }
                for ( position = 0; position < 9; position = position + 4 )
                {
                    if ( Coefficient[position] != HumanCoefficient && Coefficient[position] != AICoefficient )
                        Coefficient[position] -= 10;
                }
                for ( position = 2; position <= 6; position = position + 2 )
                {
                    if ( Coefficient[position] != HumanCoefficient && Coefficient[position] != AICoefficient )
                        Coefficient[position] -= 10;
                }
            }
            else if ( pos == 2 || pos == 6 )
            {
                for ( position = 2; position <= 6; position = position + 2 )
                {
                    if ( Coefficient[position] != HumanCoefficient && Coefficient[position] != AICoefficient )
                        Coefficient[position] -= 10;
                }
            }
        }

        void _AICoefficient ( int pos )
        {
            int position;
            Coefficient[pos] = AICoefficient;

            //  Rows change coefficient
            for ( position = pos % 3; position < 9; position = position + 3 )
            {
                if ( Coefficient[position] != HumanCoefficient && Coefficient[position] != AICoefficient )
                    Coefficient[position] += 10;
            }

            //  Columns change coefficient
            int start = pos - pos % 3;
            for ( position = start; position < start + 3; ++position )
            {
                if ( Coefficient[position] != HumanCoefficient && Coefficient[position] != AICoefficient )
                    Coefficient[position] += 10;
            }

            // Diagonal change coefficient
            if ( pos == 0 || pos == 8 )
            {
                for ( position = 0; position < 9; position = position + 4 )
                {
                    if ( Coefficient[position] != HumanCoefficient && Coefficient[position] != AICoefficient )
                        Coefficient[position] += 10;
                }
            }
            else if ( pos == 4 )
            {
                for ( position = 0; position < 9; position = position + 4 )
                {
                    if ( Coefficient[position] != HumanCoefficient && Coefficient[position] != AICoefficient )
                        Coefficient[position] += 10;
                }
                for ( position = 2; position <= 6; position = position + 2 )
                {
                    if ( Coefficient[position] != HumanCoefficient && Coefficient[position] != AICoefficient )
                        Coefficient[position] += 10;
                }
            }
            else if ( pos == 2 || pos == 6 )
            {
                for ( position = 2; position <= 6; position = position + 2 )
                {
                    if ( Coefficient[position] != HumanCoefficient && Coefficient[position] != AICoefficient )
                        Coefficient[position] += 10;
                }
            }
        }

        void _possibleWin ()
        {

            int i = 0, j = 0, count = 0, pos;
            for ( i = 0; i < 8; ++i )
            {
                pos = -1;
                for ( j = 0; j < 3; ++j )
                {
                    if ( Coefficient[Board.WIN_STATE[i, j]] == HumanCoefficient )
                        count++;
                    else if ( Coefficient[Board.WIN_STATE[i, j]] != AICoefficient && Coefficient[Board.WIN_STATE[i, j]] != HumanCoefficient )
                        pos = Board.WIN_STATE[i, j];
                }
                if ( count == 2 && pos != -1 )
                {
                    Coefficient[pos] = 100;
                }
                count = 0;
            }

            i = 0;
            j = 0;
            count = 0;
            for ( i = 0; i < 8; ++i )
            {
                pos = -1;
                for ( j = 0; j < 3; ++j )
                {
                    if ( Coefficient[Board.WIN_STATE[i, j]] == AICoefficient )
                        count++;
                    else if ( Coefficient[Board.WIN_STATE[i, j]] != AICoefficient && Coefficient[Board.WIN_STATE[i, j]] != HumanCoefficient )
                        pos = Board.WIN_STATE[i, j];
                }
                if ( count == 2 && pos != -1 )
                {
                    Coefficient[pos] = 1000;
                }
                count = 0;
            }
        }

        public override void Reset ()
        {
            Coefficient = new int[] {
                 1,   0,   1,
                 0,   100, 0,
                 1,   0,   1
            };
        }

        int _MaxValue ( List<int> freeCell )
        {
            int maxPos = -1, maxValue = int.MinValue;
            foreach ( int cell in freeCell )
            {
                if ( maxValue < Coefficient[cell] )
                {
                    maxValue = Coefficient[cell];
                    maxPos = cell;
                }
            }

            return maxPos;
        }
    }
}
