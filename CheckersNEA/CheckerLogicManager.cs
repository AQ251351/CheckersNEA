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

        internal bool CheckIfMoveValid(Square[,] board, Square SelectedSquare, Square targetSquare)
        {
            // If we have no piece on the current square, then the move is invalid.
            if (SelectedSquare.Man == null)
            {
                return false;
            }
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
                    //dark pieces will only be able to move down the board, so the target square y value must be more than the selected square y value.
                    return  ((targetSquare.row > SelectedSquare.row) ? true : false);
                   
                }
                else
                {
                    return ((targetSquare.row < SelectedSquare.row) ? true : false);                  
                    
                }                
                
            }


            return false;
                          
        }
    }   
}
// validation to make sure moves are valid, and that the game state is updated correctly after each move.
// first check if piece is king otherwise it will only be moving in one direction. Then check if the move is a valid diagonal move. 
// 