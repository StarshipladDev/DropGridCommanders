using System;
using DropGrid.Client.Asset;
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
        private bool loadedEverything = false;
        private int totalAssetsToLoad;
        private int currentlyLoaded;

        public LoadingState()
        {
            AssetLoader.LoadQueue.Add(AssetRegistry.TILESET);
        }

        public override StateId GetId() => StateId.Initialise;

        public override void Initialise(GameEngine engine)
        {
            base.Initialise(engine);
        }

        public override void Draw(GameEngine engine, SpriteBatch spriteBatch, GameTime gameTime)
        {
            // TODO: Bit font status rendering...
        }

        private bool firstRun = true;
        public override void Update(GameEngine engine, GameTime gameTime)
        {
            if (firstRun)
            {
                totalAssetsToLoad = AssetLoader.LoadQueue.GetSize();
                currentlyLoaded = 0;
                firstRun = false;
            }
            if (!loadedEverything)
            {
                AssetLoader.LoadQueue.LoadNext();
                currentlyLoaded++;
                loadedEverything |= currentlyLoaded == totalAssetsToLoad;
            }
            if (IsInitializationFinished())
            {
                engine.EnterState(StateId.Gameplay);
                return;
            }
        }

        private bool IsInitializationFinished()
        {
            return loadedEverything;
        }
    }
}
