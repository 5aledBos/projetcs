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

        private KeyboardState _keyboardState;
        private KeyboardState _prevKeyboardState;

        private int _screenWidth;
        private int _screenHeight;
        private int _wing;
        private int _limiteSup;
        private int _limiteInf;

        private float _time;
        private float _timeOrigin;
        private float _pos0;
        private float _timeGap;
        private float _transitionSpeed;
        private float _acceleration;

        private bool _jump;

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
            Random rnd = new Random();
            _obstPos1.X -= _transitionSpeed;
            _obstPos2.X -= _transitionSpeed;
            if (_obstPos1.X < -100)
            {
                _obstPos1 = new Vector2 (_screenWidth, rnd.Next(_limiteSup, _limiteInf));
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
            _keyboardState = Keyboard.GetState();

            

            if (_prevKeyboardState.IsKeyUp(Keys.Space) && _keyboardState.IsKeyDown(Keys.Space))
            {
                _timeOrigin = _time + 0.3f;
                _timeGap = _time - _timeOrigin;
                _pos0 = _position.Y - _timeGap * _timeGap * _acceleration;
                

            }
            else
            {
                _timeGap = _time - _timeOrigin;
                
            }
            _transitionSpeed += 0.001f;
            _prevKeyboardState = _keyboardState;
            if (_obstPos1.X <130 && _obstPos1.X>-70)
            {
                if (_position.Y < _obstPos1.Y + 608 || _position.Y > _obstPos1.Y + 738)
                {
                    _timeGap = 0;
                    _wing--;
                    _pos0 = _position.Y;
                    _transitionSpeed = 0;
                }
            }

            if (_obstPos2.X < 130 && _obstPos2.X > -70)
            {
                if (_position.Y < _obstPos2.Y + 608 || _position.Y > _obstPos2.Y + 738)
                {
                    _timeGap = 0;
                    _bird = Content.Load<Texture2D>("bird1");
                    _pos0 = _position.Y;
                    _transitionSpeed = 0;
                }
            }

            _position.Y = _timeGap * _timeGap * _acceleration + _pos0;
            // TODO: Add your update logic here
            
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
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
