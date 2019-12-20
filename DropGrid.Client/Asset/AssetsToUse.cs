using System;
using System.Collections.Generic;

namespace DropGrid.Client.Asset
{
    public class AssetsToUse
    {
        public Dictionary<string, Asset> AssetDictionary { get; }

        public AssetsToUse() : this(null) { }

        public AssetsToUse(Asset asset, params Asset[] more)
        {
            AssetDictionary = new Dictionary<string, Asset>();
            if (asset != null)
                Add(asset);
            Add(more);
        }

        public AssetsToUse Add(Asset asset, bool replaceDuplicate = true, params Asset[] more)
        {
            if (AssetDictionary.ContainsKey(asset.Identifier) && !replaceDuplicate)
                throw new ArgumentException("Duplicated asset '" + asset.Identifier + "'!");
            AssetDictionary.Add(asset.Identifier, asset);
            foreach (Asset extraAsset in more)
                AssetDictionary.Add(extraAsset.Identifier, extraAsset);
            return this;
        }

        public AssetsToUse Add(Asset[] assets, bool replaceDuplicate = true)
        {
            foreach (Asset asset in assets)
                Add(asset, replaceDuplicate);
            return this;
        }

        public AssetsToUse Import(AssetsToUse assets, bool replaceDuplicate = true)
        {
            Dictionary<string, Asset> importedAssets = assets.AssetDictionary;
            foreach (Asset asset in importedAssets.Values)
                Add(asset, replaceDuplicate);
            return this;
        }

        public Asset GetAsset(string identifier)
        {
            return AssetDictionary[identifier];
        }
    }
}
