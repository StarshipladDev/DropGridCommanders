using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.State
{
    /// <summary>
    /// Each GameState handles one succinct set of game routines. They each have a unique identifiable state ID.
    /// </summary>
    abstract class GameState
    {
        public bool Initialised {get; set;}

        public GameState()
        {
            Initialised = false;
        }

        public abstract StateId GetId();

        public virtual void Initialise(GameEngine engine) => Initialised = true;

        public abstract void Draw(GameEngine engine, SpriteBatch spriteBatch, GameTime gameTime);

        public abstract void Update(GameEngine engine, GameTime gameTime);

        public void OnEnter() { }

        public void OnExit() { }
    }
}
