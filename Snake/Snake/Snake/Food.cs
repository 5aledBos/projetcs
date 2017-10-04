using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Snake
{
    class Food : Sprite
    {
        private int _screenHeight;
        private int _screenWidth;


        public Food(int screenWidth, int screenHeight)
        {
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;
        }

        public override void LoadContent(ContentManager content, string assetName, List<Dot> list)
        {
            base.LoadContent(content, assetName);
            
            Position = new Vector2(0,0);
        }

        
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.Yellow);
        }
    }
}
