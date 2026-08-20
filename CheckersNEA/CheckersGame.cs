using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersNEA
{
    /************************************************************
     * Class: CheckersGame
     * 
     * This class represents the game of checkers.
     ***********************************************************/
    public class CheckersGame
    {
        public const int BOARD_DIMENSION = 8;
        public const bool DARK_INDICATOR = true;
        public const bool LIGHT_INDICATOR = false;

        // property called Board that is a 2D array of Squares. Each square has an associated Piece,
        // which can be null if there is no piece on that square.
        
        public Square[,] Board { get; set; }

        /************************************************************
         * Constructor: CheckersGame
         * 
         * Default constructor for the CheckersGame class. 
         ***********************************************************/
        public CheckersGame()
        { }

        /*
         * **********************************************************
         * Method: IntilizeGame
         * 
         * This method initializes the game by creating the board and adding pieces to it.
         * Needs to be called before the game can be played.
         *************************************************************/
        public void IntilizeGame()
        {
            Board = new Square[BOARD_DIMENSION, BOARD_DIMENSION];
            // Creates an 8x8 array to represent the board. Each square is currently null.
            CreateBoard();
            AddPiecesToBoard();

        }

        /************************************************************
         * Method: AddPiecesToBoard
         * 
         * This method adds pieces to the board. It places dark pieces on the first three rows and 
         * light pieces on the last three rows.
         ***********************************************************/
          
        private void AddPiecesToBoard()
        {
            // Puts Dark pieces onto the board 
            for (int column = 0; column < 3; column++)
            {
                for (int row = 0; row < BOARD_DIMENSION; row++)
                {
                    if ((column + row) % 2 != 0)
                    {
                        Square square = Board[column, row];
                        square.Man = new Piece(DARK_INDICATOR);
                    }
                    
                }
            }

            // Puts Light pieces onto the board
            for (int column = 5; column < 8; column++)
            {
                for (int row = 0; row < BOARD_DIMENSION; row++)
                {
                    if ((column + row) % 2 != 0)
                    {
                        Square square = Board[column, row];
                        square.Man = new Piece(LIGHT_INDICATOR);
                    }

                }
            }
        }

        /************************************************************
         * Method: CreateBoard
         * 
         * This method sets up the board by assigning the colour to each square.
         ***********************************************************/
        private void CreateBoard()
        {
            //Sets up the board squares to be black or white,
            for (int column = 0; column < BOARD_DIMENSION; column ++)
            {
                for (int row = 0; row < BOARD_DIMENSION; row++) 
                {
                    if ((column + row) % 2 != 0) 
                    {
                        Board[column, row] = new Square(DARK_INDICATOR);
                    }
                    else
                    {
                        Board[column, row] = new Square(LIGHT_INDICATOR);
                    }
                }
            }          
        }
        
    }
}
