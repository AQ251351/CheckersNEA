using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CheckersNEA.Square;

namespace CheckersNEA
{
    internal class Input_Handler


    {
        
        public Input_Handler(MouseState mouse, int X, int Y) 
        {
            handleMouseEvent(mouse,X,Y);
        }
        private void handleMouseEvent(MouseState mouse, int X, int Y)
        {
         
                int X_Coordinate = X / CheckersGameHelper.SQUARE_SIZE;
                int Y_Coordinate = Y / CheckersGameHelper.SQUARE_SIZE;

            Square square = CheckersGameHelper.Board[X_Coordinate, Y_Coordinate];
            SquareColour colour = SquareColour.Yellow;
            if (square.Man != null)
            {
                CheckersGameHelper.Board[X_Coordinate, Y_Coordinate] = new Square(colour);
            }
                
            
            

        }
    }
}
