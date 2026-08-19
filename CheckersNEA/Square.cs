namespace CheckersNEA
{
    public class Square
    {
        private string Colour { set; get; }

        public Piece Man { get; set; }

        public Square (string value)
        {
            Colour = value;

        }
     

    }
} 