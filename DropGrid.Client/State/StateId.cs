namespace DropGrid.Client.State
{
    /// <summary>
    /// A list of supported states by the game client.
    /// </summary>
    public enum StateId
    {
        /// <summary>
        /// The first stage in the engine startup process, in which all deferred resources are to be loaded.
        /// </summary>
        Initialise,
        
        /// <summary>
        /// The second stage in the game cycle, in which the player sets up the game session and game parameters.
        /// </summary>
        Menu,
        
        /// <summary>
        /// The main stage in the game cycle, in which the player interacts with the game environment.
        /// </summary>
        Gameplay
    }
}
