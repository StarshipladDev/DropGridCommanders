using System;
using DropGrid.Client.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.State
{
    /// <summary>
    /// Each GameState handles one succinct set of game routines. They each have a unique identifiable state ID.
    /// </summary>
    abstract class EngineState
    {
        public bool Initialised {get; set;}

        public EngineState()
        {
            Initialised = false;
        }

        public abstract StateId GetId();

        public virtual void Initialise(GameEngine engine) => Initialised = true;

        public abstract void Render(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime);

        public abstract void Update(GameEngine engine, GameTime gameTime);

        public void OnEnter() { }

        public void OnExit() { }
    }
}
