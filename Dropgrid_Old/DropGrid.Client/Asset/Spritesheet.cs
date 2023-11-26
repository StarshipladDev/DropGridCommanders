using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.Asset
{
    /// <summary>
    /// Provides methods to load and handle image data in a spritesheet.
    /// Once an image is loaded, it is sliced into cells of specified dimensions.
    /// </summary>
    public sealed class Spritesheet : Asset
    {
        public int CellWidth { get; }
        public int CellHeight { get; }
        public int CellsAcross { get; private set; }
        public int CellsDown { get; private set; }

        private Sprite[] _sprites;

        public Spritesheet(String reference, int cellSize) : this(reference, cellSize, cellSize) { }

        public Spritesheet(String reference, int cellWidth, int cellHeight) : base(reference)
        {
            CellWidth = cellWidth;
            CellHeight = cellHeight;
        }

        public override Asset Load(ContentManager contentManager)
        {
            Texture2D master = contentManager.Load<Texture2D>(Identifier);
            CellsAcross = master.Width / CellWidth;
            CellsDown = master.Height / CellHeight;
            int totalCells = CellsDown * CellsAcross;
            _sprites = new Sprite[totalCells];
            for (int i = 0; i < _sprites.Length; i++)
            {
                int x = i % CellsAcross;
                int y = i / CellsAcross;
                Color[] rasterData = new Color[CellWidth * CellHeight];
                Rectangle extractRegion = new Rectangle(x * CellWidth, y * CellHeight, CellWidth, CellHeight);
                master.GetData(0, extractRegion, rasterData, 0, CellWidth * CellHeight);
                Texture2D subImage = new Texture2D(master.GraphicsDevice, CellWidth, CellHeight);
                subImage.SetData(rasterData);
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
        public Sprite GetSpriteAt(int cellX, int cellY)
        {
            if (_sprites == null)
                throw new InvalidOperationException("Attempting to retrieve spritesheet data before initialization!");
            return _sprites[cellX + cellY * CellsAcross];
        }
    }
}
