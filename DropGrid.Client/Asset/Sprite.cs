using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace DropGrid.Client.Asset
{
    /// <summary>
    /// Wraps a Texture2D object while marking it as an instance of Asset.
    /// </summary>
    public class Sprite : Asset
    {
        private Texture2D _data;

        public Sprite(String identifier) : base(identifier) { }

        public Sprite(Texture2D data) : base("fromSpritesheet") => _data = data;

        public override object GetData()
        {
            return _data;
        }

        public override Asset Load(ContentManager contentManager)
        {
            Console.WriteLine("Loading...");
            _data = contentManager.Load<Texture2D>(Identifier);
            return this;
        }
    }
}
