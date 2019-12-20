using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.State
{
    /// <summary>
    /// Handles main menu logic.
    /// </summary>
    class MenuState : GameState
    {
        public override StateId GetId() => StateId.Menu;

        public override void Draw(GameEngine engine, SpriteBatch spriteBatch, GameTime gameTime) => throw new NotImplementedException();

        public override void Update(GameEngine engine, GameTime gameTime) => throw new NotImplementedException();
    }
}
