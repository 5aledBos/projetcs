﻿using Microsoft.Xna.Framework;
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

        private SpriteFont _font;

        private KeyboardState _keyboardState;
        private KeyboardState _prevKeyboardState;

        private int _screenWidth;
        private int _screenHeight;

        private float _time;
        private float _timeOrigin;
        private float _pos0;
        private float _timeGap;
        private float _transitionSpeed;
        private float _acceleration;
        private float _speed;
        private float _speed0;

        private bool _jump;

        private Vector2 _position;
        private Vector2 _prevPosition;
        
        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 800;
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
            _speed0 = 0;
            _jump = false;
            _position = new Vector2(50, 210);
            _prevPosition = _position;
            _timeOrigin = 0.0f;
            _time = 0.0f;
            _pos0 = 0.0f;
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!_jump && (int)(_time*100)%5 == 0)
            {
                _bird = Content.Load<Texture2D>("bird1");
                _jump = !_jump;
            }
            else if ((int)(_time * 100)%5 == 0)
            {
                _bird = Content.Load<Texture2D>("bird2");
                _jump = !_jump;
            }
                
            _acceleration = -5.0f;
            _keyboardState = Keyboard.GetState();
            _speed0 = (float)(_position.Y - _prevPosition.Y);

            if (_prevKeyboardState.IsKeyUp(Keys.Space) && _keyboardState.IsKeyDown(Keys.Space))
            {
                _timeOrigin = _time + 0.2f;
                _timeGap = _time - _timeOrigin;
                _pos0 = _position.Y - _timeGap * _timeGap * 800;
                 
            }
            _timeGap = _time - _timeOrigin;

            _position.Y = _timeGap * _timeGap * 800 + _pos0 ;
            _prevKeyboardState = _keyboardState;

            _prevPosition = _position;
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
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
