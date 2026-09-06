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
    /************************************************************
     * Class: InputHandler
     * 
     * This class is responsible for handling mouse events in the checkers game.
     * It processes mouse events and updates the game state accordingly.
     ***********************************************************/
    internal class InputHandler


    {

        /************************************************************
         * Constructor: InputHandler
         * 
         * This constructor initializes the InputHandler class.
         * It sets up the necessary configurations for handling user input.
         ***********************************************************/
        public InputHandler() 
        { }

        /************************************************************
         * Method: handleMouseEvent
         * 
         * This method processes mouse events. It calculates the coordinates of the square that was clicked
         * and carries out the necessary actions based on the state of the square and what 
         * mouse event occurred. 
         ***********************************************************/
        public void HandleMouseEvent(MouseState mouse)
        {


            int X_Coordinate = mouse.X / CheckersGameHelper.SQUARE_SIZE;
            int Y_Coordinate = mouse.Y / CheckersGameHelper.SQUARE_SIZE;

            Square square = CheckersGameHelper.Board[X_Coordinate, Y_Coordinate];
            
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                // TODO we probably want to highlight the peice here and not the square, but for now we will highlight the square
                if (square.Man != null)
                {
                    square.ColourOfSquare = SquareColour.Yellow;
                    CheckersGameHelper.Board[X_Coordinate, Y_Coordinate] = square;
                }
            }
            else if (mouse.LeftButton == ButtonState.Released) {
                // Add code here to handle the case when the left mouse button is released.
            }




        }
    }
}
