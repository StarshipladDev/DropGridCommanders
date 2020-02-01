using System;

namespace DropGrid.Core.Environment
{
    public abstract class CoreAbstractEntity : ICoreEntity
    {
        private bool _remove = false;
        private int GridX, GridY, GridWidth, GridHeight;

        public CoreAbstractEntity(int gridWidth, int gridHeight)
        {
            this.GridWidth = gridWidth;
            this.GridHeight = gridHeight;
        }

        /// <summary>
        /// Adjusts the entity (X,Y) position.
        /// </summary>
        /// <param name="x">New X position on the game grid.</param>
        /// <param name="y">New Y position on the game grid.</param>
        public void SetPosition(int x, int y)
        {
            SetGridX(x);
            SetGridY(y);
        }

        public void SetGridX(int gridX) => this.GridX = gridX;
        public void SetGridY(int gridY) => this.GridY = gridY;

        public int GetGridX() => GridX;
        public int GetGridY() => GridY;
        public int GetGridWidth() => GridWidth;
        public int GetGridHeight() => GridHeight;

        public bool IsVisible() => true;

        public void RemoveOnNextUpdate() => _remove = true;
        public bool ShouldRemove() => _remove;
    }
}
