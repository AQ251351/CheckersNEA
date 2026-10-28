using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersNEA
{
    internal class TakingSqaureHolder
    {

      
        public Square targetSquare {get; set; }
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
