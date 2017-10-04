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
    class Box3 : Sprite
    {
        private int _screenWidth;
        private int _screenHeight;
        private List<Vector2> _borders = new List<Vector2>();

        public Box3( int screenWidth, int screenHeight)
        {
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;
            for (int i = 3; i < _screenWidth / 20; i++)
            {
                _borders.Add(new Vector2(i*20, 80));
                _borders.Add(new Vector2(260-i*20, 120));
            }
            for (int i = 0; i < 5; i++)
            {
                _borders.Add(new Vector2(100, i*20));
                _borders.Add(new Vector2(160, 200-i * 20));
            }
            _borders.Add(new Vector2(0, 80));
            _borders.Add(new Vector2(20, 80));

            _borders.Add(new Vector2(260, 120));
            _borders.Add(new Vector2(240, 120));
        }

        public List<Vector2> getBorder()
        {
            return _borders;
        }

    }
}
