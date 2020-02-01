namespace DropGrid.Core.Environment
{
    /// <summary>
    /// Defines the standard contract for an entity that may exist in the game
    /// environment.
    /// </summary>
    public interface ICoreEntity
    {
        /// <summary>
        /// Adjusts the entity X position.
        /// </summary>
        /// <param name="gridX">New X position on the game grid.</param>
        void SetGridX(int gridX);

        /// <summary>
        /// Adjusts the entity Y position.
        /// </summary>
        /// <param name="gridX">New Y position on the game grid.</param>
        void SetGridY(int gridY);

        /// <returns>Entity X position on the game grid.</returns>
        int GetGridX();

        /// <returns>Entity Y position on the game grid.</returns>
        int GetGridY();

        /// <summary>
        /// Along with <cref>#GetGridHeight()</cref>, defines the dimensions of
        /// the entity on the game grid.
        /// </summary>
        /// <returns>Number of grids along X-axis the entity occupies.</returns>
        int GetGridWidth();

        /// <summary>
        /// Along with <cref>#GetGridWidth()</cref>, defines the dimensions of
        /// the entity on the game grid.
        /// </summary>
        /// <returns>Number of grids along Y-axis the entity occupies.</returns>
        int GetGridHeight();

        /// <summary>
        /// Determines if the entity should be considered to be rendered to the
        /// screen. By default, this is always true unless overridden.
        /// </summary>
        ///
        /// <returns>
        /// true if the entity should be considered for rendering, false
        /// otherwise.
        /// </returns>
        bool IsVisible();

        /// <summary>
        /// Marks the entity so that it will be removed in the next game update
        /// cycle.
        /// </summary>
        void RemoveOnNextUpdate();

        /// <summary>
        /// Check if the entity is marked for removal from the game environment.
        /// </summary>
        /// 
        /// <returns>
        /// true if the entity should be removed from the game environment upon
        /// the next game update cycle; false otherwise.
        /// </returns>
        bool ShouldRemove();
    }
}
