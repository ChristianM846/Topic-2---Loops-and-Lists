using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Topic_2___Loops_and_Lists
{
    //Christian
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Random generator = new Random();

        float seconds;
        float respawnTime;

        Texture2D spaceBackgroundTexture;
        Rectangle windowRect;

        List<Texture2D> textures;
        List<Texture2D> planetTextures;
        List<Rectangle> planetRects;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            seconds = 0f;
            respawnTime = 3f;


            windowRect = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            textures = new List<Texture2D>();
            planetTextures = new List<Texture2D>();
            planetRects = new List<Rectangle>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spaceBackgroundTexture = Content.Load<Texture2D>("Images/space_background");

            for (int i = 1; i <= 13; i++)
            {
                textures.Add(Content.Load<Texture2D>("Images/16-bit-planet" + i));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (seconds > respawnTime)
            {
                planetTextures.Add(textures[generator.Next(textures.Count)]);
                planetRects.Add(new Rectangle(generator.Next(windowRect.Width - 25), generator.Next(windowRect.Height - 25), generator.Next(25, 76), generator.Next(25, 76)));
                seconds = 0f;
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(spaceBackgroundTexture, windowRect, Color.White);

            for (int i = 0; i < planetRects.Count; i++)
            {
                _spriteBatch.Draw(planetTextures[i], planetRects[i], Color.White);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
