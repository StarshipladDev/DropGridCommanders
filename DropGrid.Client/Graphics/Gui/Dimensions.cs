using System;
using JetBrains.Annotations;

namespace DropGrid.Client.Graphics.Gui
{
    public class Dimensions
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Dimensions(int width, int height)
        {
            if (width < 0)
                throw new ArgumentException("Dimension width cannot be negative!");
            if (height < 0)
                throw new ArgumentException("Dimension height cannot be negative!");

            Width = width;
            Height = height;
        }
        
        public void TrimCeilingBasedOn(Dimensions size)
        {
            if (Width < size.Width)
                Width = size.Width;
            if (Height < size.Height)
                Height = size.Height;
        }

        public void TrimFloorBasedOn(Dimensions size)
        {
            if (Width > size.Width)
                Width = size.Width;
            if (Height > size.Height)
                Height = size.Height;
        }

        public static bool operator <([NotNull] Dimensions d1, [NotNull] Dimensions d2)
        {
            return d1.Width < d2.Width || d1.Height < d2.Height;
        }
        
        public static bool operator >([NotNull] Dimensions d1, [NotNull] Dimensions d2)
        {
            return d1.Width > d2.Width && d1.Height > d2.Height;
        }

        public static bool operator ==([NotNull] Dimensions d1, [NotNull] Dimensions d2)
        {
            return d1.Width == d2.Width && d1.Height == d2.Height;
        }

        public static bool operator !=([NotNull] Dimensions d1, [NotNull] Dimensions d2)
        {
            return d1.Width != d2.Width && d1.Height != d2.Height;
        }
    }
}