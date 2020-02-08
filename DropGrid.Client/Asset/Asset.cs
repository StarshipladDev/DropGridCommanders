using Microsoft.Xna.Framework.Content;

namespace DropGrid.Client.Asset
{
    /// <summary>
    /// This is the primary way the game loads and handles content. 
    /// 
    /// Each Asset is a wrapper around a specific data type. This type of implementation
    /// allows deferred / lazy resource loading.
    /// </summary>
    public abstract class Asset
    {
        public string Identifier { get; }

        protected Asset(string identifier)
        {
            Identifier = identifier;
            if (identifier != null) { 
                AssetRegistry.RegisterAsset(this);
            }
        }

        public abstract Asset Load(ContentManager contentManager);

        public abstract object GetData();

        public bool IsLoaded() => GetData() != null;
    }
}
