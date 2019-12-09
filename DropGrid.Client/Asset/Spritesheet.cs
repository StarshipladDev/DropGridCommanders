using System;
using System.Collections.Generic;
using System.Text;

namespace DropGrid.Client.Asset
{
    /// <summary>
    /// Provides functionalities to load and handle image data in a spritesheet.
    /// Once an image is loaded, it is sliced into cells of specified dimensions.
    /// </summary>
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

        /// <summary>
        /// This is the preferred way to work with spritesheets. There should be no reason
        /// to invoke GetData() here.
        /// </summary>
        /// <param name="cellX">The X co-ordinate of the sprite cell.</param>
        /// <param name="cellY">The Y co-ordinate of the sprite cell.</param>
        /// <returns></returns>
        public Sprite getSpriteAt(int cellX, int cellY)
        {
            return _sprites[cellX + cellY * _cellColumns];
        }
    }
}
