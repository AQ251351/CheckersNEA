using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersNEA
{
    /************************************************************
     * Class: CheckerLogicManager
     * 
     * This class is a singleton that manages the logic of the checkers game.
     * Its job is to keep track of the game state and enforce the rules of the game.
     * It will be responsible for determining if a move is valid, if a piece can be captured, 
     * and if the game has been won.
     ***********************************************************/
    public sealed class CheckerLogicManager
    {
        private static CheckerLogicManager instance = null;

        private CheckerLogicManager()
        {
        }

        public static CheckerLogicManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CheckerLogicManager();
                }
                return instance;
            }
        }

        internal List<Square> CheckSelectedPieceCanTakeAPiece(Square[,] board, Square selectedSquare)
        {
            List <Square> possibleTakingMoves = new List<Square>();

            if (selectedSquare.Man != null)
            {
                checkDirectionTakeMoves(board, selectedSquare, possibleTakingMoves);
                int direction = selectedSquare.Man.IsDarkPiece ? 1 : -1;


            }
                
            {
                return possibleTakingMoves;
            }
            return possibleTakingMoves;

        }

        /************************************************************
         * Method: checkDirectionTakeMoves
         * 
         * This method checks if the selected piece can take an opponent's piece in any direction.
         * It takes in the current board state, the selected square, and a list to store possible taking moves as parameters.
         ***********************************************************/

        private void checkDirectionTakeMoves(Square[,] board, Square selectedSquare, List<Square> possibleTakingMoves)
        {
            if (selectedSquare.Man.IsKing)
            {
                // Kings can move in any diagonal direction, so we need to check all four diagonals
                CheckDiagonalTakeMoves(board, selectedSquare, 2, 1, possibleTakingMoves);
                CheckDiagonalTakeMoves(board, selectedSquare, 2, -1, possibleTakingMoves);
                CheckDiagonalTakeMoves(board, selectedSquare, -2, 1, possibleTakingMoves);
                CheckDiagonalTakeMoves(board, selectedSquare, -2, -1, possibleTakingMoves);
            }
            else
            {
                // Non-king pieces can only move forward diagonally
                int direction = selectedSquare.Man.IsDarkPiece ? 2 : -2;
                CheckDiagonalTakeMoves(board, selectedSquare, direction, 1, possibleTakingMoves);
                CheckDiagonalTakeMoves(board, selectedSquare, direction, -1, possibleTakingMoves);
            }
        }

        /************************************************************
         * Method: CheckDiagonalTakeMoves
         * 
         * This method checks if a piece can take an opponent's piece in a specific diagonal direction.
         * It takes in the current board state, the selected square, 
         * the direction of the move, and a list to store possible taking moves as parameters.
         ***********************************************************/
        private void CheckDiagonalTakeMoves(Square[,] board, Square selectedSquare, int row, int column, List<Square> possibleTakingMoves)
        {
            // sanity check. Are we actually checking a square that has a piece on it?
            if (selectedSquare.Man == null)
            {
                // Work out the possible row and column of the target square based on the direction of the move
                int possibleRow = selectedSquare.row + row;
                int possibleColumn = selectedSquare.column + column;

                // Check if the possible move is within the bounds of the board
                if (possibleRow < 0 || possibleRow >= BoardContainer.BOARD_DIMENSION ||
                    possibleColumn < 0 || possibleColumn >= BoardContainer.BOARD_DIMENSION)
                {
                    return;
                }
                Square targetSquare = board[possibleRow, possibleColumn];

                // We now work out the position of the square that is in between the selected square and the target square. 
                int middleRow = selectedSquare.row + row / 2;
                int middleColumn = selectedSquare.column + column / 2;
               
                Square middleSquare = board[middleRow, middleColumn];

                // Check if the target square is empty and the middle square has an opponent's piece
                if (targetSquare.Man == null && middleSquare.Man != null &&
                    middleSquare.Man.IsDarkPiece != selectedSquare.Man.IsDarkPiece)
                {
                    possibleTakingMoves.Add(targetSquare);
                }
            }
        }



        /************************************************************
         * Method: CheckSelectedPieceCanMove
         * 
         * This method checks if the selected piece can move to any of the valid squares.
         * It takes in the current board state and the selected square as parameters.
         * It returns true if the piece can move, and false otherwise.
         ***********************************************************/
        internal bool CheckSelectedPieceCanMove (Square[,] board, Square selectedSquare)
        {
            // Check if the selected square has a piece on it
            if (selectedSquare.Man == null)
            {
                return false;
            }
            // Check if the piece is a king
            if (selectedSquare.Man.IsKing)
            {
                // Kings can move in any diagonal direction, so we need to check all four diagonals
                return CheckDiagonalMoves(board, selectedSquare, 1, 1) ||
                       CheckDiagonalMoves(board, selectedSquare, 1, -1) ||
                       CheckDiagonalMoves(board, selectedSquare, -1, 1) ||
                       CheckDiagonalMoves(board, selectedSquare, -1, -1);
            }
            else
            {
                // Non-king pieces can only move forward diagonally
                int direction = selectedSquare.Man.IsDarkPiece ? 1 : -1;
                return CheckDiagonalMoves(board, selectedSquare, direction, 1) ||
                       CheckDiagonalMoves(board, selectedSquare, direction, -1);
            }
        }

        /************************************************************
         * Method: CheckDiagonalMoves
         * 
         * This method checks if a piece can move to a specific diagonal square.
         * It takes in the current board state, the selected square, and the direction of the move as parameters.
         * It returns true if the move is valid, and false otherwise.
         ***********************************************************/

        private bool CheckDiagonalMoves(Square[,] board, Square selectedSquare, int possibleRow, int possibleColumn)
        {
            possibleRow = selectedSquare.row + possibleRow;
            possibleColumn = selectedSquare.column + possibleColumn;

            // Check if the possible move is within the bounds of the board
            if (possibleRow < 0 || possibleRow >= BoardContainer.BOARD_DIMENSION ||
                possibleColumn < 0 || possibleColumn >= BoardContainer.BOARD_DIMENSION)
            {
                return false;
            }
            if (board[possibleRow, possibleColumn].Man == null)
            {
                return true;
            }
            return false;
        }

        /*************************************************************
        * Method: CheckIfMoveValid
        * 
        * This method checks if a move is valid based on the current state of the board and the rules of checkers.
        * It takes in the current board state, the selected square, and the target square as parameters.
        * It returns true if the move is valid, and false otherwise.
        * TODO. Still in development. Need to add logic to check if the move is a valid diagonal move, and if the piece is a king or not.
        */
        internal bool CheckIfMoveValid(Square[,] board, Square SelectedSquare, Square targetSquare)
        {
            
            
            // targetSquare needs to be empty for the move to be valid.
            if (targetSquare.Man != null)
            {
                return false;
            }
            
            if (SelectedSquare.Man.IsKing)
            {
                // Kings can move in any diagonal direction.
            }
            else
            {
                
                if (SelectedSquare.Man.IsDarkPiece)
                {
                    int NewRowNum = targetSquare.row;
                    int SelectedRowNum = SelectedSquare.row;
                    return  ( (NewRowNum - SelectedRowNum == 1) ? true : false);
                   
                }
                else
                {
                    int NewRowNum = Convert.ToInt32(targetSquare.row);
                    int SelectedRowNum = Convert.ToInt32(SelectedSquare.row);
                    return ((SelectedRowNum - NewRowNum == 1) ? true : false);                  
                    
                }                
                
            }


            return false;
                          
        }
    }   
}
// validation to make sure moves are valid, and that the game state is updated correctly after each move.
// first check if piece is king otherwise it will only be moving in one direction. Then check if the move is a valid diagonal move. 
// 