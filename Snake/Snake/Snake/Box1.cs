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
    class Box1 : Sprite
    {
        private int _screenWidth;
        private int _screenHeight;
        private List<Vector2> _borders = new List<Vector2>();

        public Box1( int screenWidth, int screenHeight)
        {
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;
            for (int i = 0; i < _screenHeight / 20; i++)
            { 
                _borders.Add(new Vector2(0,i*20));
                _borders.Add(new Vector2(260, i * 20));
            }
            for (int i = 0; i < _screenWidth / 20; i++)
            { 
                _borders.Add(new Vector2(i*20, 0));
                _borders.Add(new Vector2(i*20, 180));
            }

        }

        public List<Vector2> getBorder()
        {
            return _borders;
        }

    }
}
