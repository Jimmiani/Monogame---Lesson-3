using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame___Lesson_3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D tribbleGreyTexture;
        Vector2 greyTribbleSpeed;
        Rectangle greyTribbleRect;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            this.Window.Title = "Animation!";
            greyTribbleSpeed = new Vector2(2, 2);
            greyTribbleRect = new Rectangle(300, 10, 100, 100);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            tribbleGreyTexture = Content.Load<Texture2D>("tribbleGrey");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            greyTribbleRect.X += (int)greyTribbleSpeed.X;
            greyTribbleRect.Y += (int)greyTribbleSpeed.Y;
            if (greyTribbleRect.Left < 0 || greyTribbleRect.Right > _graphics.PreferredBackBufferWidth)
                greyTribbleSpeed.X *= -1;
            if (greyTribbleRect.Bottom > _graphics.PreferredBackBufferHeight || greyTribbleRect.Top < 0)
                greyTribbleSpeed.Y *= -1;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            _spriteBatch.Begin();

            _spriteBatch.Draw(tribbleGreyTexture, greyTribbleRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
