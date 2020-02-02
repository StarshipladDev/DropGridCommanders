using System;
using DropGrid.Client.Asset;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.Graphics
{
    public sealed class GraphicsRenderer
    {
        private readonly Camera _camera;
        private readonly SpriteBatch _spriteBatch;
        private IViewPerspective Perspective { get; }
        public Vector2 CameraOffset => new Vector2(_camera.OffsetX, _camera.OffsetY);

        public GraphicsRenderer(SpriteBatch spriteBatch, IViewPerspective perspective)
        {
            _camera = new Camera(0, 0);
            _spriteBatch = spriteBatch;
            Perspective = perspective;
        }

        public void Render(Sprite sprite, float x, float y, float offsetX=0, float offsetY=0)
        {
            Texture2D texture = (Texture2D)sprite.GetData();
            int textureWidth = texture.Width;
            int textureHeight = texture.Height;

            (float transformedX, float transformedY) = Perspective.ToProjected(new Vector2(x, y));
            int drawX = (int) Math.Round(transformedX + _camera.OffsetX + offsetX);
            int drawY = (int) Math.Round(transformedY + _camera.OffsetY + offsetY);
            int drawWidth = textureWidth * GameEngine.GRAPHICS_SCALE;
            int drawHeight = textureHeight * GameEngine.GRAPHICS_SCALE;

            Rectangle destination = new Rectangle(drawX, drawY, drawWidth, drawHeight);
            _spriteBatch.Draw(texture, destination, Color.White);
        }

        // TODO: Define custom render methods here with camera offsets applied

        public void Start() => _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        public void Finish() => _spriteBatch.End();

        public void CameraPan(float xDiff, float yDiff) => _camera.Pan(xDiff, yDiff);
        public void CameraSet(float xOffset, float yOffset) => _camera.Set(xOffset, yOffset);
    }
}
