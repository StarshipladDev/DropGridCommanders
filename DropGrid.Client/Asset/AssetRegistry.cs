using System;
namespace DropGrid.Client.Asset
{
    public class AssetRegistry
    {
        public static readonly Spritesheet TILESET = new Spritesheet("ground_tiles", 32);

        private AssetRegistry() { }
    }
}
