using System;
using DropGrid.Client.Map;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.Environment
{
    public class EnvironmentRenderer
    {
        private Camera _camera;
        private SpriteBatch _spriteBatch;

        public EnvironmentRenderer(GameEnvironment environment)
        {
            _camera = environment.Camera;
        }

        void Initialise(SpriteBatch spriteBatch) => _spriteBatch = spriteBatch;

        // TODO: Define your own draw methods here.
    }
}
