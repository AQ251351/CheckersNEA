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
        private Square SquareSelected = null;
        private bool PlayerOneTurn;
        /************************************************************
         * Constructor: InputHandler
         * 
         * This constructor initializes the InputHandler class.
         * It sets up the necessary configurations for handling user input.
         ***********************************************************/
        public InputHandler()
        {
            // CheckersGame is initialized and ready to play. Player one starts the game. He will be the dark pieces.
            // Player two will be the light pieces.
            PlayerOneTurn = true;
        }

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
            int X_Coordinate = mouse.X / BoardContainer.SQUARE_SIZE;
            int Y_Coordinate = mouse.Y / BoardContainer.SQUARE_SIZE;
            //Find position in the array of the square that was clicked on and store it in a variable
            Square currentSquare = BoardContainer.Board[X_Coordinate, Y_Coordinate];

            //runs when the left mouse button is pressed`
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                // if no piece is selected, we want to select a piece to be moved. If a piece is already selected, we want to move it to the new square.
                if (SquareSelected == null)
                {
                    //Checks that square chosen has a piece in it.
                    if (currentSquare.Man != null)
                    {
                        //Checks that the piece chosen is the colour of the player whose turn it is. If it is, then the square is highlighted and the piece is selected.
                        if ((PlayerOneTurn && currentSquare.Man.IsDarkPiece) || (!PlayerOneTurn && !currentSquare.Man.IsDarkPiece))
                        {
                            //highlights square
                            currentSquare.ColourOfSquare = SquareColour.Yellow;

                            // saves current square as to be used later when moving the piece. 
                            BoardContainer.Board[X_Coordinate, Y_Coordinate] = currentSquare;
                            SquareSelected = currentSquare;

                            // Switch turns after a piece is selected
                            if (PlayerOneTurn)
                            {
                                PlayerOneTurn = false;
                            }
                            else
                            {
                                PlayerOneTurn = true;
                            }
                        }
                    }
                }
                else
                {
                    //if a piece is already selected, we want to move it to the new square
                    //TODO: we will need to add some logic here to check if the move is valid before moving the piece
                    int NewX = mouse.X / BoardContainer.SQUARE_SIZE;
                    int NewY = mouse.Y / BoardContainer.SQUARE_SIZE;
                    if (CheckerLogicManager.Instance.CheckIfMoveValid(BoardContainer.Board, SquareSelected, currentSquare))
                    {
                        ChoosePieceMove(mouse, currentSquare);
                    }   
                }
            }             

        }
        /************************************************************
         * Method: ChoosePieceMove
         * 
         * This method is responsible for moving a selected piece to a new square.
         * It checks if the new square is valid for the move and updates the game state accordingly.
         ***********************************************************/
        void ChoosePieceMove(MouseState mouse, Square newSquare)
        {
                      

            int NewX = mouse.X/ BoardContainer.SQUARE_SIZE;
            int NewY = mouse.Y/ BoardContainer.SQUARE_SIZE;
               

               
            if (newSquare.Man == null && newSquare.ColourOfSquare == SquareColour.Black)
            {
                //as soon as the button is pressed the piece should be moved from the old position to the new position               
                BoardContainer.Board[NewX, NewY].Man =  SquareSelected.Man;

                // Now move piece from the old square.
                SquareSelected.Man = null;
                
                // Finally, we want to change the colour of the previous square back to black if it was yellow
                if (SquareSelected.ColourOfSquare == SquareColour.Yellow)
                {
                    SquareSelected.ColourOfSquare = SquareColour.Black;
                }
                //allows a new square to be selected after the piece has been moved.
                SquareSelected = null;
            }
            
        }
    }
}
