using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Flappy
{
    class Obstacle : Sprite
    {
        public Obstacle() { }

        public void Initialize()
        {
            this.Position = new Vector2(0, 0);

        }
    }
}
