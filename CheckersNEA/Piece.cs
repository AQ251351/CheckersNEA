using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersNEA
{


    public class Piece
    {
        public string Colour { get; set; }

        public bool IsKing { get; set; }

        public Piece(string colour)
        {
            Colour = colour;
            IsKing = false;
        }
        public void MakeKing()
        {
            IsKing = true;
        }
    }
}
