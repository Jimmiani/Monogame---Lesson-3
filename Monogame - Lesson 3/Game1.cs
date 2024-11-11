using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Monogame___Lesson_3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window;
        Random generator = new Random();
        List<int> speeds = new List<int>();

        Texture2D enterpriseTexture, greyTribbleTexture, orangeTribbleTexture, creamTribbleTexture, brownTribbleTexture;
        Vector2 greyTribbleSpeed, orangeTribbleSpeed, creamTribbleSpeed, brownTribbleSpeed;
        Rectangle greyTribbleRect, orangeTribbleRect, creamTribbleRect, brownTribbleRect;
        int tribSize = 100;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            window = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            this.Window.Title = "Tribbles all over the place!";
            // Brown tribble
            brownTribbleSpeed = new Vector2(7, 6);
            brownTribbleRect = new Rectangle(generator.Next(window.Width - tribSize), generator.Next(window.Height - tribSize), tribSize, tribSize);

            // Cream tribble
            creamTribbleSpeed = new Vector2(6, 2);
            creamTribbleRect = new Rectangle(generator.Next(window.Width), generator.Next(window.Height), tribSize, tribSize);

            // Orange tribble
            orangeTribbleSpeed = new Vector2(4, 0);
            orangeTribbleRect = new Rectangle(window.Width / 2, (window.Height / 2) - tribSize / 2, tribSize, tribSize);

            // Grey tribble
            for (int i = 0; i < 20; i++)
            {
                speeds.Add(i);
            }
            greyTribbleSpeed = new Vector2(2, 2);
            greyTribbleRect = new Rectangle(generator.Next(window.Width), generator.Next(window.Height), tribSize, tribSize);


            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            enterpriseTexture = Content.Load<Texture2D>("enterprise");
            greyTribbleTexture = Content.Load<Texture2D>("tribbleGrey");
            orangeTribbleTexture = Content.Load<Texture2D>("tribbleOrange");
            creamTribbleTexture = Content.Load<Texture2D>("tribbleCream");
            brownTribbleTexture = Content.Load<Texture2D>("tribbleBrown");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Brown tribble
            brownTribbleRect.X += (int)brownTribbleSpeed.X;
            brownTribbleRect.Y += (int)brownTribbleSpeed.Y;
            
            if (brownTribbleRect.Right > window.Width)
            {
                brownTribbleSpeed.X *= -1;
                brownTribbleRect.Width = 150;
                brownTribbleRect.Height = 150;
                brownTribbleRect.X = window.Width - brownTribbleRect.Width;
            }
            if (brownTribbleRect.Left < 0)
            {
                brownTribbleSpeed.X *= -1;
                brownTribbleRect.Width = 150;
                brownTribbleRect.Height = 150;
            }
            if (brownTribbleRect.Top < 0 || brownTribbleRect.Bottom > window.Height)
            {
                brownTribbleSpeed.Y *= -1;
                brownTribbleRect.Width = 100;
                brownTribbleRect.Height = 100;
            }

            // Cream tribble
            creamTribbleRect.X += (int)creamTribbleSpeed.X;
            creamTribbleRect.Y += (int)creamTribbleSpeed.Y;
            if (creamTribbleRect.Left < 0 || creamTribbleRect.Right > window.Width)
            {
                creamTribbleRect = new Rectangle(generator.Next(window.Width), generator.Next(window.Height), tribSize, tribSize);
                creamTribbleSpeed.X *= -1;
            }
            if (creamTribbleRect.Top < 0 || creamTribbleRect.Bottom > window.Height)
            {
                creamTribbleRect = new Rectangle(generator.Next(window.Width), generator.Next(window.Height), tribSize, tribSize);
                creamTribbleSpeed.Y *= -1;
            }

            // Orange tribble
            orangeTribbleRect.X += (int)orangeTribbleSpeed.X;
            orangeTribbleRect.Y += (int)orangeTribbleSpeed.Y;
            if (orangeTribbleRect.Right < 0)
            {
                orangeTribbleRect = new Rectangle(window.Width - tribSize, orangeTribbleRect.Y, tribSize, tribSize);
            }
            if (orangeTribbleRect.Left > window.Width)
            {
                orangeTribbleRect = new Rectangle(-tribSize, orangeTribbleRect.Y, tribSize, tribSize);
            }
            if (orangeTribbleRect.Bottom < 0)
            {
                orangeTribbleRect = new Rectangle(orangeTribbleRect.X, window.Height - tribSize, tribSize, tribSize);
            }
            if (orangeTribbleRect.Top > window.Height)
            {
                orangeTribbleRect = new Rectangle(orangeTribbleRect.X, -tribSize, tribSize, tribSize);
            }

            // Grey tribble
            greyTribbleRect.X += (int)greyTribbleSpeed.X;
            greyTribbleRect.Y += (int)greyTribbleSpeed.Y;
            if (greyTribbleRect.Left < 0 || greyTribbleRect.Right > window.Width)
            {
                if (greyTribbleRect.X < 0)
                    greyTribbleSpeed.X = speeds[generator.Next(speeds.Count)];
                else
                    greyTribbleSpeed.X = -1 * speeds[generator.Next(speeds.Count)];
            }
                
            if (greyTribbleRect.Bottom > window.Height || greyTribbleRect.Top < 0)
            {
                if (greyTribbleRect.Y < 0)
                    greyTribbleSpeed.Y = speeds[generator.Next(speeds.Count)];
                else
                    greyTribbleSpeed.Y = -1 * speeds[generator.Next(speeds.Count)];
            }
                

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            _spriteBatch.Begin();

            _spriteBatch.Draw(enterpriseTexture, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(greyTribbleTexture, greyTribbleRect, Color.White);
            _spriteBatch.Draw(orangeTribbleTexture, orangeTribbleRect, Color.White);

            if (orangeTribbleRect.Right > window.Width)
            {
                _spriteBatch.Draw(orangeTribbleTexture, new Rectangle(0, orangeTribbleRect.Y, tribSize, tribSize), Color.White);
            }
            _spriteBatch.Draw(creamTribbleTexture, creamTribbleRect, Color.White);
            _spriteBatch.Draw(brownTribbleTexture, brownTribbleRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
