using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace Flappy
{
    class Sprite
    {
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        private Texture2D _texture;
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        private Vector2 _position;
        public string Asset
        {
            get { return _assetName; }
        }
        public virtual void Initialize() { }

        private string _assetName;
        public virtual void LoadContent(ContentManager content, string assetName)
        {
            _texture = content.Load<Texture2D>(assetName);
            _assetName = assetName;
        }
        public virtual void LoadContent(ContentManager content)
        {
        }

        public string AssetName
        {
            get { return _assetName; }
        }


        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, int i)
        { }

    }
}
