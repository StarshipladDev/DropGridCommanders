using System;
using DropGrid.Client.Asset;
using DropGrid.Client.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.Graphics
{
    public class GraphicsRenderer
    {
        private Camera _camera;
        private SpriteBatch _spriteBatch;
        private IViewPerspective _mapPerspective;

        public GraphicsRenderer(SpriteBatch spriteBatch, IViewPerspective mapPerspective)
        {
            _camera = new Camera(0, 0);
            _spriteBatch = spriteBatch;
            _mapPerspective = mapPerspective;
        }

        public void Render(SpriteFrame spriteFrame, float x, float y)
        {
            Render(spriteFrame.Sprite, x, y);
        }

        public void Render(Sprite sprite, float x, float y)
        {
            Texture2D texture = (Texture2D)sprite.GetData();
            int textureWidth = texture.Width;
            int textureHeight = texture.Height;

            Vector2 perspectiveTransform = _mapPerspective.ToProjected(new Vector2(x, y));
            int drawX = (int) Math.Round(perspectiveTransform.X + _camera.OffsetX);
            int drawY = (int) Math.Round(perspectiveTransform.Y + _camera.OffsetY);
            int drawWidth = textureWidth * GameEngine.GRAPHICS_SCALE;
            int drawHeight = textureHeight * GameEngine.GRAPHICS_SCALE;

            Rectangle destination = new Rectangle(drawX, drawY, drawWidth, drawHeight);
            _spriteBatch.Draw(texture, destination, Color.White);
        }

        // TODO: Define custom render methods here with camera offsets applied

        public void Start() => _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        public void Finish() => _spriteBatch.End();
    }
}
