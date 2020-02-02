namespace DropGrid.Core.Environment
{
    public abstract class CoreAbstractEntity : ICoreEntity
    {
        private bool _remove;
        private int _gridX, _gridY;
        private readonly int _gridWidth;
        private readonly int _gridHeight;

        protected CoreAbstractEntity(int gridWidth, int gridHeight)
        {
            _gridWidth = gridWidth;
            _gridHeight = gridHeight;
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

        public void SetGridX(int gridX) => this._gridX = gridX;
        public void SetGridY(int gridY) => this._gridY = gridY;

        public int GetGridX() => _gridX;
        public int GetGridY() => _gridY;
        public int GetGridWidth() => _gridWidth;
        public int GetGridHeight() => _gridHeight;

        public bool IsVisible() => true;

        public void RemoveOnNextUpdate() => _remove = true;
        public bool ShouldRemove() => _remove;
    }
}
