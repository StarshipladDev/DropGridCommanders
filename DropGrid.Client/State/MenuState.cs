﻿using System;
using DropGrid.Client.Graphics;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.State
{
    /// <summary>
    /// Handles main menu logic.
    /// </summary>
    class MenuState : EngineState
    {
        public override StateId GetId() => StateId.Menu;

        public override void Render(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime) => throw new NotImplementedException();

        public override void Update(GameEngine engine, GameTime gameTime) => throw new NotImplementedException();
    }
}
