using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;

namespace Monogame___Lesson_3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseState mouseState, prevMouseState;
        Rectangle window;
        Random generator = new Random();
        List<int> speeds = new List<int>();

        Song trekSong;
        SpriteFont textFont;
        Texture2D enterpriseTexture, greyTribbleTexture, orangeTribbleTexture, creamTribbleTexture, brownTribbleTexture, introScreenTexture, playBtnTexture, continueBtnTexture;
        Texture2D endScreenTexture, endTextTexture, menuTexture, optionsBtnTexture, musicBtnTexture, instructionsBtnTexture;
        Vector2 greyTribbleSpeed, orangeTribbleSpeed, creamTribbleSpeed, brownTribbleSpeed, orangeTribbleSpeed2;
        Rectangle greyTribbleRect, orangeTribbleRect, creamTribbleRect, brownTribbleRect, orangeTribbleRect2, playBtnRect, continueBtnRect, menuRect, optionsBtnRect, instructionsBtnRect, musicBtnRect;
        int tribSize, collisions;
        enum Screen
        {
            Intro,
            Options,
            Rules,
            TribbleYard,
            End
                
        }
        Screen screen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            screen = Screen.Intro;
            window = new Rectangle(0, 0, 800, 500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            tribSize = 100;
            this.Window.Title = "Tribbles all over the place!";
            trekSong = Content.Load<Song>("trekSong");
            // Music
            MediaPlayer.Play(trekSong);

            // Start Button
            playBtnRect = new Rectangle((window.Width - 250) / 2, (window.Height - 114) / 2, 250, 107);

            // Continue Button
            continueBtnRect = new Rectangle(640, 450, 150, 43);

            // Menu
            menuRect = new Rectangle((window.Width - 700) / 2, (window.Height - 288) / 2, 700, 288);

            // Options Button
            optionsBtnRect = new Rectangle(20, 415, 200, 65);

            // Brown tribble
            brownTribbleSpeed = new Vector2(4, 4);
            brownTribbleRect = new Rectangle(generator.Next(window.Width - tribSize), generator.Next(window.Height - tribSize), tribSize, tribSize);

            // Cream tribble
            creamTribbleSpeed = new Vector2(6, 2);
            creamTribbleRect = new Rectangle(generator.Next(window.Width), generator.Next(window.Height), tribSize, tribSize);

            // Orange tribble
            orangeTribbleSpeed = new Vector2(4, 0);
            orangeTribbleRect = new Rectangle(window.Width / 2, (window.Height / 2) - tribSize / 2, tribSize, tribSize);
            orangeTribbleSpeed2 = new Vector2(4, 0);
            orangeTribbleRect2 = new Rectangle(-orangeTribbleRect.Width, orangeTribbleRect.Y, tribSize, tribSize);

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
            endScreenTexture = Content.Load<Texture2D>("endScreen");
            greyTribbleTexture = Content.Load<Texture2D>("tribbleGrey");
            orangeTribbleTexture = Content.Load<Texture2D>("tribbleOrange");
            creamTribbleTexture = Content.Load<Texture2D>("tribbleCream");
            brownTribbleTexture = Content.Load<Texture2D>("tribbleBrown");
            introScreenTexture = Content.Load<Texture2D>("tribble_intro");
            playBtnTexture = Content.Load<Texture2D>("playBtn");
            continueBtnTexture = Content.Load<Texture2D>("continueBtn");
            textFont = Content.Load<SpriteFont>("textFont");
            endTextTexture = Content.Load<Texture2D>("endText");
            menuTexture = Content.Load<Texture2D>("menuBtn");
            optionsBtnTexture = Content.Load<Texture2D>("optionsBtn");
            musicBtnTexture = Content.Load<Texture2D>("musicBtn");
            instructionsBtnTexture = Content.Load<Texture2D>("instructionsBtn");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (screen == Screen.Intro)
            {
                
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (playBtnRect.Contains(mouseState.Position))
                    {
                        // Start Button
                        screen = Screen.TribbleYard;
                    }
                    if (optionsBtnRect.Contains(mouseState.Position))
                    {
                        // Options Button
                        screen = Screen.Options;
                    }
                }
            }

            else if (screen == Screen.Options)
            {

            }

            else if (screen == Screen.Rules)
            {

            }

            else if (screen == Screen.TribbleYard)
            {
                // Continue button
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (continueBtnRect.Contains(mouseState.Position))
                    {
                        screen = Screen.End;
                    }
                }

                // Brown tribble
                brownTribbleRect.X += (int)brownTribbleSpeed.X;
                brownTribbleRect.Y += (int)brownTribbleSpeed.Y;

                if (brownTribbleRect.Right > window.Width)
                {
                    brownTribbleSpeed.X *= -1;
                    brownTribbleRect.Width *= 2;
                    brownTribbleRect.Height *= 2;
                    brownTribbleRect.X = window.Width - brownTribbleRect.Width;
                    collisions++;
                }
                if (brownTribbleRect.Left < 0)
                {
                    brownTribbleSpeed.X *= -1;
                    brownTribbleRect.Width *= 2;
                    brownTribbleRect.Height *= 2;
                    collisions++;
                }
                if (brownTribbleRect.Top < 0 || brownTribbleRect.Bottom > window.Height)
                {
                    brownTribbleSpeed.Y *= -1;
                    brownTribbleRect.Width = tribSize;
                    brownTribbleRect.Height = tribSize;
                    collisions++;
                }

                // Cream tribble
                creamTribbleRect.X += (int)creamTribbleSpeed.X;
                creamTribbleRect.Y += (int)creamTribbleSpeed.Y;
                if (creamTribbleRect.Left < 0 || creamTribbleRect.Right > window.Width)
                {
                    creamTribbleRect = new Rectangle(generator.Next(window.Width), generator.Next(window.Height), tribSize, tribSize);
                    creamTribbleSpeed.X *= -1;
                    collisions++;
                }
                if (creamTribbleRect.Top < 0 || creamTribbleRect.Bottom > window.Height)
                {
                    creamTribbleRect = new Rectangle(generator.Next(window.Width), generator.Next(window.Height), tribSize, tribSize);
                    creamTribbleSpeed.Y *= -1;
                    collisions++;
                }

                // Orange tribble
                orangeTribbleRect.X += (int)orangeTribbleSpeed.X;
                orangeTribbleRect.Y += (int)orangeTribbleSpeed.Y;
                orangeTribbleRect2.X += (int)orangeTribbleSpeed2.X;
                orangeTribbleRect2.Y += (int)orangeTribbleSpeed2.Y;
                if (orangeTribbleRect.Right > window.Width)
                {
                    orangeTribbleSpeed2.X = orangeTribbleSpeed.X;
                    collisions++;
                }

                if (orangeTribbleRect.Left > window.Width)
                {
                    orangeTribbleRect = orangeTribbleRect2;
                }
                if (orangeTribbleRect2.Left > 0)
                {
                    orangeTribbleSpeed2.X = 0;
                    orangeTribbleRect2.X = -orangeTribbleRect2.Width;
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
                    collisions++;
                }

                if (greyTribbleRect.Bottom > window.Height || greyTribbleRect.Top < 0)
                {
                    if (greyTribbleRect.Y < 0)
                        greyTribbleSpeed.Y = speeds[generator.Next(speeds.Count)];
                    else
                        greyTribbleSpeed.Y = -1 * speeds[generator.Next(speeds.Count)];
                    collisions++;
                }
            }
            else if (screen == Screen.End)
            {

            }   

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introScreenTexture, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.Draw(playBtnTexture, playBtnRect, Color.White);
                _spriteBatch.Draw(optionsBtnTexture, optionsBtnRect, Color.White);
            }
            else if (screen == Screen.Options)
            {
                _spriteBatch.Draw(menuTexture, menuRect, Color.White);
            }
            else if (screen == Screen.TribbleYard)
            {
                _spriteBatch.Draw(enterpriseTexture, new Vector2(0, -50), Color.White);
                _spriteBatch.Draw(greyTribbleTexture, greyTribbleRect, Color.White);
                _spriteBatch.Draw(orangeTribbleTexture, orangeTribbleRect, Color.White);

                if (orangeTribbleRect.Right > window.Width)
                {
                    _spriteBatch.Draw(orangeTribbleTexture, orangeTribbleRect2, Color.White);
                }

                _spriteBatch.Draw(creamTribbleTexture, creamTribbleRect, Color.White);
                _spriteBatch.Draw(brownTribbleTexture, brownTribbleRect, Color.White);
                _spriteBatch.Draw(continueBtnTexture, continueBtnRect, Color.White);
            }
            else if (screen == Screen.End)
            {
                _spriteBatch.Draw(endScreenTexture, new Vector2(0, 0), Color.White);
                _spriteBatch.Draw(endTextTexture, new Rectangle(105, 180, 590, 60), Color.White);
                _spriteBatch.DrawString(textFont, "Congratulations! You got rid of " + collisions + " tribbles!", new Vector2(130, 190), Color.Black);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
