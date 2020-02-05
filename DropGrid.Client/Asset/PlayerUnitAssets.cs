using System;
using System.Collections.Generic;
using DropGrid.Client.Environment;
using DropGrid.Core.Environment;

namespace DropGrid.Client.Asset
{
    public static class PlayerUnitAssets
    {
        private static readonly Dictionary<PlayerUnitType, PlayerUnitTextureBank> Textures = new Dictionary<PlayerUnitType, PlayerUnitTextureBank>();
        private static bool _initialised;

        public static void Initialise()
        {
            if (_initialised)
                return;

            CreateTestSoldierTextureBank();

            _initialised = true;
        }

        private static void CreateTestSoldierTextureBank()
        {
            var bank = new PlayerUnitTextureBank();

            Spritesheet ss = AssetRegistry.TEST_ENTITY;
            SpriteAnimation idle = SpriteAnimation.Create(AssetRegistry.TEST_ENTITY.Identifier)
                .SetLoop(true)
                .SetPingPong(true);
            
            idle.AddFrame(ss.GetSpriteAt(0, 0), 200);
            idle.AddFrame(ss.GetSpriteAt(1, 0), 200);
            idle.AddFrame(ss.GetSpriteAt(2, 0), 200);
            bank.AddAnimation(PlayerUnitAnimationType.Idle, idle);

            Register(PlayerUnitType.TestSoldier, bank);
        }

        private static void Register(PlayerUnitType unitType, PlayerUnitTextureBank textureBank)
        {
            Textures.Add(unitType, textureBank);
        }

        public static PlayerUnitTextureBank Get(PlayerUnitType unitType)
        {
            return Textures[unitType] == null
                ? throw new ArgumentException("No texture bank exist for unit type: " + unitType)
                : Textures[unitType];
        }
    }
    
    public sealed class PlayerUnitTextureBank
    {
        private static readonly Dictionary<PlayerUnitAnimationType, SpriteAnimation> _animations = new Dictionary<PlayerUnitAnimationType, SpriteAnimation>();
        
        internal void AddAnimation(PlayerUnitAnimationType animationType, SpriteAnimation animation) => _animations[animationType] = animation;

        public SpriteAnimation GetAnimation(PlayerUnitAnimationType animationType)
        {
            return _animations[animationType] == null
                ? throw new ArgumentException("No animation exist for animation type: " + animationType)
                : _animations[animationType];
        }
    }
}