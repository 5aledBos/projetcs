using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake
{
    class NoBox : Sprite
    {
        private int _screenWidth;
        private int _screenHeight;

        public NoBox( int screenWidth, int screenHeight)
        {
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;
        }


    }
}
