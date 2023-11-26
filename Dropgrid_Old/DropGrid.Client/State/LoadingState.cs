using System.Collections.Generic;
using DropGrid.Client.Asset;
using DropGrid.Client.Graphics;
using DropGrid.Core.Environment;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.State
{

    sealed class LoadingState : EngineState
    {
        private bool _loadedEverything;
        private int _totalAssetsToLoad;
        private int _currentlyLoaded;

        public override StateId GetId() => StateId.Initialise;
        
        public override void Render(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime)
        {

        }

        private bool _firstRun = true;
        public override void Update(GameEngine engine, GameTime gameTime)
        {
            if (_firstRun)
            {
                List<Asset.Asset> assetsToLoad = AssetRegistry.GetAssetsToLoad();
                AssetLoader.LoadQueue.AddAll(assetsToLoad);

                _totalAssetsToLoad = AssetLoader.LoadQueue.GetSize();
                _currentlyLoaded = 0;
                _loadedEverything = _currentlyLoaded == _totalAssetsToLoad;
                _firstRun = false;
            }
            if (!_loadedEverything)
            {
                AssetLoader.LoadQueue.LoadNext();
                _currentlyLoaded++;
                _loadedEverything |= _currentlyLoaded == _totalAssetsToLoad;
            }

            if (!IsAssetLoaded()) return;
            
            MapTileTextures.Initialise();
            PlayerUnitAssets.Initialise();

            engine.EnterState(StateId.Gameplay);
        }

        private bool IsAssetLoaded()
        {
            return _loadedEverything;
        }
    }
}
