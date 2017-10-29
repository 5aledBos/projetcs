using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Flappy
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    
    

    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D _bird;
        private Texture2D _obstacle1;
        private Texture2D _obstacle2;

        private SpriteFont _font;
        private SpriteFont _fontGameOver;
        private SpriteFont _fontPlay;

        private KeyboardState _keyboardState;
        private KeyboardState _prevKeyboardState;

        private int _screenWidth;
        private int _screenHeight;
        private int _wing;
        private int _limiteSup;
        private int _limiteInf;
        private int _score;

        private float _time;
        private float _timeOrigin;
        private float _pos0;
        private float _timeGap;
        private float _transitionSpeed;
        private float _speed;
        private float _acceleration;

        private bool _jump;
        private bool _gameOver;
        private bool _isPaused;

        private Vector2 _position;
        private Vector2 _obstPos1;
        private Vector2 _obstPos2;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 650;
            graphics.PreferredBackBufferWidth = 1100;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _screenWidth = Window.ClientBounds.Width;
            _screenHeight = Window.ClientBounds.Height;
            Random rnd = new Random();
            _limiteSup = -590;
            _limiteInf = -158;

            _jump = false;
            _position = new Vector2(50, 210);
            _obstPos1 = new Vector2(_screenWidth, rnd.Next(_limiteSup, _limiteInf));
            _obstPos2 = new Vector2(_screenWidth + 600, rnd.Next(_limiteSup, _limiteInf));
         
            _timeOrigin = 0.0f;
            _time = 0.0f;
            _pos0 = 0.0f;
            _acceleration = 800;
            _wing = 0;
            _transitionSpeed = 5;
            _speed = 0.3f;
            _score = 24;
            _isPaused = true;
            _gameOver = false;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Score");
            _fontGameOver = Content.Load<SpriteFont>("File");
            _fontPlay = Content.Load<SpriteFont>("play");
            _bird = Content.Load<Texture2D>("bird1");
            _obstacle1 = Content.Load<Texture2D>("obstacle");
            _obstacle2 = Content.Load<Texture2D>("obstacle");
            //_font = Content.Load<SpriteFont>("Police");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyUp(Keys.P) && _prevKeyboardState.IsKeyDown(Keys.P))
            {
                if (_gameOver)
                    this.Initialize();
                else
                {
                    _isPaused = !_isPaused;
                }
            }
            if (_gameOver)
            {
                _position.Y += 20;
            }else if (!_isPaused)
            {
                Random rnd = new Random();
                _obstPos1.X -= _transitionSpeed;
                _obstPos2.X -= _transitionSpeed;
                if (_obstPos1.X < -100)
                {
                    _obstPos1 = new Vector2(_screenWidth, rnd.Next(_limiteSup, _limiteInf));
                }
                if (_obstPos2.X < -100)
                {
                    _obstPos2 = new Vector2(_screenWidth, rnd.Next(_limiteSup, _limiteInf));
                }

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_wing == 0)
                {
                    if (_jump)
                    {
                        _bird = Content.Load<Texture2D>("bird1");
                        _jump = !_jump;
                    }
                    else
                    {
                        _bird = Content.Load<Texture2D>("bird2");
                        _jump = !_jump;
                    }
                }
                _wing++;
                _wing = _wing % 5;
                //_acceleration = -5.0f;
                
                

                if (_prevKeyboardState.IsKeyUp(Keys.Space) && _keyboardState.IsKeyDown(Keys.Space))
                {
                    _timeOrigin = _time + _speed;
                    _timeGap = _time - _timeOrigin;
                    _pos0 = _position.Y - _timeGap * _timeGap * _acceleration;


                }
                else
                {
                    _timeGap = _time - _timeOrigin;

                }
                _transitionSpeed += 0.005f;
                _acceleration += 0.5f;
                
                if (_obstPos1.X < 130 && _obstPos1.X > -70 || _position.Y < 0 || _position.Y > _screenHeight - 50)
                {
                    if (_position.Y < _obstPos1.Y + 608 || _position.Y > _obstPos1.Y + 738)
                    {
                        _timeGap = 0;
                        _bird = Content.Load<Texture2D>("bird1");
                        _pos0 = _position.Y;
                        _transitionSpeed = 0;
                        _gameOver = true;
                    }
                }

                if (_obstPos2.X < 130 && _obstPos2.X > -70)
                {
                    if (_position.Y < _obstPos2.Y + 608 || _position.Y > _obstPos2.Y + 738)
                    {
                        _timeGap = 0;
                        _bird = Content.Load<Texture2D>("bird1");
                        _pos0 =_position.Y;
                        _transitionSpeed = 0;
                        _gameOver = true;
                    }
                }


                if (_obstPos1.X + _transitionSpeed > -50 && _obstPos1.X < -50)
                    _score++;
                if (_obstPos2.X +_transitionSpeed > -50 && _obstPos2.X < -50)
                    _score++;


                _position.Y = _timeGap * _timeGap * _acceleration + _pos0;
                // TODO: Add your update logic here
            }
            _prevKeyboardState = _keyboardState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(_bird, _position);
            spriteBatch.Draw(_obstacle1, _obstPos1);
            spriteBatch.Draw(_obstacle2, _obstPos2);
            if (_gameOver)
            {
                spriteBatch.DrawString(_fontGameOver, "Game Over", new Vector2((_screenWidth - _fontGameOver.MeasureString("Game Over").X) / 2, 50), Color.Black);
                spriteBatch.DrawString(_font, "Score: " + _score.ToString(), new Vector2((_screenWidth - _font.MeasureString("Score: " + _score.ToString()).X) / 2, 200), Color.Black);

            }
            else if (_isPaused)
                spriteBatch.DrawString(_fontPlay, "Press P to play", new Vector2((_screenWidth - _fontPlay.MeasureString("Press P to play").X) / 2, 50), Color.Black);
            else
                spriteBatch.DrawString(_font, "Score: " + _score.ToString(), new Vector2((_screenWidth - _font.MeasureString("Score: " + _score.ToString()).X) / 2, 50), Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
