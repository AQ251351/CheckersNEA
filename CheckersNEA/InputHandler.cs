
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
       //enum to hold possible direction values. direction will be relative to the players view not relative to checkers piece.
        
        private Square SquareHighlighted = null;
        private bool PlayerOneTurn;        
        List<TakingSqaureHolder> TotalTakingMovesList = null;

        public enum UserEventAction
        {
            INVALID_SELECTION, SELECTED_PIECE, MOVED_PIECE,
            NOTHING_TO_DO
        }
        //private bool squareChanged;
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
        
        public UserEventAction HandleMouseEvent(MouseState mouse)
        {
        
            // divide by scale factor of the square to get the cooresponding array position
            int columnPosition = mouse.X / BoardContainer.SQUARE_SIZE;
            int rowPosition= mouse.Y / BoardContainer.SQUARE_SIZE;
            //Find position in the array of the square that was clicked on and store it in a variable.

            Square currentSquare = BoardContainer.Board[rowPosition, columnPosition ];

            //runs when the left mouse button is pressed and the square clicked on is not a red square.
            //Red squares are not playable squares and should be ignored.
            if (mouse.LeftButton == ButtonState.Pressed &&
                Square.SquareColour.Red != currentSquare.ColourOfSquare)              
            {

                // if no piece is currently highlighted 
                if (SquareHighlighted == null)
                {
                    // We also ensure the current square has a piece in it and that the piece is
                    // the correct colour for the current player.
                    if (currentSquare.Man != null &&
                        ((currentSquare.Man.IsDarkPiece && PlayerOneTurn) ||
                        (!currentSquare.Man.IsDarkPiece && !PlayerOneTurn)))
                    {
                        if (TotalTakingMovesList != null && TotalTakingMovesList.Count > 0)
                        {
                            // If there are taking moves available, we need to check if the selected square is one of the squares that can make a taking move.
                            bool isTakingMoveSquare = false;
                            foreach (TakingSqaureHolder holder in TotalTakingMovesList)
                            {
                                if (holder.targetSquare == currentSquare)
                                {
                                    isTakingMoveSquare = true;
                                    break;
                                }
                            }
                            if (!isTakingMoveSquare)
                            {
                                // If the selected square is not one of the squares that can make a taking move, we need to ignore the selection and return.
                                return UserEventAction.INVALID_SELECTION;
                            }
                        }
                        SquareHighlighted = CheckAndSelectChosenSquare(currentSquare);
                        return UserEventAction.SELECTED_PIECE;
                    }

                }
                //if a piece is already selected, we want to move it to the new square
                //TODO: we will need to add some logic here to check if the move is valid before moving the piece
                    
                else if (CheckerLogicManager.Instance.CheckIfMoveValid(BoardContainer.Board, SquareHighlighted, currentSquare, TotalTakingMovesList))
                {
                    PieceMove(mouse, currentSquare, TotalTakingMovesList);
                    if (TakingMovePossible())
                    {                        
                        if (TotalTakingMovesList.Count > 0)
                        {
                            // If the piece can make a taking move, we need to highlight the possible squares that it can move to.
                            foreach (TakingSqaureHolder holder in TotalTakingMovesList)
                            {
                                // use a lambda expression to set the colour of all the squares in the list to green
                                holder.getTakingSquares().ForEach(s => s.ColourOfSquare = SquareColour.Green);                              
                            }


                        }
                    }
                    return UserEventAction.MOVED_PIECE;

                }                                                      
            }
            return UserEventAction.NOTHING_TO_DO;

        }

        /******************************************************
         * Method: TakingMovePossible
         * 
         * This method checks if a taking move is possible for the current player.
         * It iterates through the board and checks each piece to see if it can make a taking move.
         * If it can it adds the possible taking moves to a list.
         * It returns true if a taking move is possible, and false otherwise.
         ***********************************************************/

        bool TakingMovePossible()
        {
            // start by clearing the board of any previous taking moves that were highlighted.
            foreach (Square s in BoardContainer.Board)
            {
                if (s.ColourOfSquare == SquareColour.Green)
                {
                    s.ColourOfSquare = SquareColour.Black;
                }
            }

            TotalTakingMovesList = new List<TakingSqaureHolder>();
            foreach (Square s in BoardContainer.Board)
            {
                if (s.Man != null)
                {
                    // Player 1 is dark pieces and player 2 is light pieces.
                    // This checks that the piece being checked is the correct colour for the current player.
                    if ((PlayerOneTurn && s.Man.IsDarkPiece) ||
                        (!PlayerOneTurn && !s.Man.IsDarkPiece))
                    {
                        List<Square> TakingMoves = CheckerLogicManager.Instance.CheckSelectedPieceCanTakeAPiece(BoardContainer.Board, s);
                        if (TakingMoves.Count > 0)
                        {
                            TakingSqaureHolder newHolder = new TakingSqaureHolder(s);
                            newHolder.AddTakingSquares(TakingMoves);
                            TotalTakingMovesList.AddRange(newHolder);
                        }
                    }
                        
                }
             
            }
           
            return (TotalTakingMovesList.Count > 0 ? true : false);
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
                if ((PlayerOneTurn && currentSquare.Man.IsDarkPiece) || 
                    (!PlayerOneTurn && !currentSquare.Man.IsDarkPiece))
                {
                    if (CheckerLogicManager.Instance.CheckSelectedPieceCanMove(BoardContainer.Board, currentSquare) == false)
                    {
                        return null;
                    }                    


                    //highlights square
                    currentSquare.ColourOfSquare = SquareColour.Yellow;

                    // saves current square as to be used later when moving the piece. 
                    
                    SquareHighlighted = currentSquare;

                    return SquareHighlighted;
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
        void PieceMove(MouseState mouse, Square newSquare, List<TakingSqaureHolder> totalTakingMovesList)
        {
          
            if (newSquare.Man == null && newSquare.ColourOfSquare != SquareColour.Red)
            {
                //as soon as the button is pressed the piece should be moved from the old position to the new position
                
                BoardContainer.Board[newSquare.row, newSquare.column].Man =  SquareHighlighted.Man;

                // Now move piece from the old square.
                SquareHighlighted.Man = null;
                
                // Finally, we want to change the colour of the previous square back to black if it was yellow
                if (SquareHighlighted.ColourOfSquare == SquareColour.Yellow)
                {
                    SquareHighlighted.ColourOfSquare = SquareColour.Black;
                }

                if (PlayerOneTurn)
                {
                    PlayerOneTurn = false;
                }
                else
                {
                    PlayerOneTurn = true;
                }
                //allows a new square to be selected after the piece has been moved.
                SquareHighlighted = null;
            }
            
        }
    }
}
