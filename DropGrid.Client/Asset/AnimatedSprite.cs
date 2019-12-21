using System;
using System.Collections;
using System.Collections.Generic;

namespace DropGrid.Client.Asset
{
    /// <summary>
    /// Stores a collection of sprites to be drawn in sequential order to portray an animation.
    /// </summary>
    public class SpriteAnimation
    {

        private string AssetReference;
        private ArrayList SpriteFrames = new ArrayList();

        private SpriteAnimation() { }

        public SpriteAnimation(string assetReference, params SpriteFrame[] animationFrames)
        {
            AssetReference = assetReference;
            foreach (SpriteFrame subsequentFrame in animationFrames)
                SpriteFrames.Add(subsequentFrame);
        }

        public SpriteAnimation AddFrame(SpriteFrame frame)
        {
            SpriteFrames.Add(frame);
            return this;
        }

        public SpriteFrame GetFrame(int frameIndex)
        {
            if (frameIndex < 0 || frameIndex >= SpriteFrames.Count)
                throw new ArgumentException("frameIndex out of bounds: " + frameIndex);
            return (SpriteFrame)SpriteFrames[frameIndex];
        }

        public static SpriteAnimation create(string reference) => new SpriteAnimation(reference);
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
            this.Sprite = sprite;
            this.Duration = duration;
        }
    }
}
