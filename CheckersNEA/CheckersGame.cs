using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersNEA
{

    public class CheckersGame
    {
        public const int BOARD_DIMENSION = 8;
        public const string BLACK_COLOUR = "Black";
        public const string White_COLOUR = "White";
        public Square[,] Board { get; set; }
        //property called squares that is a 2D array.Each square can hold a pice or null.

        public CheckersGame()
        {
            Board = new Square[BOARD_DIMENSION, BOARD_DIMENSION];
            // Creates an 8x8 array to represent the board. Each square is currently null.
            CreateBoard();
            AddPiecesToBoard();
            

        }

        private void AddPiecesToBoard()
        {

            for (int column = 0; column < 3; column++)
            {
                for (int row = 0; row < BOARD_DIMENSION; row++)
                {
                    if ((column + row) % 2 == 0)
                    {
                        Board[column, row] = new Piece(BLACK_COLOUR);
                    }
                    else
                    {
                        Board[column, row] = new Piece(White_COLOUR);
                    }
                }
            }
        }

        private void CreateBoard()
        {
            //Sets up the board squares to be black or white,
            for (int column = 0; column < BOARD_DIMENSION; column ++)
            {
                for (int row = 0; row < BOARD_DIMENSION; row++) 
                {
                    if ((column + row) % 2 == 0) 
                    {
                        Board[column, row] = new Square(BLACK_COLOUR);
                    }
                    else
                    {
                        Board[column, row] = new Square(White_COLOUR);
                    }
                }
            }
            // Puts White pieces onto the grid
        }
        
    }
}
