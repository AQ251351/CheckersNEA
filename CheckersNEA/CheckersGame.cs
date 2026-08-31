using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        private CheckersGameHelper checkersGame;

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
        
            base.Initialize();

            checkersGame =  new CheckersGameHelper();           
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

            handleMouseEvent(gameTime);
            
            base.Update(gameTime);
        }

        private void handleMouseEvent(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                int X_Coordinate = mouse.X / CheckersGameHelper.SQUARE_SIZE;
                int Y_Coordinate = mouse.Y / CheckersGameHelper.SQUARE_SIZE;
            }
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

            for (int row = 0; row < CheckersGameHelper.BOARD_DIMENSION; row++) 
            {
                for (int column = 0; column < CheckersGameHelper.BOARD_DIMENSION; column ++)
                {
                    
                    int x = (row * CheckersGameHelper.SQUARE_SIZE);
                    int y = (column * CheckersGameHelper.SQUARE_SIZE);

                    Square square = checkersGame.Board[row, column];

                    if (square.IsDarkSquare)
                    {
                        _spriteBatch.Draw(_texture, new Rectangle(x, y, CheckersGameHelper.SQUARE_SIZE, CheckersGameHelper.SQUARE_SIZE), Color.Black);
                    }
                    else 
                    {
                        _spriteBatch.Draw(_texture, new Rectangle(x, y, CheckersGameHelper.SQUARE_SIZE, CheckersGameHelper.SQUARE_SIZE), Color.Red);
                    }
                    // if there is a piece in the square 
                    if (square.Man != null )
                    {              
                        // not dark piece means its white
                        if (!square.Man.IsDarkPiece)
                        {
                            Rectangle destination = new Rectangle(x,y,CheckersGameHelper.SQUARE_SIZE,CheckersGameHelper.SQUARE_SIZE);

                            _spriteBatch.Draw(_lightPiece, destination, Color.White);
                            //_spriteBatch.Draw(_WhitePiece, new Vector2(x, y), Color.White);
                        }
                        else
                        {
                            Rectangle destination = new Rectangle(x, y, CheckersGameHelper.SQUARE_SIZE, CheckersGameHelper.SQUARE_SIZE);

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
