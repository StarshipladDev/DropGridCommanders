using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ObjCRuntime;

namespace DropGrid.Client.Asset
{
    /// <summary>
    /// Stores a collection of sprites to be drawn in sequential order to portray an animation.
    /// </summary>
    public sealed class SpriteAnimation
    {

        private string _assetReference;
        private readonly List<SpriteFrame> _spriteFrames = new List<SpriteFrame>();
        
        /// <summary>
        /// Whether to play the animation in an endless cycle.
        /// </summary>
        private bool _loop = false;
        
        /// <summary>
        /// When animation reaches the end for the first time, play the next cycle in reverse mode, then reverse again...
        /// </summary>
        private bool _pingPong = false;
        
        /// <summary>
        /// Related to <c>_pingPong</c>, determines if this cycle should iterate frames backwards.
        /// </summary>
        private bool _playReverse = false;

        private bool _done = false;
        
        /// <summary>
        /// Counter for which current frame we are displaying.
        /// </summary>
        private int _currentFrame = 0;
        
        /// <summary>
        /// Time, in milliseconds, of last time we updated our frame.
        /// </summary>
        private long _lastUpdateTime;
        
        /// <summary>
        /// Time accumulated, in milliseconds, since the last time the frame was updated.
        /// </summary>
        private long _accumulatedTime;

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
            return _spriteFrames[frameIndex];
        }

        public void Update(GameTime gameTime)
        {
            if (_lastUpdateTime == 0)
            {
                _lastUpdateTime = gameTime.ElapsedGameTime.Milliseconds;
                return;
            }

            _accumulatedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (_accumulatedTime > GetCurrentFrame().Duration)
            {
                TickFrame();
            }
        }

        private void TickFrame()
        {
            if (_done)
                return;
            
            if (_playReverse)
            {
                if (_currentFrame > 0)
                    PreviousFrame();
                else
                {
                    if (_loop)
                    {
                        if (_pingPong)
                        {
                            _playReverse = !_playReverse;
                            NextFrame();
                        }
                        else
                            _currentFrame = _spriteFrames.Count - 1;
                    }
                    else _done = true;
                }
            }
            else
            {
                if (_currentFrame < _spriteFrames.Count - 1)
                    NextFrame();
                else
                {
                    if (_loop)
                    {
                        if (_pingPong)
                        {
                            _playReverse = !_playReverse;
                            PreviousFrame();
                        }
                        else
                            _currentFrame = 0;
                    }
                    else _done = true;
                }
            }

            _lastUpdateTime = new DateTime().Millisecond;
            _accumulatedTime = 0;
        }

        private void NextFrame()
        {
            if (_currentFrame < _spriteFrames.Count - 1)
                ++_currentFrame;
        }

        private void PreviousFrame()
        {
            if (_currentFrame > 0)
                --_currentFrame;
        }

        public SpriteFrame GetCurrentFrame() => _spriteFrames[_currentFrame];

        public SpriteAnimation SetLoop(bool flag)
        {
            _loop = flag;
            return this;
        }

        /// <summary>
        /// Upon completing the first animation cycle, determine if the next cycle should be played in reverse frame
        /// order, and then again in the subsequent cycle. Note that this has to be used in conjunction with
        /// <c>SetLoop(true);</c> otherwise the animation will not play more than 1 cycle.
        /// </summary>
        /// <param name="flag">true to enable this form of playback, false otherwise.</param>
        /// <returns></returns>
        public SpriteAnimation SetPingPong(bool flag)
        {
            _pingPong = flag;
            return this;
        }

        /// <summary>
        /// Reinitialise the animation state.
        /// </summary>
        public void Reset()
        {
            _done = false;
            _currentFrame = 0;
            _lastUpdateTime = new DateTime().Millisecond;
            _accumulatedTime = 0;
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
