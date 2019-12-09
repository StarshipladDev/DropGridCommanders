using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace DropGrid.Client.Asset
{
    /// <summary>
    /// This is the primary way the game loads and handles content. 
    /// 
    /// Each Asset is a wrapper around a specific data type. This type of implementation
    /// allows deferred / lazy resource loading.
    /// 
    /// TODO: Generify this class. Casting Objects is a bit disgusting.
    /// </summary>
    public abstract class Asset
    {
        public string Identifier { get; }

        public Asset(string identifier)
        {
            Identifier = identifier;

            AssetLoader.Register(this);
        }

        public abstract Asset Load(ContentManager contentManager);

        public abstract Object GetData();

        public bool IsLoaded() => GetData() != null;
    }
}
