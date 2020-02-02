using System;
using System.Collections;

namespace DropGrid.Client.Asset
{
    /// <summary>
    /// Stores a collection of sprites to be drawn in sequential order to portray an animation.
    /// </summary>
    public sealed class SpriteAnimation
    {

        private string _assetReference;
        private readonly ArrayList _spriteFrames = new ArrayList();

        public SpriteAnimation(string assetReference, params SpriteFrame[] animationFrames)
        {
            _assetReference = assetReference;
            foreach (SpriteFrame subsequentFrame in animationFrames)
                _spriteFrames.Add(subsequentFrame);
        }

        public SpriteAnimation AddFrame(Sprite sprite, int duration)
        {
            _spriteFrames.Add(new SpriteFrame(sprite, duration));
            return this;
        }

        public SpriteFrame GetFrame(int frameIndex)
        {
            if (frameIndex < 0 || frameIndex >= _spriteFrames.Count)
                throw new ArgumentException("frameIndex out of bounds: " + frameIndex);
            return (SpriteFrame)_spriteFrames[frameIndex];
        }

        public static SpriteAnimation Create(string reference) => new SpriteAnimation(reference);
    }

    /// <summary>
    /// Describes a single frame used in a SpriteAnimation.
    /// </summary>
    public class SpriteFrame
    {
        public int Duration { get; set; }
        public Sprite Sprite { get; set; }

        public SpriteFrame(Sprite sprite, int duration)
        {
            Sprite = sprite;
            Duration = duration;
        }
    }
}
