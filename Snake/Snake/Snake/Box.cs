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
    class Box : Sprite
    {
        private int _screenWidth;
        private int _screenHeight;
        private int _levelChoice;
        private List<Vector2> _borders;
        Box1 box1;
        Box2 box2;
        Box3 box3;

        public Box() { }

        public Box(int screenWidth, int screenHeight)
        {
            _borders = new List<Vector2>() { };
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;
            _levelChoice = 0;
            box1 = new Box1(_screenWidth, _screenHeight);
            box2 = new Box2(_screenWidth, _screenHeight);
            box3 = new Box3(_screenWidth, _screenHeight);
        }

        public void SetBorders(int i)
        {
            _levelChoice = i;
        }

        public List<Vector2> getBorder()
        {
            switch (_levelChoice)
            {
                case 0:
                    _borders = new List<Vector2>() { };
                    break;
                case 1:
                    _borders = box1.getBorder();
                    break;
                case 2:
                    _borders = box2.getBorder();
                    break;
                case 3:
                    _borders = box3.getBorder();
                    break;
                default:
                    _borders = new List<Vector2>() { };;
                    break;
            }
            return _borders;
        }

        public override void LoadContent(ContentManager content)
        {
            box1.Texture = content.Load<Texture2D>("border1");
            box2.Texture = content.Load<Texture2D>("border2");
            box3.Texture = content.Load<Texture2D>("border3");
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, int i)
        {
            if (i==1)
                spriteBatch.Draw(box1.Texture, box1.Position, Color.White);
            else if (i==2)
                spriteBatch.Draw(box2.Texture, box1.Position, Color.White);
            else if (i==3)
                spriteBatch.Draw(box3.Texture, box2.Position, Color.White);
        }

    }
}
