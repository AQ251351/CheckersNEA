using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersNEA
{
    /***********************************************************
     * Class: Piece
     * 
     * This class represents a piece in the game of checkers. 
     * It has properties to determine if the piece is a dark piece or a light piece, and if it is a king or not.
     ***********************************************************/

    public class Piece
    {
        public bool IsDarkPiece { get; set; }

        public bool IsKing { get; set; }

        public Piece(bool value)
        {
            IsDarkPiece = value;
            IsKing = false;
        }
       
    }
}
