using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DropGrid.Client.Asset;

namespace DropGrid.Client.Graphics
{
    public static class GameFont
    {
        /// <summary>
        /// This is the game's standard font to use when an alternative is not provided.
        /// </summary>
        public static readonly FontStyle STYLE_DEFAULT = new DefaultFontStyle();

        /// <summary>
        /// Draws a string to the screen using bitmap font. Note that line breaks '\n' is also
        /// supported, which will begin subsequent texts on the next line.
        ///
        /// Fonts are rendered in cartesian co-ordinates and are not affected by renderer offsets.
        /// You do not wrap the call with <c>renderer.Start()</c> or <c>renderer.Finish()</c> for the invocation. 
        /// </summary>
        /// <param name="renderer">The renderer to draw textures with.</param>
        /// <param name="text">String text content to display.</param>
        /// <param name="x">X co-ordinate, in pixels, of the first character of the text.</param>
        /// <param name="y">Y co-ordinate, in pixels, of the first character of the text.</param>
        /// <param name="style">Optional: type of font to use, set to <c cref="STYLE_DEFAULT">FontStyle.STYLE_DEFAULT</c> by default.</param>
        /// <param name="scale">Optional: size of each character, which is 1.0f magnification by default.</param>
        public static void Render([NotNull] GraphicsRenderer renderer, string text, float x, float y,
            FontStyle style = null, float scale = 1.0f)
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
                    drawY += style.GetCharacterHeight() * scale;
                    drawX = x;
                }

                Sprite characterImage = style.GetTextureFor(character);
                if (characterImage != null)
                {
                    renderer.Render(characterImage, drawX, drawY, applyOffset:false, scale:scale);
                }

                drawX += style.GetCharacterWidth(character) * scale;
            }
            
            renderer.PopPerspective();
            renderer.Finish();
        }
    }

    public abstract class FontStyle
    {
        private FontSpacing _spacing;
        private string _supportedChars;
        private Spritesheet _bitmapSource;
        internal int RawCharacterRenderedWidth => _bitmapSource.CellWidth * GameEngine.GraphicsScale;
        
        internal FontStyle(Spritesheet source)
        {
            _bitmapSource = source;
            
            // ReSharper disable once VirtualMemberCallInConstructor
            _supportedChars = GetSupportedCharacters();
            // ReSharper disable once VirtualMemberCallInConstructor
            _spacing = GetSpacing();
        }

        protected abstract string GetSupportedCharacters();

        protected abstract FontSpacing GetSpacing();

        public int GetCharacterHeight() => _bitmapSource.CellHeight * GameEngine.GraphicsScale;

        public int GetCharacterWidth(char character) => _spacing.CharacterWidth(character, _supportedChars.Contains(character));

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

        protected override FontSpacing GetSpacing()
        {
            FontSpacing config = new FontSpacing(8);
            config.SetKerning("ABCDEFGHJKLMNOPQRSTUVWXYZ abcdeghkmnopqvuwyz023456789@_~?#$&=", 6);
            config.SetKerning("Ifjrstx1\"<>%", 5);
            config.SetKerning("-+^*/\\", 4);
            config.SetKerning("il,;'()[]", 3);
            config.SetKerning("!.|:", 2);
            return config;
        }
    }

    /// <summary>
    /// Represents the spacing (kerning) profile for a font style. 
    /// </summary>
    public class FontSpacing
    {
        private readonly Dictionary<char, int> _kerning = new Dictionary<char, int>();
        private readonly int _defaultWidth;

        /// <summary>
        /// Constructs a standard font spacing profile with a standard character width.
        /// </summary>
        /// <param name="defaultWidth">The width, in pixels, of a default (non-kerned) character.</param>
        internal FontSpacing(int defaultWidth)
        {
            _defaultWidth = defaultWidth;
        }

        internal void SetKerning(string characters, int width)
        {
            foreach (char character in characters)
                _kerning.Add(character, width);
        }

        /// <summary>
        /// Retrieves the actual width of a character glyph based on the kerning profile.
        /// </summary>
        /// <param name="character">The character to inquire.</param>
        /// <param name="supported">Whether the character is one of the supported characters by the font style.</param>
        /// <returns>The actual width of the character glyph if the character is supported, otherwise 0.</returns>
        internal int CharacterWidth(char character, bool supported)
        {
            if (!_kerning.ContainsKey(character))
                return supported ? _defaultWidth : 0;

            return _kerning[character] * GameEngine.GraphicsScale;
        }
    }
}