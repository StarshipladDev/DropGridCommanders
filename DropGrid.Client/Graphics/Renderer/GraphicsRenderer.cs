using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DropGrid.Client.Asset;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DropGrid.Client.Graphics
{
    public sealed class GraphicsRenderer
    {
        private readonly Camera _camera;
        private readonly SpriteBatch _spriteBatch;
        private readonly GraphicsDevice _device;
        internal GameTime LastUpdateTime;
        private readonly Stack<IViewPerspective> _perspective = new Stack<IViewPerspective>();

        public Vector2 CameraOffset => new Vector2(_camera.OffsetX, _camera.OffsetY);

        public GraphicsRenderer(GraphicsDevice device, SpriteBatch spriteBatch, IViewPerspective perspective)
        {
            _camera = new Camera(350, 0);
            _spriteBatch = spriteBatch;
            _device = device;
            PushPerspective(perspective);

            if (_perspective.Count != 1)
                throw new InvalidOperationException("Bad internal state: renderer perspective stack should be of size 1");
        }

        public void Render([NotNull] SpriteAnimation animation, float x, float y, 
            float offsetX = 0, float offsetY = 0, Color mask=new Color(), bool applyOffset=true, float scale=1.0f)
        {
            SpriteFrame frame = animation.GetCurrentFrame();
            Render(frame.Sprite, x, y, offsetX, offsetY, mask, applyOffset, scale);
            animation.Update(LastUpdateTime);
        }
        
        public void Render([NotNull] Sprite sprite, float x, float y, 
            float offsetX=0, float offsetY=0, Color mask=new Color(), bool applyOffset=true, float scale=1.0f)
        {
            Texture2D texture = (Texture2D)sprite.GetData();
            int textureWidth = texture.Width;
            int textureHeight = texture.Height;

            (float transformedX, float transformedY) = GetProjectedCoordinates(new Vector2(x, y));
            int drawX = (int) (applyOffset ? Math.Round(transformedX + _camera.OffsetX + offsetX) : transformedX);
            int drawY = (int) (applyOffset ? Math.Round(transformedY + _camera.OffsetY + offsetY) : transformedY);
            int drawWidth = (int) Math.Round(textureWidth * GameEngine.GRAPHICS_SCALE * scale);
            int drawHeight = (int) Math.Round(textureHeight * GameEngine.GRAPHICS_SCALE * scale);

            if (mask.A == 0)
                mask = Color.White;
            Rectangle destination = new Rectangle(drawX, drawY, drawWidth, drawHeight);
            _spriteBatch.Draw(texture, destination, mask);
        }

        // TODO: Define custom render methods here with camera offsets applied

        public void PushPerspective([NotNull] IViewPerspective perspective) => _perspective.Push(perspective);

        public IViewPerspective PopPerspective()
        {
            if (_perspective.Count == 1)
                throw new InvalidOperationException("Cannot pop last view perspective in the stack!");

            return _perspective.Pop();
        }  

        public IViewPerspective GetCurrentPerspective() => _perspective.Peek();

        public Vector2 GetProjectedCoordinates(Vector2 coords) => GetCurrentPerspective().ToProjected(coords);
        public Vector2 GetProjectedCoordinates(Vector3 coords) => GetCurrentPerspective().ToProjected(coords);
        
        public Vector2 GetInternalCoordinates(Vector2 coords) => GetCurrentPerspective().ToInternal(coords);
        public Vector2 GetInternalCoordinates(Vector3 coords) => GetCurrentPerspective().ToInternal(coords);
        
        public void Start() => _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        public void Finish() => _spriteBatch.End();

        public void CameraPan(float xDiff, float yDiff) => _camera.Pan(xDiff, yDiff);
        public void CameraSet(float xOffset, float yOffset) => _camera.Set(xOffset, yOffset);
    }
}
