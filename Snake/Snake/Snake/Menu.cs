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
    class Menu : Sprite
    {
        private Texture2D arrowR;
        private Texture2D arrowL;
        private Texture2D arrowRClicked;
        private Texture2D arrowLClicked;
        private Texture2D arrowR1;
        private Texture2D arrowL1;
        private Texture2D arrowRClicked1;
        private Texture2D arrowLClicked1;
        private Texture2D newGame;
        private Texture2D newGameSelected;
        private Texture2D newGameClicked;
        private Texture2D easy;
        private Texture2D medium;
        private Texture2D hard;
        private Texture2D b0;
        private Texture2D b1;
        private Texture2D b2;
        private Texture2D b3;
        private Texture2D c0;
        private Texture2D c1;
        private Texture2D background;
        
        public Menu()
        { }

        public override void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>("Background2");

            arrowR = content.Load<Texture2D>("arrowR");
            arrowR1 = content.Load<Texture2D>("arrowR");
            arrowL = content.Load<Texture2D>("arrowL");
            arrowL1 = content.Load<Texture2D>("arrowL");
            arrowRClicked = content.Load<Texture2D>("arrowRClicked");
            arrowRClicked1 = content.Load<Texture2D>("arrowRClicked");
            arrowLClicked = content.Load<Texture2D>("arrowLClicked");
            arrowLClicked1 = content.Load<Texture2D>("arrowLClicked");

            newGame = content.Load<Texture2D>("newGame");
            newGameSelected = content.Load<Texture2D>("newGameSelected");
            newGameClicked = content.Load<Texture2D>("newGameClicked");

            easy = content.Load<Texture2D>("easy");
            medium = content.Load<Texture2D>("medium");
            hard = content.Load<Texture2D>("hard");

            b0 = content.Load<Texture2D>("b0");
            b1 = content.Load<Texture2D>("b1");
            b2 = content.Load<Texture2D>("b2");
            b3 = content.Load<Texture2D>("b3");

            c0 = content.Load<Texture2D>("c0");
            c1 = content.Load<Texture2D>("c1");
        }
        
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, int cursor, int speed, int level, KeyboardState keyboardState)
        {
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            if (cursor == 0)
                spriteBatch.Draw(c0, new Vector2(20, 4), Color.White);
            else if (cursor == 1)
                spriteBatch.Draw(c1, new Vector2(20, 61), Color.White);
            spriteBatch.Draw(arrowL, new Vector2 (40,7), Color.White);
            spriteBatch.Draw(arrowR, new Vector2(210, 7), Color.White);
            spriteBatch.Draw(arrowL1, new Vector2(40, 90), Color.White);
            spriteBatch.Draw(arrowR1, new Vector2(210, 90), Color.White);
            spriteBatch.Draw(newGame, new Vector2(36, 173), Color.White);

            switch (cursor)
            { 
                case 0 :
                    if (keyboardState.IsKeyDown(Keys.Right))
                        spriteBatch.Draw(arrowRClicked, new Vector2(210, 7), Color.White);
                    if (keyboardState.IsKeyDown(Keys.Left))
                        spriteBatch.Draw(arrowLClicked, new Vector2(40, 7), Color.White);
                    break;
                case 1:
                    if (keyboardState.IsKeyDown(Keys.Right))
                        spriteBatch.Draw(arrowRClicked, new Vector2(210, 90), Color.White);
                    if (keyboardState.IsKeyDown(Keys.Left))
                        spriteBatch.Draw(arrowLClicked, new Vector2(40, 90), Color.White);
                    break;
                case 2:
                    spriteBatch.Draw(newGameSelected, new Vector2(36, 173), Color.White);
                    if (keyboardState.IsKeyDown(Keys.Enter))
                        spriteBatch.Draw(newGameClicked, new Vector2(34, 171), Color.White);
                    break;
            }

            switch (speed)
            { 
                case 15:
                    spriteBatch.Draw(easy, new Vector2(95, 10), Color.White);
                    break;
                case 7:
                    spriteBatch.Draw(medium, new Vector2(75, 10), Color.White);
                    break;
                case 3:
                    spriteBatch.Draw(hard, new Vector2(90, 10), Color.White);
                    break;
            }

            switch (level)
            { 
                case 0:
                    spriteBatch.Draw(b0, new Vector2(85,65), Color.White);
                    break;
                case 1:
                    spriteBatch.Draw(b1, new Vector2(85, 65), Color.White);
                    break;
                case 2:
                    spriteBatch.Draw(b2, new Vector2(85, 65), Color.White);
                    break;
                case 3:
                    spriteBatch.Draw(b3, new Vector2(85, 65), Color.White);
                    break;
            }
        }

    }
}
