using System;
using System.Collections.Generic;

namespace DropGrid.Client.Asset
{
    public static class AssetRegistry
    {
        private static readonly Dictionary<string, Asset> AllAssets = new Dictionary<string, Asset>();

        // Add new assets here.
        public static readonly Spritesheet TILESET = new Spritesheet("tileset", 32);
        public static readonly Spritesheet TEST_ENTITY = new Spritesheet("testEntity", 32);

        public static Asset GetAssetFromReference(string reference) => AllAssets[reference];

        public static void RegisterAsset(Asset asset)
        {
            if (AllAssets.ContainsKey(asset.Identifier))
                throw new ArgumentException("Another asset exists with reference: " + asset.Identifier);
            AllAssets.Add(asset.Identifier, asset);
        }
    }
}
