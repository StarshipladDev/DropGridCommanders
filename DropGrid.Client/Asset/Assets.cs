using System;
using System.Collections.Generic;

namespace DropGrid.Client.Asset
{
    public static class AssetRegistry
    {
        private static readonly Dictionary<string, Asset> AllAssets = new Dictionary<string, Asset>();

        // Add new assets here.
        public static readonly Spritesheet TILESET = new Spritesheet("tileset", 32);
        public static readonly Spritesheet TEST_ENTITY = new Spritesheet("testEntity", 16);
        public static readonly Sprite TILE_SELECTION = new Sprite("selection");
        public static readonly Sprite TILE_BOUNDARY = new Sprite("tileBoundary");

        public static List<Asset> GetAssetsToLoad()
        {
            List<Asset> assets = new List<Asset>();
            assets.Add(TILESET);
            assets.Add(TEST_ENTITY);
            assets.Add(TILE_SELECTION);
            assets.Add(TILE_BOUNDARY);
            return assets;
        }
        
        public static Asset GetAssetFromReference(string reference) => AllAssets[reference];

        public static void RegisterAsset(Asset asset)
        {
            if (AllAssets.ContainsKey(asset.Identifier))
                throw new ArgumentException("Another asset exists with reference: " + asset.Identifier);
            AllAssets.Add(asset.Identifier, asset);
        }
    }
}
