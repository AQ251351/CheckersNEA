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
        int currentX = 0;
        int currentY = 0;
        bool PieceSelected = false;
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


            // divide by scale factor of the square to get the cooresponding array position
            int X_Coordinate = mouse.X / CheckersGameHelper.SQUARE_SIZE;
            int Y_Coordinate = mouse.Y / CheckersGameHelper.SQUARE_SIZE;

            Square square = CheckersGameHelper.Board[X_Coordinate, Y_Coordinate];

            //Runs when button is pressed and there is not a piece already selected 
            if (mouse.LeftButton == ButtonState.Pressed && PieceSelected == false)
            {

                // TODO we probably want to highlight the piece here and not the square, but for now we will highlight the square
                if (square.Man != null)
                {
                    //highlights square
                    square.ColourOfSquare = SquareColour.Yellow;
                    CheckersGameHelper.Board[X_Coordinate, Y_Coordinate] = square;
                    Square clickedSquare = CheckersGameHelper.Board[X_Coordinate, Y_Coordinate];
                }
                

            }
            
            // runs when the button is let go off or if a piece has been selected
            else if (mouse.LeftButton == ButtonState.Released || PieceSelected == true)
            {
                // code that was used previously not relevant right now
                //int targetX = mouse.X / CheckersGameHelper.SQUARE_SIZE;
                //int targetY = mouse.Y / CheckersGameHelper.SQUARE_SIZE;
                //CheckersGameHelper.Board[targetX, targetY].Man = CheckersGameHelper.Board[mouse.X, mouse.Y].Man;


                // Add code here to handle the case when the left mouse button is released.

                //stored the (x,y) value of the square selected
                if (PieceSelected == false)
                {
                    currentX = mouse.X / CheckersGameHelper.SQUARE_SIZE;
                    currentY = mouse.Y / CheckersGameHelper.SQUARE_SIZE;
                }



                if (currentX == X_Coordinate && currentY == Y_Coordinate)
                {
                    // makes sure the (x,y) value is not constantly overwritten
                    PieceSelected = true;

                }
                
                ChoosePieceMove(mouse, currentX, currentY);
            }

            else 
            {
                return ;
            }


        }
        //Method for collecting information about the square the piece will move too and then moving it.
        static void ChoosePieceMove(MouseState mouse, int currentX, int currentY)
        {
           
            if (mouse.LeftButton == ButtonState.Pressed )
            {

                int NewX = mouse.X/ CheckersGameHelper.SQUARE_SIZE;
                int NewY = mouse.Y/ CheckersGameHelper.SQUARE_SIZE;

                Square NewSquare = CheckersGameHelper.Board[NewX, NewY];

               
                if (NewSquare.Man == null && NewSquare.ColourOfSquare == SquareColour.Black)
                {
                    //as soon as the button is pressed the piece should be moved from the old position to the new position 
                    CheckersGameHelper.Board[NewX, NewY] = NewSquare;
                    CheckersGameHelper.Board[NewX, NewY].Man = CheckersGameHelper.Board[currentX,currentY].Man;
                }
            }
            return;
        }
    }
}
// want to be able to move a piece
// do this by recoding the orginal position and the position it will be moved too
// Then rewrite the piece into that postion