using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DropGrid.Client.Asset
{
    /// <summary>
    /// Wraps a Texture2D object while marking it as an instance of Asset.
    /// </summary>
    public sealed class Sprite : Asset
    {
        private Texture2D _data;

        public Sprite(string identifier) : base(identifier) { }

        public Sprite(Texture2D data) : base(null) => _data = data;

        public override object GetData()
        {
            return _data;
        }

        public override Asset Load(ContentManager contentManager)
        {
            _data = contentManager.Load<Texture2D>(Identifier);
            return this;
        }
    }
}
