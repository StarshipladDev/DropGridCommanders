using System;
using System.Collections.Generic;

namespace DropGrid.Client.Asset
{
    public class AssetRegistry
    {
        private static readonly Dictionary<string, Asset> ALL_ASSETS = new Dictionary<string, Asset>();

        // Add new assets here.
        public static readonly Spritesheet FONT = new Spritesheet("font", 8, 10);
        public static readonly Spritesheet TILESET = new Spritesheet("ground_tiles", 32);

        private AssetRegistry() { }

        public static Asset GetAssetFromReference(String reference) => ALL_ASSETS[reference];

        public static void RegisterAsset(Asset asset)
        {
            if (ALL_ASSETS.ContainsKey(asset.Identifier))
                throw new ArgumentException("Another asset exists with reference: " + asset.Identifier);
            ALL_ASSETS.Add(asset.Identifier, asset);
        }
    }
}
