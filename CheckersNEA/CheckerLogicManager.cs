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

        internal bool CheckIfMoveValid(Square[,] board, Square currentSquare, Square targetSquare)
        {
            //TODO Implement logic to check if the move is valid based on the rules of checkers.
            return true;          
        }
    }   
}
