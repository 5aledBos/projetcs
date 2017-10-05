using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Snake
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        private Texture2D _background;
        private Texture2D gameOverImg;

        GraphicsDeviceManager graphics;
        
        SpriteBatch spriteBatch;

        private bool isPaused = true;
        private bool pauseRequest = false;
        private bool gameOver = false;
        private bool winner = false;
        private bool isStarted;
       
        private KeyboardState _keyboardState;
        private KeyboardState _oldKey;
        
        private Keys _previousKey;
        private Keys _previousMove;

        private Box _box;

        private Menu _menu;

        private Food _food;
        
        private Dot _head;
        private Dot _dot2;
        private Dot _tail;

        private int _score = 0;
        private int _screenWidth;
        private int _screenHeight;
        private int _speed = 0;
        private int _posX;
        private int _posY;
        private int _menuCursor;
        private int _speedFactor;
        private int _levelChoice;
                
        private Vector2 _position1;
        private Vector2 _position2;
        private Vector2 _position3;
        private Vector2 _foodPos;
        private Vector2 _prevPos1;
        private Vector2 _prevPos2;

        private List<Dot> _snake;
        private List<Keys> _moves = new List<Keys> {};
        private List<int> _menuParams = new List<int> { };

        private const int pas = 20;

        List<Vector2> _borders = new List<Vector2> { };

        private SpriteFont _font;
        private SpriteFont _scorePolice;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 12*pas;
            graphics.PreferredBackBufferWidth = 14*pas;
            
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
            _screenHeight = Window.ClientBounds.Height - 40;
            _menu = new Menu();

            _menuCursor = 0;
            _speedFactor = 15;
            _levelChoice = 0;
            isStarted = false;
            

            _borders = new List<Vector2> { };
            
            _position3 = new Vector2(0, 100);
            _position2 = new Vector2(pas, 100);
            _position1 = new Vector2(2 * pas,100);
            _foodPos = new Vector2((int)_screenWidth / 2, (int)_screenHeight / 2);

            _food = new Food(_screenWidth, _screenHeight);
            _head = new Dot(_screenWidth, _screenHeight);
            _dot2 = new Dot(_screenWidth, _screenHeight);
            _tail = new Dot(_screenWidth, _screenHeight);

            _snake = new List<Dot>{_head, _dot2, _tail};
            _previousMove = Keys.Right;
            _moves.Add(_previousMove);
            _moves.Add(_previousMove);
            _moves.Add(_previousMove);

            _box = new Box(_screenWidth, _screenHeight);
            _score = 0;
            gameOver = false;
            isPaused = true;
            pauseRequest = false;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _menu.LoadContent(Content);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _background = Content.Load<Texture2D>("Background");
            gameOverImg = Content.Load<Texture2D>("GameOver");
            _head.LoadContent(Content, "headR");
            _dot2.LoadContent(Content, "bodyH");
            _tail.LoadContent(Content, "tailR");
            _food.LoadContent(Content, "food");
            _box.LoadContent(Content);
            
            _food.Position = _foodPos;
            _head.Position = _position1;
            _dot2.Position = _position2;
            _tail.Position = _position3;

            _font = Content.Load<SpriteFont>("Police");
            _scorePolice = Content.Load<SpriteFont>("score");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
                        
            _keyboardState = Keyboard.GetState();
            
            if (_keyboardState.IsKeyDown(Keys.Q))
                this.Exit();

            if (_keyboardState.GetPressedKeys().Length > 0)
            {
                _previousKey = _keyboardState.GetPressedKeys()[0];
            }
            
            if (_keyboardState.IsKeyUp(Keys.R) && _previousKey == Keys.R)
                this.Initialize();

            if (!isStarted)
            {
                if (_keyboardState.IsKeyUp(Keys.Down) && _oldKey.IsKeyDown(Keys.Down))
                {
                    switch (_menuCursor)
                    {
                        case 0:
                            _menuCursor = 1;
                            break;
                        case 1:
                            _menuCursor = 2;
                            break;
                        case 2:
                            _menuCursor = 0;
                            break;
                    }
                }
                if (_keyboardState.IsKeyUp(Keys.Up) && _oldKey.IsKeyDown(Keys.Up))
                {
                    switch (_menuCursor)
                    {
                        case 0:
                            _menuCursor = 2;
                            break;
                        case 1:
                            _menuCursor = 0;
                            break;
                        case 2:
                            _menuCursor = 1;
                            break;
                    }
                }

                switch (_menuCursor)
                {
                    case 0:
                        if (_keyboardState.IsKeyUp(Keys.Right) && _oldKey.IsKeyDown(Keys.Right))
                        {
                            if (_speedFactor == 15)
                                _speedFactor = 7;
                            else if (_speedFactor == 7)
                                _speedFactor = 3;
                            else if (_speedFactor == 3)
                                _speedFactor = 15;
                        }

                        if (_keyboardState.IsKeyUp(Keys.Left) && _oldKey.IsKeyDown(Keys.Left))
                        {
                            if (_speedFactor == 15)
                                _speedFactor = 3;
                            else if (_speedFactor == 7)
                                _speedFactor = 15;
                            else if (_speedFactor == 3)
                                _speedFactor = 7;
                        }
                        break;

                    case 1:
                        if (_keyboardState.IsKeyUp(Keys.Right) && _oldKey.IsKeyDown(Keys.Right))
                        {
                            switch (_levelChoice)
                            {
                                case 0:
                                    _levelChoice = 1;
                                    break;
                                case 1:
                                    _levelChoice = 2;
                                    break;
                                case 2:
                                    _levelChoice = 3;
                                    break;
                                case 3:
                                    _levelChoice = 0;
                                    break;
                            }
                        }

                        if (_keyboardState.IsKeyUp(Keys.Left) && _oldKey.IsKeyDown(Keys.Left))
                        {
                            switch (_levelChoice)
                            {
                                case 0:
                                    _levelChoice = 3;
                                    break;
                                case 1:
                                    _levelChoice = 0;
                                    break;
                                case 2:
                                    _levelChoice = 1;
                                    break;
                                case 3:
                                    _levelChoice = 2;
                                    break;
                            }
                        }
                        break;
                    case 2:
                        if (_keyboardState.IsKeyUp(Keys.Enter) && _oldKey.IsKeyDown(Keys.Enter))
                            isStarted = true;
                        break;
                }
            }
            else
            {
                _box.SetBorders(_levelChoice);
                _borders.AddRange(_box.getBorder());
                if (_keyboardState.IsKeyDown(Keys.Space) && !gameOver)
                {
                    if (!isPaused)
                    {
                        pauseRequest = false;
                    }
                    else
                    {
                        pauseRequest = true;
                    }
                }
                if (_keyboardState.IsKeyUp(Keys.Space))
                {
                    if (!pauseRequest)
                    {
                        isPaused = true;
                    }
                    else
                    {
                        isPaused = false;
                    }
                }

                _speed++;
                _speed = _speed % _speedFactor;
                if (_snake.Count < _screenHeight * _screenWidth / (pas * pas) + _box.getBorder().Count)
                {
                    if (!isPaused && (_speed == 0))
                    {
                        if (
                            ((_keyboardState.IsKeyDown(Keys.Right) || _previousKey == Keys.Right) && _previousMove != Keys.Left) ||
                            (_previousMove == Keys.Right && !_keyboardState.IsKeyDown(Keys.Up) && !_keyboardState.IsKeyDown(Keys.Down) && _previousKey != Keys.Up && _previousKey != Keys.Down)
                            )
                        {
                            _snake[0].LoadContent(Content, "headR");
                            _prevPos1 = _snake[0].Position;
                            if (_snake[0].Position.X == _screenWidth - pas)
                            {
                                _snake[0].Position = new Vector2(0, _prevPos1.Y);
                            }
                            else
                            {
                                _snake[0].Position = new Vector2(_snake[0].Position.X + 20, _snake[0].Position.Y);
                            }
                            for (int i = 1; i < _snake.Count; i++)
                            {
                                if (_snake[i].Position != _snake[i - 1].Position)
                                {
                                    _prevPos2 = _snake[i].Position;
                                    _snake[i].Position = _prevPos1;
                                    _prevPos1 = _prevPos2;
                                }
                            }
                            _previousMove = Keys.Right;
                        }
                        else if (
                            ((_keyboardState.IsKeyDown(Keys.Left) || _previousKey == Keys.Left) && _previousMove != Keys.Right) ||
                            (_previousMove == Keys.Left && !_keyboardState.IsKeyDown(Keys.Up) && !_keyboardState.IsKeyDown(Keys.Down) && _previousKey != Keys.Up && _previousKey != Keys.Down)
                            )
                        {
                            _snake[0].LoadContent(Content, "headL");
                            _prevPos1 = _snake[0].Position;
                            if (_snake[0].Position.X == 0)
                            {
                                _snake[0].Position = new Vector2(_screenWidth - pas, _prevPos1.Y);
                            }
                            else
                            {
                                _snake[0].Position = new Vector2(_snake[0].Position.X - 20, _snake[0].Position.Y);
                            }

                            for (int i = 1; i < _snake.Count; i++)
                            {
                                if (_snake[i].Position != _snake[i - 1].Position)
                                {
                                    _prevPos2 = _snake[i].Position;
                                    _snake[i].Position = _prevPos1;
                                    _prevPos1 = _prevPos2;
                                }
                            }
                            _previousMove = Keys.Left;
                        }
                        else if (
                            ((_keyboardState.IsKeyDown(Keys.Up) || _previousKey == Keys.Up) && _previousMove != Keys.Down) ||
                            (_previousMove == Keys.Up && !_keyboardState.IsKeyDown(Keys.Left) && !_keyboardState.IsKeyDown(Keys.Right) && _previousKey != Keys.Left && _previousKey != Keys.Right)
                            )
                        {
                            _snake[0].LoadContent(Content, "headU");
                            _prevPos1 = _snake[0].Position;
                            if (_snake[0].Position.Y == 0)
                            {
                                _snake[0].Position = new Vector2(_prevPos1.X, _screenHeight - pas);
                            }
                            else
                            {
                                _snake[0].Position = new Vector2(_snake[0].Position.X, _snake[0].Position.Y - 20);
                            }
                            for (int i = 1; i < _snake.Count; i++)
                            {
                                if (_snake[i].Position != _snake[i - 1].Position)
                                {
                                    _prevPos2 = _snake[i].Position;
                                    _snake[i].Position = _prevPos1;
                                    _prevPos1 = _prevPos2;
                                }
                            }
                            _previousMove = Keys.Up;
                        }
                        else if (
                            ((_keyboardState.IsKeyDown(Keys.Down) || _previousKey == Keys.Down) && _previousMove != Keys.Up) ||
                            (_previousMove == Keys.Down && !_keyboardState.IsKeyDown(Keys.Right) && !_keyboardState.IsKeyDown(Keys.Left) && _previousKey != Keys.Right && _previousKey != Keys.Left)
                            )
                        {
                            _snake[0].LoadContent(Content, "headD");
                            _prevPos1 = _snake[0].Position;
                            if (_snake[0].Position.Y == _screenHeight - pas)
                            {
                                _snake[0].Position = new Vector2(_prevPos1.X, 0);
                            }
                            else
                            {
                                _snake[0].Position = new Vector2(_snake[0].Position.X, _snake[0].Position.Y + 20);
                            }
                            for (int i = 1; i < _snake.Count; i++)
                            {
                                if (_snake[i].Position != _snake[i - 1].Position)
                                {
                                    _prevPos2 = _snake[i].Position;
                                    _snake[i].Position = _prevPos1;
                                    _prevPos1 = _prevPos2;
                                }
                            }
                            _previousMove = Keys.Down;
                        }
                        _moves.Add(_previousMove);
                    }

                    for (int i = 1; i < _snake.Count - 1; i++)
                    {
                        if (_snake[i].Position.X == _snake[i + 1].Position.X && _snake[i].Position.X == _snake[i - 1].Position.X)
                            _snake[i].LoadContent(Content, "bodyV");
                        if (_snake[i].Position.Y == _snake[i + 1].Position.Y && _snake[i].Position.Y == _snake[i - 1].Position.Y)
                            _snake[i].LoadContent(Content, "bodyH");
                    }

                    if (_snake[_snake.Count - 1].Position == Vector2.Add(_snake[_snake.Count - 2].Position, new Vector2(pas, 0))
                        || (_snake[_snake.Count - 1].Position.X == 0 && _snake[_snake.Count - 2].Position == new Vector2(_screenWidth-pas,_snake[_snake.Count - 1].Position.Y)))
                        _snake[_snake.Count - 1].LoadContent(Content, "tailL");
                    if (_snake[_snake.Count - 1].Position == Vector2.Add(_snake[_snake.Count - 2].Position, new Vector2(-pas, 0))
                        || (_snake[_snake.Count - 1].Position.X == _screenWidth-pas && _snake[_snake.Count - 2].Position == new Vector2(0,_snake[_snake.Count - 1].Position.Y)))
                        _snake[_snake.Count - 1].LoadContent(Content, "tailR");
                    if (_snake[_snake.Count - 1].Position == Vector2.Add(_snake[_snake.Count - 2].Position, new Vector2(0, pas))
                        || (_snake[_snake.Count - 1].Position.Y == 0 && _snake[_snake.Count - 2].Position == new Vector2(_snake[_snake.Count - 1].Position.X,_screenHeight-pas)))
                        _snake[_snake.Count - 1].LoadContent(Content, "tailU");
                    if (_snake[_snake.Count - 1].Position == Vector2.Add(_snake[_snake.Count - 2].Position, new Vector2(0, -pas))
                        || (_snake[_snake.Count - 1].Position.Y == _screenHeight - pas && _snake[_snake.Count - 2].Position == new Vector2(_snake[_snake.Count - 1].Position.X, 0)))
                        _snake[_snake.Count - 1].LoadContent(Content, "tailD");


                    for (int j = 1; j < _snake.Count - 1; j++)
                    {
                        int k = _moves.Count - j;
                        switch (_moves[k])
                        {
                            case Keys.Up:
                                switch (_moves[k - 1])
                                {
                                    case Keys.Up:
                                        _snake[j].LoadContent(Content, "bodyV");
                                        break;
                                    case Keys.Right:
                                        _snake[j].LoadContent(Content, "cornerRD");
                                        break;
                                    case Keys.Left:
                                        _snake[j].LoadContent(Content, "cornerLD");
                                        break;
                                }
                                break;
                            case Keys.Down:
                                switch (_moves[k - 1])
                                {
                                    case Keys.Down:
                                        _snake[j].LoadContent(Content, "bodyV");
                                        break;
                                    case Keys.Right:
                                        _snake[j].LoadContent(Content, "cornerRU");
                                        break;
                                    case Keys.Left:
                                        _snake[j].LoadContent(Content, "cornerLU");
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case Keys.Left:
                                switch (_moves[k - 1])
                                {
                                    case Keys.Left:
                                        _snake[j].LoadContent(Content, "bodyH");
                                        break;
                                    case Keys.Up:
                                        _snake[j].LoadContent(Content, "cornerRU");
                                        break;
                                    case Keys.Down:
                                        _snake[j].LoadContent(Content, "cornerRD");
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case Keys.Right:
                                switch (_moves[k - 1])
                                {
                                    case Keys.Right:
                                        _snake[j].LoadContent(Content, "bodyH");
                                        break;
                                    case Keys.Up:
                                        _snake[j].LoadContent(Content, "cornerLU");
                                        break;
                                    case Keys.Down:
                                        _snake[j].LoadContent(Content, "cornerLD");
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                    }



                    if (_snake[0].Position == _food.Position)
                    {
                        Dot dot = new Dot(_screenWidth, _screenHeight);
                        dot.LoadContent(Content, _snake[_snake.Count - 1].AssetName);
                        dot.Position = _snake[_snake.Count - 1].Position;

                        _snake.Add(dot);

                        Random rnd = new Random();
                        bool test1 = true;
                        bool test2;
                        while (test1)
                        {
                            test2 = true;
                            _posX = 20 * rnd.Next(0, _screenWidth / 20 - 1);
                            _posY = 20 * rnd.Next(0, _screenHeight / 20 - 1);
                            foreach (Dot elt in _snake)
                            {
                                if ((_posX == elt.Position.X) && (_posY == elt.Position.Y))
                                {
                                    test2 = false;
                                }
                            }
                            foreach (Vector2 pos in _borders)
                            {
                                if ((_posX == pos.X) && (_posY == pos.Y))
                                {
                                    test2 = false;
                                }
                            }
                            if (test2)
                            {
                                test1 = false;
                            }
                            else
                            {
                                test1 = true;
                            }
                        }
                        _food.Position = new Vector2(_posX, _posY);

                        _score++;
                    }
                    else
                    {
                        if (!isPaused && !gameOver && _speed == 0)
                            _moves.Remove(_moves[0]);
                    }

                    for (int i = 1; i < _snake.Count; i++)
                    {
                        if (_snake[0].Position == _snake[i].Position)
                        {
                            if (!gameOver)
                                _previousKey = Keys.A;
                            gameOver = true;
                            isPaused = true;
                            pauseRequest = false;
                            if (_keyboardState.IsKeyUp(Keys.Space) && _previousKey == Keys.Space)
                                this.Initialize();
                        }
                    }

                    foreach (Vector2 pos in _borders)
                    {
                        if (_snake[0].Position == pos)
                        {
                            if (!gameOver)
                                _previousKey = Keys.A;
                            gameOver = true;
                            isPaused = true;
                            pauseRequest = false;
                            if (_keyboardState.IsKeyUp(Keys.Space) && _previousKey == Keys.Space)
                                this.Initialize();
                        }
                    }
                }
                else
                {
                    winner = true;
                }

                if (_snake[_snake.Count - 1].Position == _snake[_snake.Count - 2].Position)
                {
                    _snake[_snake.Count - 2].LoadContent(Content, _snake[_snake.Count - 1].AssetName);
                }
            }
            _oldKey = _keyboardState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            
            if (!isStarted)
                _menu.Draw(spriteBatch, gameTime, _menuCursor, _speedFactor, _levelChoice, _keyboardState);
            else
            {
                spriteBatch.Draw(_background, new Vector2(0, 0), Color.White);
                _box.Draw(spriteBatch, gameTime, _levelChoice);
                _food.Draw(spriteBatch, gameTime);
                //spriteBatch.DrawString(_font, _menuCursor, new Vector2(0, 80), Color.Black);
                //spriteBatch.DrawString(_font, _moves[1].ToString(), new Vector2(0, 40), Color.Black);
                //spriteBatch.DrawString(_font, _moves[2].ToString(), new Vector2(0, 80), Color.Black);

                for (int i = _snake.Count - 1; i >= 0; i--)
                {
                    _snake[i].Draw(spriteBatch, gameTime);
                }
                if (isPaused && !gameOver)
                {
                    spriteBatch.DrawString(_font, "Press SpaceBar to play", new Vector2((_screenWidth - _font.MeasureString("Press SpaceBar to play").X) / 2, 0), Color.Black);
                }

                spriteBatch.DrawString(_font, "[R]:Restart [Q]:Quit [SPACE]:Pause", new Vector2((_screenWidth - _font.MeasureString("[R]:Restart [Q]:Quit [SPACE]:Pause").X) / 2, _screenHeight + pas), Color.Black);

                if (gameOver)
                {

                    spriteBatch.Draw(gameOverImg, new Vector2(-10, 0), Color.White);
                    spriteBatch.DrawString(_scorePolice, "Score: " + _score.ToString(), new Vector2((_screenWidth - _font.MeasureString("Score: " + _score.ToString()).X) / 2, 99), Color.Black);
                }
                else
                {
                    spriteBatch.DrawString(_font, "Score: " + _score.ToString(), new Vector2((_screenWidth - _font.MeasureString("Score: " + _score.ToString()).X) / 2, _screenHeight), Color.Black);
                }

                if (winner)
                {
                    spriteBatch.DrawString(_font, "You won !", new Vector2((_screenWidth - _font.MeasureString("You won !").X) / 2, 0), Color.Black);
                }
            }            
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
