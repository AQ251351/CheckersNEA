
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
  
        private Square SquareSelected = null;
        private bool PlayerOneTurn;
        private bool TakingMove;
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
            TakingMove = false;
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

            TakingMove = TakingMovePossible();
            // divide by scale factor of the square to get the cooresponding array position
            int X_Coordinate = mouse.X / BoardContainer.SQUARE_SIZE;
            int Y_Coordinate = mouse.Y / BoardContainer.SQUARE_SIZE;
            //Find position in the array of the square that was clicked on and store it in a variable.

            Square currentSquare = BoardContainer.Board[X_Coordinate, Y_Coordinate];

            //runs when the left mouse button is pressed`
            if (mouse.LeftButton == ButtonState.Pressed )
            {
                
                // if no piece is selected, we want to select a piece to be moved. If a piece is already selected, we want to move it to the new square.
                if (SquareSelected == null)
                {
                    SquareSelected = CheckAndSelectChosenSquare(currentSquare);
                }
                else
                {
                    //if a piece is already selected, we want to move it to the new square
                    //TODO: we will need to add some logic here to check if the move is valid before moving the piece
                    
                    if (CheckerLogicManager.Instance.CheckIfMoveValid(BoardContainer.Board, SquareSelected, currentSquare))
                    {
                        PieceMove(mouse, currentSquare);
                    }
                    
                }
            }             

        }
        bool TakingMovePossible()
        {
            if (PlayerOneTurn )
            {
                foreach (Square s in BoardContainer.Board)
                {
                    if (s.Man != null)
                    {

                    }
                }
            }
            
            return false;
        }
        /************************************************************
         * Method: CheckAndSelectChosenSquare
         * Checks the chosen square is valid and then selects it
         ***********************************************************/
        Square CheckAndSelectChosenSquare (Square currentSquare )
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
                    return SquareSelected;
                }
            }
            return null;
        }
        /************************************************************
         * Method: ChoosePieceMove
         * 
         * This method is responsible for moving a selected piece to a new square.
         * It checks if the new square is valid for the move and updates the game state accordingly.
         ***********************************************************/
        void PieceMove(MouseState mouse, Square newSquare)
        {
                      

               
            if (newSquare.Man == null && newSquare.ColourOfSquare == SquareColour.Black)
            {
                //as soon as the button is pressed the piece should be moved from the old position to the new position
                
                BoardContainer.Board[newSquare.column, newSquare.row].Man =  SquareSelected.Man;

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
