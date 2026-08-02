using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersNEA
{

    public class Board
    {
        public Piece[,] Squares { get; set; }
        //property called squares that is a 2D array.Each square can hold a pice or null.

        public Board()
        {
            Squares = new Piece[8, 8];
            // Creates an 8x8 array to represent the board. Each square is currently null.
            CreateBoard();
        }
        private void CreateBoard()
        {
            //Puts Black pieces onto the grid
            for (int column = 0; column < 8; column ++)
            {
                for (int row = 0; row < 3; row++) 
                {
                    if ((column + row) % 2 != 0) 
                    {
                        Squares[column, row] = new Piece("Black");
                    }
                }
            }
            // Puts White pieces onto the grid

            for (int row = 5; row < 8; row++ )
            {
                for (int column = 0; column <8; column ++)
                {
                    if ((column + row) % 2 != 0)
                    {
                        Squares[column, row] = new Piece("White");
                    }
                }
            }
        }
        
        
    }
}
