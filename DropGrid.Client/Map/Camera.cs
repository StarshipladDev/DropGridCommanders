using System;
namespace DropGrid.Client.Map
{
    /// <summary>
    /// Manages screen viewable region and offset.
    /// </summary>
    public class Camera
    {
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }

        public Camera(double x, double y)
        {
            OffsetX = x;
            OffsetY = y;
        }

        public void Pan(double xIncrement, double yIncrement)
        {
            OffsetX += xIncrement;
            OffsetY += yIncrement;
        }

        public void Set(double offsetX, double offsetY)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
        }
    }
}
