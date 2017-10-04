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
    class Box2 : Sprite
    {
        private int _screenWidth;
        private int _screenHeight;
        private List<Vector2> _borders = new List<Vector2>();

        public Box2( int screenWidth, int screenHeight)
        {
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;
            _borders.Add(new Vector2(100, 60));
            _borders.Add(new Vector2(100, 80));
            _borders.Add(new Vector2(80, 80));
            _borders.Add(new Vector2(80, 120));
            _borders.Add(new Vector2(100, 140));
            _borders.Add(new Vector2(100, 120));
            _borders.Add(new Vector2(160, 60));
            _borders.Add(new Vector2(160, 80));
            _borders.Add(new Vector2(180, 80));
            _borders.Add(new Vector2(160, 120));
            _borders.Add(new Vector2(160, 140));
            _borders.Add(new Vector2(180, 120));
            _borders.Add(new Vector2(200, 120)); 
            _borders.Add(new Vector2(60, 120)); 
            _borders.Add(new Vector2(60, 80));
            _borders.Add(new Vector2(200, 80));


            _borders.Add(new Vector2(0, 0));
            _borders.Add(new Vector2(0, 20));
            _borders.Add(new Vector2(20, 0));
            
            _borders.Add(new Vector2(0, 180));
            _borders.Add(new Vector2(0, 160));
            _borders.Add(new Vector2(20, 180));

            _borders.Add(new Vector2(260, 0));
            _borders.Add(new Vector2(260, 20));
            _borders.Add(new Vector2(240, 0));

            _borders.Add(new Vector2(260, 180));
            _borders.Add(new Vector2(260, 160));
            _borders.Add(new Vector2(240, 180));
        }

        public List<Vector2> getBorder()
        {
            return _borders;
        }

    }
}
