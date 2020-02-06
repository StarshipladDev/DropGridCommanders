using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DropGrid.Client.Asset;

namespace DropGrid.Client.Graphics
{
    public class GameFont
    {
        public static readonly FontStyle STYLE_DEFAULT = new DefaultFontStyle();

        public static void Render([NotNull] GraphicsRenderer renderer, string text, float x, float y,
            FontStyle style = null)
        {
            if (style == null)
                style = STYLE_DEFAULT;
            
            renderer.Start();
            renderer.PushPerspective(ViewPerspectives.CARTESIAN);
            float drawX = x;
            float drawY = y;
            
            foreach (char character in text)
            {
                if (character == '\n')
                {
                    drawY += style.GetCharacterHeight();
                    drawX = x - style.RawCharacterRenderedWidth;
                }

                Sprite characterImage = style.GetTextureFor(character);
                if (characterImage != null)
                {
                    renderer.Render(characterImage, drawX, drawY, applyOffset:false);
                }

                drawX += style.GetCharacterWidth(character);
            }
            
            renderer.PopPerspective();
            renderer.Finish();
        }
    }

    public abstract class FontStyle
    {
        private FontSpacingConfiguration _spacing;
        private string _supportedChars;
        private Spritesheet _bitmapSource;
        internal int RawCharacterRenderedWidth => _bitmapSource.CellWidth * GameEngine.GRAPHICS_SCALE;
        
        internal FontStyle(Spritesheet source)
        {
            _bitmapSource = source;
            
            // ReSharper disable once VirtualMemberCallInConstructor
            _supportedChars = GetSupportedCharacters();
            // ReSharper disable once VirtualMemberCallInConstructor
            _spacing = GetSpacingConfiguration();
        }

        protected abstract string GetSupportedCharacters();

        protected abstract FontSpacingConfiguration GetSpacingConfiguration();

        public int GetCharacterHeight() => _bitmapSource.CellHeight * GameEngine.GRAPHICS_SCALE;

        public int GetCharacterWidth(char character) => _spacing.CharacterWidth(character);

        public Sprite GetTextureFor(char character)
        {
            if (!IsCharacterSupported(character))
                return null;

            int charIndex = _supportedChars.IndexOf(character);
            if (charIndex < 0 || charIndex > _supportedChars.Length - 1)
                return null;

            int cellX = charIndex % _bitmapSource.CellsAcross;
            int cellY = charIndex / _bitmapSource.CellsAcross;
            return _bitmapSource.GetSpriteAt(cellX, cellY);
        }

        public bool IsCharacterSupported(char character) => _supportedChars.Contains(character);
    }

    internal sealed class DefaultFontStyle : FontStyle
    {
        public DefaultFontStyle() : base(AssetRegistry.FONT_DEFAULT)
        {
        }

        protected override string GetSupportedCharacters()
        {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                   "abcdefghijklmnopqrstuvwxyz" +
                   "0123456789;'\"             " +
                   "!@#$%^&*()-+_=~.,<>?/\\[]|:";
        }

        protected override FontSpacingConfiguration GetSpacingConfiguration()
        {
            FontSpacingConfiguration config = new FontSpacingConfiguration(8);
            config.SetKerning("ABCDEFGHJKLMNOPQRSTUVWXYZ abcdeghkmnopqvuwyz023456789@_~?#$&=", 6);
            config.SetKerning("Ifjrstx1\"<>%", 5);
            config.SetKerning("-+^*/\\", 4);
            config.SetKerning("il,;'()[]", 3);
            config.SetKerning("!.|:", 2);
            return config;
        }
    }

    public class FontSpacingConfiguration
    {
        private readonly Dictionary<char, int> _kerning = new Dictionary<char, int>();
        private readonly int _defaultWidth;

        internal FontSpacingConfiguration(int defaultWidth)
        {
            _defaultWidth = defaultWidth;
        }

        internal void SetKerning(string characters, int width)
        {
            foreach (char character in characters)
                _kerning.Add(character, width);
        }

        internal int CharacterWidth(char character)
        {
            if (!_kerning.ContainsKey(character))
                return _defaultWidth * GameEngine.GRAPHICS_SCALE;
            
            return _kerning[character] * GameEngine.GRAPHICS_SCALE;
        }
    }
}