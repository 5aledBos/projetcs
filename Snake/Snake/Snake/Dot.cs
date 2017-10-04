using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Snake
{
    class Dot: Sprite
    {
        private int _screenHeight;
        private int _screenWidth;
        
        public Dot(int screenWidth, int screenHeight)
        {
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;
        }

        public override void LoadContent(ContentManager content, string assetName, Vector2 pos)
        {
            base.LoadContent(content, assetName);
            Position = pos;
        }

    }
}
