using System;
using System.Collections.Generic;

namespace TicTacToe
{
    enum Marks
    {
        Free,
        X,
        O,
        Y,
        T
    }

    class Board
    {
        const int Size = 3;
        public static int[,] WIN_STATE = new int[,] {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 },

                { 0, 3, 6 },
                { 1, 4, 7 },
                { 2, 5, 8 },

                { 0, 4, 8 },
                { 2, 4, 6 }
        };

        int moves = 0;
        Marks[] board = new Marks[Size * Size];
        List<int> _history = new List<int>();
        List<int> _freeCell = new List<int>() {
            0, 1, 2,
            3, 4, 5,
            6, 7, 8
        };

        public List<int> History
        {
            get
            {
                return _history;
            }
        }
        public List<int> FreeCell
        {
            get
            {
                return _freeCell;
            }
        }
        //not in use!
        public int GetSize () { return Size * Size; }
        public void Print ()
        {
            Console.WriteLine();
            Console.WriteLine( "{0}  0   1   2 ", new string( ' ', 3 ) );
            for ( int i = 0; i < Size; ++i )
            {
                Console.WriteLine( "{0}+---+---+---+", new string( ' ', 3 ) );
                Console.Write( new string( ' ', 3 ) );
                for ( int j = 3 * i; j < 3 * i + Size; ++j )
                {
                    if ( board[j] == Marks.Free )
                        Console.Write( "|   " );
                    else
                        Console.Write( "| {0} ", board[j] );
                }
                Console.WriteLine( "| {0}", i );
            }
            Console.WriteLine( "{0}+---+---+---+", new string( ' ', 3 ) );
            Console.WriteLine();
        }

        public bool SetMark ( int position, Marks mark )
        {
            if ( _freeCell.Contains( position ) )
            {
                board[position] = mark;
                moves++;

                _freeCell.Remove( position );
                _history.Add( position );

                return true;
            }
            else return false;
        }

        public Marks GetMark ( int row, int col ) { return board[3 * row + col]; }

        public bool isDraw ()
        {
            Console.Clear();
            if ( moves >= Size * Size )
            {
                Print();
                Console.WriteLine( "Draw!" );
                return true;
            }
            return false;
        }

        public bool isWin ( Player player )
        {
            int i = 0, j = 0, count = 0;
            for ( i = 0; i < 8; ++i )
            {
                for ( j = 0; j < 3; ++j )
                {
                    if ( board[WIN_STATE[i, j]] != player.playerMark )
                        break;
                    count++;
                }
                if ( count == 3 )
                {
                    Print();
                    Console.WriteLine( "Player: {0} wins!", player.playerName );
                    return true;
                }
                count = 0;
            }

            return false;
        }
    }
}
