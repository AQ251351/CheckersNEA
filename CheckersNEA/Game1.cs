using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CheckersNEA
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private CheckersGame checkersGame;
        private Texture2D _WhitePiece;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
        
            base.Initialize();

            checkersGame =  new CheckersGame();
            checkersGame.IntilizeGame();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _WhitePiece = Content.Load<Texture2D>("White_Checker_Piece");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            for (int row = 0; row < 8; row++) 
            {
                for (int column = 0; column < CheckersGame.BOARD_DIMENSION; column ++)
                {
                    Square square = checkersGame.Board[row, column];

                    // if there is a piece in the square 
                    if (square.Man != null )
                    {
                        int x = (row * 60) ;
                        int y = (column * 60);

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
