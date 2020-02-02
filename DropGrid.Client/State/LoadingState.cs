using DropGrid.Client.Asset;
using DropGrid.Client.Graphics;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.State
{
    /// <summary>
    /// The initialisation state handles deferred content loading. It is too costly to load ALL game assets into memory during startup.
    /// When new unloaded assets have been requested, we switch to this state and load them.
    /// </summary>
    class LoadingState : EngineState
    {
        private bool _loadedEverything;
        private int _totalAssetsToLoad;
        private int _currentlyLoaded;

        public LoadingState()
        {
            AssetLoader.LoadQueue.Add(AssetRegistry.TILESET);
        }

        public override StateId GetId() => StateId.Initialise;
        
        public override void Render(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime)
        {

        }

        private bool firstRun = true;
        public override void Update(GameEngine engine, GameTime gameTime)
        {
            if (firstRun)
            {
                _totalAssetsToLoad = AssetLoader.LoadQueue.GetSize();
                _currentlyLoaded = 0;
                firstRun = false;
            }
            if (!_loadedEverything)
            {
                AssetLoader.LoadQueue.LoadNext();
                _currentlyLoaded++;
                _loadedEverything |= _currentlyLoaded == _totalAssetsToLoad;
            }
            if (IsAssetLoaded())
            {
                MapTileTextures.Initialise();

                engine.EnterState(StateId.Gameplay);
                return;
            }
        }

        private bool IsAssetLoaded()
        {
            return _loadedEverything;
        }
    }
}
