namespace DropGrid.Core.Environment
{
    /// <summary>
    /// Represents the setting against which the game board (map) and core game logic take place.
    /// </summary>
    public class CoreGameEnvironment
    {
        /// <summary>
        /// The map to be used in the environment.
        /// </summary>
        protected CoreMap Map { get; set; }

        protected CoreGameEnvironment()
        {
            
        }
    }
}
