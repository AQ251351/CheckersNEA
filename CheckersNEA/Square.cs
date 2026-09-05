namespace CheckersNEA
{
    /************************************************************
     * Class: Square
     * 
     * This class represents a square on the checkers board. 
     * 
     ***********************************************************/

    public class Square
    {
        public enum SquareColour
        { 
            Black, Yellow, Red, 
        }
        public SquareColour ColourOfSquare { set; get; }
          

        public Piece Man { get; set; }

        /************************************************************
         * Constructor: Square
         * 
         * This constructor initializes the square with its colour.
         * The Man piece is set to null, indicating that there is no piece on the square initially.
         * The colour is set where true indicates a dark square and false indicates a light square.
         ***********************************************************/
        public Square (SquareColour c)
        {
            ColourOfSquare = c;
            Man = null;

        }
        

    }
} 