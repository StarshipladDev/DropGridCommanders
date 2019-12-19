using System;
using DropGrid.Client.Asset;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.State
{
    /// <summary>
    /// The crux of the client-side game logic belongs here.
    /// </summary>
    class GameplayState : GameState
    {
        public override StateId GetId() => StateId.Gameplay;

        // TODO: This is only temporary
        bool loaded = false;
        AssetsToUse assetsToUse;
        public override void Draw(GameEngine engine, SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!loaded)
            {
                assetsToUse = new AssetsToUse().Add(new Spritesheet("basic_ground_tiles", 128));

                AssetLoader.LoadQueue.Add(assetsToUse);
                AssetLoader.LoadQueue.LoadAll();
                loaded = true;
            }
            spriteBatch.Begin();
            spriteBatch.Draw((Texture2D)((Spritesheet)assetsToUse.GetAsset("basic_ground_tiles")).getSpriteAt(0, 0).GetData(), new Vector2(200, 200));
            spriteBatch.End();
        }

        public override void Update(GameEngine engine, GameTime gameTime)
        {

        }
    }
}
