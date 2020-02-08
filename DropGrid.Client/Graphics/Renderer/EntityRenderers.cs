using System;
using System.Collections.Generic;
using DropGrid.Client;
using DropGrid.Client.Asset;
using DropGrid.Client.Environment;
using DropGrid.Client.Graphics;
using DropGrid.Core.Environment;
using Microsoft.Xna.Framework;
//using Vector2 = OpenTK.Vector2;

namespace DropGrid.MacOS.Graphics.Renderer
{
    public static class EntityRenderers
    {
        private static readonly Dictionary<EntityType, EntityRenderer> _entityRenderers = new Dictionary<EntityType, EntityRenderer>();

        public static EntityRenderer Get(CoreAbstractEntity entity) => _entityRenderers[entity.EntityType];

        static EntityRenderers()
        {
            _entityRenderers[EntityType.PlayerUnit] = new PlayerUnitRenderer();
        }
    }

    public abstract class EntityRenderer
    {
        public abstract void Render(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime, CoreAbstractEntity entity, ClientMapTile onTile, float drawX, float drawY);
        public abstract void Update(GameEngine engine, GameTime gameTime);
    }

    class PlayerUnitRenderer : EntityRenderer
    {
        public override void Render(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime, CoreAbstractEntity entity, ClientMapTile onTile, float drawX, float drawY)
        {
            // TODO: Temporary testing
            ClientPlayerUnit unit = (ClientPlayerUnit) entity;
            SpriteAnimation animation = unit.Textures.GetAnimation(PlayerUnitAnimationType.Idle);
            Vector2 position = unit.ScreenPosition;
            Color factionColor = unit.Player.FactionColor;

            float height = ClientMapTile.TILE_HEIGHT / 4 + unit.SpriteSize.Height - onTile.HeightOffset;
            renderer.Render(animation, position.X, position.Y, offsetY:-height, mask:factionColor);
        }

        public override void Update(GameEngine engine, GameTime gameTime)
        {
            
        }
    }
}