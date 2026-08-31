using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        private Texture2D _WhitePiece;
        private Texture2D _whiteSquare;
        private Texture2D _blackSquare;
        private Texture2D _redPiece;
        private Texture2D _blackPiece;
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

            _WhitePiece = Content.Load<Texture2D>("White_Checker_Piece");
        }

        /*****************************************************
         * Method: Update
         * 
         * This method is called every frame and is used to update the game state.
         * It handles input and game logic within monogame's game loop
         ********************************************************/
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

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

            for (int row = 0; row < CheckersGameHelper.BOARD_DIMENSION; row++) 
            {
                for (int column = 0; column < CheckersGameHelper.BOARD_DIMENSION; column ++)
                {
                    
                    int x = (row * 60);
                    int y = (column * 60);

                    Square square = checkersGame.Board[row, column];

                    if (square.IsDarkSquare)
                    {
                        _spriteBatch.Draw(_texture, new Rectangle(x, y, 60, 60), Color.Black);
                    }
                    else 
                    {
                        _spriteBatch.Draw(_texture, new Rectangle(x, y, 60, 60), Color.Red);
                    }
                    // if there is a piece in the square 
                    if (square.Man != null )
                    {              
                        // not dark piece means its white
                        if (!square.Man.IsDarkPiece)
                        {
                            Rectangle destination = new Rectangle(x,y,60,60);

                            _spriteBatch.Draw(_WhitePiece, destination, Color.White);
                            //_spriteBatch.Draw(_WhitePiece, new Vector2(x, y), Color.White);
                        }
                    }
                        
                }
            }
                
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
