﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;

namespace DropGrid.Client.Assets
{
    class AssetLoader
    {
        private GameEngine _engine;
        private Queue<Asset> _loadQueue = new Queue<Asset>();

        private static AssetLoader _instance = null;
        public static AssetLoader Instance { get { return _instance; } }

        private AssetLoader() { }

        public void AddToLoadingQueue(Asset asset) => _loadQueue.Enqueue(asset);

        public void ClearLoadingQueue() => _loadQueue.Clear();

        public Asset LoadNextAsset()
        {
            if (IsLoadingQueueEmpty())
                throw new InvalidOperationException("The loading queue is empty! Nothing to process.");
            Asset nextItem = _loadQueue.Dequeue();
            return nextItem.IsLoaded() ? nextItem : nextItem.Load(_engine.Content);
        }

        public Asset GetItemAtFrontOfQueue() => IsLoadingQueueEmpty() ? null : _loadQueue.Peek();

        public bool IsLoadingQueueEmpty() => _loadQueue.Count == 0;

        public bool LoadAllAssetsInQueue()
        {
            while (!IsLoadingQueueEmpty())
                LoadNextAsset();
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

    public abstract class Asset
    {
        public string Identifier { get; }

        public Asset(string identifier)
        {
            Identifier = identifier;
        }

        public abstract Asset Load(ContentManager contentManager);

        public abstract Object GetData();

        public bool IsLoaded() => GetData() != null;
    }

    public class Spritesheet : Asset
    {
        private int _cellWidth, _cellHeight;
        private int _cellRows, _cellColumns;

        private Sprite[] _sprites;

        public int CellWidth { get; }
        public int CellHeight { get; }

        public Spritesheet(String identifier, int cellSize) : this(identifier, cellSize, cellSize) { }

        public Spritesheet(String identifier, int cellWidth, int cellHeight) : base(identifier)
        {
            _cellWidth = cellWidth;
            _cellHeight = cellHeight;
        }

        public override Asset Load(ContentManager contentManager)
        {
            Texture2D master = contentManager.Load<Texture2D>(Identifier);
            _cellColumns = master.Width / _cellWidth;
            _cellRows = master.Height / _cellHeight;
            int totalCells = _cellColumns * _cellRows;
            _sprites = new Sprite[totalCells];
            Color[] rasterData;
            Rectangle extractRegion;
            for (int i = 0; i < _sprites.Length; i++)
            {
                int x = i % _cellColumns;
                int y = i / _cellColumns;
                rasterData = new Color[_cellWidth * _cellHeight];
                extractRegion = new Rectangle(x * _cellWidth, y * _cellHeight, _cellWidth, _cellHeight);
                master.GetData<Color>(0, extractRegion, rasterData, 0, _cellWidth * _cellHeight);
                Texture2D subImage = new Texture2D(master.GraphicsDevice, _cellWidth, _cellHeight);
                subImage.SetData<Color>(rasterData);
                _sprites[i] = new Sprite(subImage);
            }
            return this;
        }

        public override object GetData()
        {
            return _sprites;
        }

        public Sprite getSpriteAt(int cellX, int cellY)
        {
            return _sprites[cellX + cellY * _cellColumns];
        }
    }

    /// <summary>
    /// Wraps a Texture2D object while marking it as an instance of Asset.
    /// </summary>
    public class Sprite : Asset
    {
        private Texture2D _data;

        public Sprite(String identifier) : base(identifier) { }

        public Sprite(Texture2D data) : base("fromSpritesheet") => _data = data;

        public override object GetData()
        {
            return _data;
        }

        public override Asset Load(ContentManager contentManager)
        {
            Console.WriteLine("Loading...");
            _data = contentManager.Load<Texture2D>(Identifier);
            return this;
        }
    }
}
