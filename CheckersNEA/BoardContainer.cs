using System.Collections.Generic;
using static CheckersNEA.Square;

namespace CheckersNEA
{
    /************************************************************
     * Class: BoardContainer
     * 
     * This class represents the checkers board game. It has a 2 dimensional
     * array of squares. Each square may or may not hold a piece.
     ***********************************************************/
    public class BoardContainer
    {
        public const int SQUARE_SIZE = 60;
        public const int BOARD_DIMENSION = 8;
        public const bool DARK_INDICATOR = true;
        public const bool LIGHT_INDICATOR = false;

        

        // property called Board that is a 2D array of Squares. Each square has an associated Piece,
        // which can be null if there is no piece on that square.
        
        public static Square[,] Board { get; set; }

        /************************************************************
         * Constructor: BoardContainer
         * 
         * Default constructor for the BoardContainer class. 
         ***********************************************************/
        public BoardContainer()
        {
            InitializeGame();
        }

        /*
         * **********************************************************
         * Method: InitializeGame
         * 
         * This method initializes the game by creating the board and adding pieces to it.
         * Needs to be called before the game can be played.
         *************************************************************/
        public void InitializeGame()
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
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < BOARD_DIMENSION; column++)
                {
                    if ((column + row) % 2 != 0)
                    {
                        Square square = Board[column,row];
                        square.Man = new Piece(DARK_INDICATOR);
                    }
                    
                }
            }

            // Puts Light pieces onto the board
            for (int row = 5; row < 8; row++)
            {
                for (int column = 0; column < BOARD_DIMENSION; column++)
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
                        SquareColour colour = SquareColour.Black;
                        Board[column, row] = new Square(colour, column, row);
                    }
                    else
                    {
                        SquareColour colour = SquareColour.Red;
                        Board[column, row] = new Square(colour, column, row);
                    }
                }
            }          
        }
        
    }
}
