namespace DropGrid.Core.Environment
{
    /// <summary>
    /// Defines the standard contract for an entity that may exist in the game
    /// environment.
    /// </summary>
    public interface IGameEntity
    {
        /// <summary>
        /// Adjusts the entity X position.
        /// </summary>
        /// <param name="gridX">New X position on the game grid.</param>
        public void SetGridX(int gridX);

        /// <summary>
        /// Adjusts the entity Y position.
        /// </summary>
        /// <param name="gridX">New Y position on the game grid.</param>
        public void SetGridY(int gridY);

        /// <returns>Entity X position on the game grid.</returns>
        public int GetGridX();

        /// <returns>Entity Y position on the game grid.</returns>
        public int GetGridY();

        /// <summary>
        /// Along with <cref>#GetGridHeight()</cref>, defines the dimensions of
        /// the entity on the game grid.
        /// </summary>
        /// <returns>Number of grids along X-axis the entity occupies.</returns>
        public int GetGridWidth();

        /// <summary>
        /// Along with <cref>#GetGridWidth()</cref>, defines the dimensions of
        /// the entity on the game grid.
        /// </summary>
        /// <returns>Number of grids along Y-axis the entity occupies.</returns>
        public int GetGridHeight();

        /// <summary>
        /// Determines if the entity should be considered to be rendered to the
        /// screen. By default, this is always true unless overridden.
        /// </summary>
        ///
        /// <returns>
        /// true if the entity should be considered for rendering, false
        /// otherwise.
        /// </returns>
        public virtual bool IsVisible() => true;

        /// <summary>
        /// Marks the entity so that it will be removed in the next game update
        /// cycle.
        /// </summary>
        public void RemoveOnNextUpdate();

        /// <summary>
        /// Check if the entity is marked for removal from the game environment.
        /// </summary>
        /// 
        /// <returns>
        /// true if the entity should be removed from the game environment upon
        /// the next game update cycle; false otherwise.
        /// </returns>
        public bool ShouldRemove();
    }
}
