using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Part_5___Screens
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont titleText, instructText;

        Texture2D tribbleGreyTexture, tribbleCreamTexture, tribbleBrownTexture, closetTexture, tribbleIntroTexture;
        Rectangle tribbleGreyRect, tribbleCreamRect, tribbleBrownRect, BackroundRect;
        Vector2 tribbleGreySpeed, tribbleCreamSpeed, tribbleBrownSpeed, gravity;
        SoundEffect tribbleSound;
        Song outroMusic;
        SoundEffectInstance explodeInstance;
        MouseState mouseState;

        float seconds;
        float startTime;
        float tribbleTime = 10;
        enum Screen
        {
            Intro,
            TribbleYard,
            Outro
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
            // TODO: Add your initialization logic here
            screen = Screen.Intro;
            base.Initialize();
            // TODO: Add your initialization logic here
            Random rand = new Random();
            int randomX = rand.Next(0, 600);

            _graphics.PreferredBackBufferWidth = 800; // Sets the width of the window
            _graphics.PreferredBackBufferHeight = 600; // Sets the height of the window
            _graphics.ApplyChanges(); // Applies the new dimensions

            BackroundRect = new Rectangle(0, 0, 800, 600);

            tribbleGreyRect = new Rectangle(rand.Next(0, 600), rand.Next(0, 300), 120, 120);
            tribbleGreySpeed = new Vector2(2, 1);

            tribbleCreamRect = new Rectangle(rand.Next(0, 600), rand.Next(0, 300), 100, 100);
            tribbleCreamSpeed = new Vector2(-3, 1);

            tribbleBrownRect = new Rectangle(rand.Next(0, 600), rand.Next(0, 300), 80, 80);
            tribbleBrownSpeed = new Vector2(3, 1);

            gravity = new Vector2(0, 1);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            tribbleIntroTexture = Content.Load<Texture2D>("tribble_intro");
            tribbleGreyTexture = Content.Load<Texture2D>("tribbleGrey");
            tribbleCreamTexture = Content.Load<Texture2D>("tribbleCream");
            tribbleBrownTexture = Content.Load<Texture2D>("tribbleBrown");
            closetTexture = Content.Load<Texture2D>("closet Backround");
            tribbleSound = Content.Load<SoundEffect>("tribble_sound");
            titleText = Content.Load<SpriteFont>("File");
            instructText = Content.Load<SpriteFont>("File2");
            outroMusic = Content.Load<Song>("Outro-Music-Meme");
            explodeInstance = outroMusic.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here
            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    screen = Screen.TribbleYard;

            }
            else if (screen == Screen.TribbleYard)
            {
                seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
                if (seconds >= tribbleTime)
                    screen = Screen.Outro;

                tribbleGreySpeed.Y += (int)gravity.Y;
                tribbleCreamSpeed.Y += (int)gravity.Y;
                tribbleBrownSpeed.Y += (int)gravity.Y;

                tribbleGreyRect.X += (int)tribbleGreySpeed.X;
                tribbleGreyRect.Y += (int)tribbleGreySpeed.Y;

                tribbleCreamRect.X += (int)tribbleCreamSpeed.X;
                tribbleCreamRect.Y += (int)tribbleCreamSpeed.Y;

                tribbleBrownRect.X += (int)tribbleBrownSpeed.X;
                tribbleBrownRect.Y += (int)tribbleBrownSpeed.Y;


                if (tribbleGreyRect.X > 700 || tribbleGreyRect.X <= 0)
                    tribbleGreySpeed.X *= -1;


                if (tribbleGreyRect.Y > 480 || tribbleGreyRect.Y <= 0)
                {
                    tribbleGreySpeed.Y *= -1;
                    tribbleGreyRect.Y = 480;
                    tribbleSound.Play();
                }



                if (tribbleCreamRect.X > 700 || tribbleCreamRect.X <= 0)
                    tribbleCreamSpeed.X *= -1;
                if (tribbleCreamRect.Y > 500 || tribbleCreamRect.Y <= 0)
                {
                    tribbleCreamSpeed.Y *= -1;
                    tribbleCreamRect.Y = 500;
                    tribbleSound.Play();
                }


                if (tribbleBrownRect.X > 700 || tribbleBrownRect.X <= 0)
                    tribbleBrownSpeed.X *= -1;
                if (tribbleBrownRect.Y > 520 || tribbleBrownRect.Y <= 0)
                {
                    tribbleBrownSpeed.Y *= -1;
                    tribbleBrownRect.Y = 520;
                    tribbleSound.Play();
                }
                
            }
            else if (screen == Screen.Outro)
            {
                outroMusic.P
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                
                _spriteBatch.Draw(tribbleIntroTexture, new Rectangle(0, 0, 800, 500), Color.White);
                _spriteBatch.DrawString(titleText, "Welcome to the Tribble Tribe", new Vector2(120, 500), Color.Black);
                _spriteBatch.DrawString(instructText, $"You will have {tribbleTime} seconds with the tribbles", new Vector2(100, 550), Color.Black);
            }
            else if (screen == Screen.TribbleYard)
            {

                _spriteBatch.Draw(closetTexture, BackroundRect, Color.White);
                _spriteBatch.Draw(tribbleGreyTexture, tribbleGreyRect, Color.White);
                _spriteBatch.Draw(tribbleCreamTexture, tribbleCreamRect, Color.White);
                _spriteBatch.Draw(tribbleBrownTexture, tribbleBrownRect, Color.White);
                _spriteBatch.DrawString(instructText, $"Time Left: {(10 - seconds).ToString("00.0")}", new Vector2(50, 50), Color.White);
            }
            else if (screen == Screen.Outro)
            {
                _spriteBatch.DrawString(titleText, "See you next time!", new Vector2(100, 250), Color.Black);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}