using System;
using System.Collections.Generic;

namespace DropGrid.Client.Asset
{
    internal sealed class AssetLoader
    {
        private GameEngine _engine;
        private readonly Queue<Asset> _loadQueue = new Queue<Asset>();

        public static AssetLoader LoadQueue { get; private set; }

        private AssetLoader() { }

        public void Add(Asset asset, params Asset[] more)
        {
            _loadQueue.Enqueue(asset);
            foreach (Asset assetItem in more)
                _loadQueue.Enqueue(assetItem);
        }

        public Asset LoadNext()
        {
            if (IsEmpty())
                throw new InvalidOperationException("The loading queue is empty! Nothing to process.");
            Asset nextItem = _loadQueue.Dequeue();
            return nextItem.IsLoaded() ? nextItem : nextItem.Load(_engine.Content);
        }

        internal int GetSize()
        {
            return _loadQueue.Count;
        }

        public bool IsEmpty() => _loadQueue.Count == 0;

        public bool LoadAll()
        {
            while (!IsEmpty())
                LoadNext();
            return true;
        }

        public static void Initialise(GameEngine gameEngine)
        {
            if (LoadQueue != null)
                throw new InvalidOperationException("AssetLoader cannot be initialised twice!");
            LoadQueue = new AssetLoader {_engine = gameEngine};
        }
    }
}
