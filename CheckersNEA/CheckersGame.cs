using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.ComponentModel.DataAnnotations;
using static CheckersNEA.Square;

namespace CheckersNEA
{
    /************************************************************
     * Class: CheckersGame
     * 
     * This class represents the game of checkers.
     * It is derived from the Game class provided by the MonoGame framework.
     ***********************************************************/
    public class CheckersGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _texture;
        private BoardContainer checkersGame;

        private InputHandler handleinput;
        private Texture2D _lightSquare;
        private Texture2D _darkSquare;
        private Texture2D _lightPiece;
        private Texture2D _darkPiece;
        private Texture2D _highlightSquare;
       




        /*****************************************************
         * Constructor: CheckersGame
         * 
         * Default constructor for the CheckersGame class. 
         * 
         ********************************************************/

        public CheckersGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /*****************************************************
         * Method: Initialize
         * 
         * Monogame dedicated place for additional configuration and initializations.
         ********************************************************/
        protected override void Initialize()
        {
        
            

            checkersGame =  new BoardContainer();
            handleinput = new InputHandler();

            
           
            base.Initialize();
        }

        /*****************************************************
         * Method: LoadContent
         * 
         * The place for asset management. 
         * Here you can load textures, and other game assets
         ********************************************************/
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _texture = new Texture2D(GraphicsDevice,1,1);
            _texture.SetData(new[] { Color.White });

            _lightPiece = Content.Load<Texture2D>("LightPiece");
            _darkPiece = Content.Load<Texture2D>("DarkPiece");

        }

        /*****************************************************
         * Method: Update
         * 
         * This method is called every frame and is used to update the game state.
         * It handles input and game logic within monogame's game loop
         ********************************************************/
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Handle mouse input
            MouseState mouse = Mouse.GetState();

            Rectangle windowBounds = new Rectangle(0, 0 ,480, GraphicsDevice.Viewport.Height);
            if(windowBounds.Contains (mouse.X, mouse.Y))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {

                    handleinput.HandleMouseEvent(mouse);

                }
            }
            


            base.Update(gameTime);

        }

        

        /*****************************************************
         * Method: Draw
         * 
         * This method is called every frame and is used to draw the game state.
         * It handles rendering within monogame's game loop
         ********************************************************/
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            
            
            _spriteBatch.Begin();

            for (int row = 0; row < BoardContainer.BOARD_DIMENSION; row++) 
            {
                for (int column = 0; column < BoardContainer.BOARD_DIMENSION; column ++)
                {
                    
                    int y = (row * BoardContainer.SQUARE_SIZE);
                    int x = (column * BoardContainer.SQUARE_SIZE);

                    Square square = BoardContainer.Board[column, row];
                    
                    if (square.ColourOfSquare == SquareColour.Black )
                    {
                        _spriteBatch.Draw(_texture, new Rectangle(x, y, BoardContainer.SQUARE_SIZE, BoardContainer.SQUARE_SIZE), Color.Black);
                    }
                    else if (square.ColourOfSquare == SquareColour.Red)
                    {
                        _spriteBatch.Draw(_texture, new Rectangle(x, y, BoardContainer.SQUARE_SIZE, BoardContainer.SQUARE_SIZE), Color.Red);
                        
                    }
                    else if (square.ColourOfSquare == SquareColour.Yellow)
                    {
                        _spriteBatch.Draw(_texture, new Rectangle(x, y, BoardContainer.SQUARE_SIZE, BoardContainer.SQUARE_SIZE), Color.Yellow);
                    }
                    // if there is a piece in the square 
                    if (square.Man != null )
                    {              
                        // not dark piece means its white
                        if (!square.Man.IsDarkPiece)
                        {
                            Rectangle destination = new Rectangle(x,y,BoardContainer.SQUARE_SIZE,BoardContainer.SQUARE_SIZE);

                            _spriteBatch.Draw(_lightPiece, destination, Color.White);
                            //_spriteBatch.Draw(_WhitePiece, new Vector2(x, y), Color.White);
                        }
                        else
                        {
                            Rectangle destination = new Rectangle(x, y, BoardContainer.SQUARE_SIZE, BoardContainer.SQUARE_SIZE);

                            _spriteBatch.Draw(_darkPiece, destination, Color.White);
                        }
                    }
                        
                }
            }
                
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
