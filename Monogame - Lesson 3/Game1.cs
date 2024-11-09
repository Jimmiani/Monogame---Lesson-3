﻿using Microsoft.Xna.Framework;
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
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
            this.Window.Title = "Tribbles all over the place!";
            // Brown tribble
            brownTribbleSpeed = new Vector2(3, 2);
            brownTribbleRect = new Rectangle(generator.Next(800), generator.Next(600), tribSize, tribSize);

            // Cream tribble
            creamTribbleSpeed = new Vector2(6, 2);
            creamTribbleRect = new Rectangle(generator.Next(800), generator.Next(600), tribSize, tribSize);

            // Orange tribble
            orangeTribbleSpeed = new Vector2(4, 2);
            orangeTribbleRect = new Rectangle(generator.Next(800), generator.Next(600), tribSize, tribSize);

            // Grey tribble
            for (int i = 0; i < 20; i++)
            {
                speeds.Add(i);
            }
            greyTribbleSpeed = new Vector2(2, 2);
            greyTribbleRect = new Rectangle(generator.Next(800), generator.Next(600), tribSize, tribSize);


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
            if (brownTribbleRect.Left < 0 || brownTribbleRect.Right > _graphics.PreferredBackBufferWidth)
            {
                brownTribbleSpeed.X *= -1;
            }
            if (brownTribbleRect.Top < 0 || brownTribbleRect.Bottom > _graphics.PreferredBackBufferHeight)
            {
                brownTribbleSpeed.Y *= -1;
            }

            // Cream tribble
            creamTribbleRect.X += (int)creamTribbleSpeed.X;
            creamTribbleRect.Y += (int)creamTribbleSpeed.Y;
            if (creamTribbleRect.Left < 0 || creamTribbleRect.Right > _graphics.PreferredBackBufferWidth)
            {
                creamTribbleRect = new Rectangle(generator.Next(800), generator.Next(600), tribSize, tribSize);
                creamTribbleSpeed.X *= -1;
            }
            if (creamTribbleRect.Top < 0 || creamTribbleRect.Bottom > _graphics.PreferredBackBufferHeight)
            {
                creamTribbleRect = new Rectangle(generator.Next(800), generator.Next(600), tribSize, tribSize);
                creamTribbleSpeed.Y *= -1;
            }

            // Orange tribble
            orangeTribbleRect.X += (int)orangeTribbleSpeed.X;
            orangeTribbleRect.Y += (int)orangeTribbleSpeed.Y;
            if (orangeTribbleRect.Right < 0)
            {
                orangeTribbleRect = new Rectangle(_graphics.PreferredBackBufferWidth - tribSize, orangeTribbleRect.Y, tribSize, tribSize);
            }
            if (orangeTribbleRect.Left > _graphics.PreferredBackBufferWidth)
            {
                orangeTribbleRect = new Rectangle(-tribSize, orangeTribbleRect.Y, tribSize, tribSize);
            }
            if (orangeTribbleRect.Bottom < 0)
            {
                orangeTribbleRect = new Rectangle(orangeTribbleRect.X, _graphics.PreferredBackBufferHeight - tribSize, tribSize, tribSize);
            }
            if (orangeTribbleRect.Top > _graphics.PreferredBackBufferHeight)
            {
                orangeTribbleRect = new Rectangle(orangeTribbleRect.X, -tribSize, tribSize, tribSize);
            }

            // Grey tribble
            greyTribbleRect.X += (int)greyTribbleSpeed.X;
            greyTribbleRect.Y += (int)greyTribbleSpeed.Y;
            if (greyTribbleRect.Left < 0 || greyTribbleRect.Right > _graphics.PreferredBackBufferWidth)
            {
                if (greyTribbleRect.X < 0)
                    greyTribbleSpeed.X = speeds[generator.Next(speeds.Count)];
                else
                    greyTribbleSpeed.X = -1 * speeds[generator.Next(speeds.Count)];
            }
                
            if (greyTribbleRect.Bottom > _graphics.PreferredBackBufferHeight || greyTribbleRect.Top < 0)
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
            _spriteBatch.Draw(creamTribbleTexture, creamTribbleRect, Color.White);
            _spriteBatch.Draw(brownTribbleTexture, brownTribbleRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}