namespace DropGrid.Core.Environment
{
    /// <summary>
    /// Represents a player participating in a <c cref="CoreGameSession">CoreGameSession</c>.
    /// </summary>
    public abstract class CorePlayer
    {
        /// <summary>
        /// The username of the player.
        /// </summary>
        public string Username { get; }

        public CorePlayer(string username)
        {
            Username = username;
        }
    }
}