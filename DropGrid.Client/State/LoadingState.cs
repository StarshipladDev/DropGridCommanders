using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.State
{
    /// <summary>
    /// The initialisation state handles deferred content loading. It is too costly to load ALL game assets into memory during startup.
    /// When new unloaded assets have been requested, we switch to this state and load them.
    /// </summary>
    class LoadingState : GameState
    {
        public override StateId GetId() => StateId.Initialise;

        public override void Draw(GameEngine engine, SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public override void Update(GameEngine engine, GameTime gameTime)
        {
            if (IsInitializationFinished())
                engine.EnterState(StateId.Gameplay);
        }

        private bool IsInitializationFinished()
        {
            // TODO: Implement deferred resource loading later.
            return true;
        }
    }
}
