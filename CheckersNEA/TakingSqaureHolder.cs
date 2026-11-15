using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersNEA
{
    /************************************************************
     * Class: TakingSqaureHolder
     * 
     * This class is used to hold a target square and a list of squares that can be taken.
     * It is used in the game logic to determine valid moves and captures.
     ***********************************************************/
    internal class TakingSqaureHolder
    {

      
        // Target square is the one that the player is trying to move
        public Square targetSquare {get; set; }

        // List of squares that can be potentially taken by the target square.  
        private List<Square> takingSquares = new List<Square>();

        public TakingSqaureHolder(Square NewTargetSquare)
        {
            this.targetSquare = NewTargetSquare;
        }
        
       

        public List<Square> getTakingSquares()
        {
            return takingSquares;
        }       
        public void AddTakingSquares(List<Square> squares)
        {
            takingSquares.AddRange(squares);
        }

    }
}
