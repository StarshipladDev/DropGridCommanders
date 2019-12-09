using System;
using System.Collections.Generic;

namespace DropGrid.Client.Asset
{
    class AssetLoader
    {
        private GameEngine _engine;
        private Queue<Asset> _loadQueue = new Queue<Asset>();

        private static AssetLoader _instance = null;
        public static AssetLoader LoadQueue { get { return _instance; } }

        private AssetLoader() { }

        public void Add(Asset asset) => _loadQueue.Enqueue(asset);

        public void Clear() => _loadQueue.Clear();

        public Asset LoadNext()
        {
            if (IsEmpty())
                throw new InvalidOperationException("The loading queue is empty! Nothing to process.");
            Asset nextItem = _loadQueue.Dequeue();
            return nextItem.IsLoaded() ? nextItem : nextItem.Load(_engine.Content);
        }

        public Asset GetItemAtFront() => IsEmpty() ? null : _loadQueue.Peek();

        public bool IsEmpty() => _loadQueue.Count == 0;

        public bool LoadAll()
        {
            while (!IsEmpty())
                LoadNext();
            return true;
        }

        public static void Initialise(GameEngine gameEngine)
        {
            if (_instance != null)
                throw new InvalidOperationException("AssetLoader cannot be initialised twice!");
            _instance = new AssetLoader();
            _instance._engine = gameEngine;
        }
    }
}
