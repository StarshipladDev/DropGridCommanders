using System;
using System.Collections.Generic;
using DropGrid.Client;
using DropGrid.Client.Asset;
using DropGrid.Client.Environment;
using DropGrid.Client.Graphics;
using DropGrid.Core.Environment;
using Microsoft.Xna.Framework;

namespace DropGrid.MacOS.Graphics.Renderer
{
    public static class EntityRenderers
    {
        private static readonly Dictionary<EntityType, EntityRenderer> _entityRenderers = new Dictionary<EntityType, EntityRenderer>();

        public static EntityRenderer Get(IClientEntity entity) => _entityRenderers[entity.GetEntityType()];

        static EntityRenderers()
        {
            _entityRenderers[EntityType.PlayerUnit] = new PlayerUnitEntityRenderer();
        }
    }

    public abstract class EntityRenderer
    {
        public abstract void Render(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime, IClientEntity entity, ClientMapTile onTile, float drawX, float drawY);
        public abstract void Update(GameEngine engine, GameTime gameTime);
    }

    class PlayerUnitEntityRenderer : EntityRenderer
    {
        public override void Render(GameEngine engine, GraphicsRenderer renderer, GameTime gameTime, IClientEntity entity, ClientMapTile onTile, float drawX, float drawY)
        {
            // TODO: Temporary testing
            ClientPlayerUnit unit = (ClientPlayerUnit) entity;
            PlayerUnitTextureBank textureBank = PlayerUnitAssets.Get(unit.UnitType);
            SpriteAnimation animation = textureBank.GetAnimation(PlayerUnitAnimationType.Idle);
            
            renderer.Render(animation.GetFrame(0).Sprite, drawX, drawY, offsetY: -64 + onTile.HeightOffset);
        }

        public override void Update(GameEngine engine, GameTime gameTime)
        {
            
        }
    }
}