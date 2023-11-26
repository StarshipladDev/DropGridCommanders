namespace DropGrid.Client.Graphics
{
    /// <summary>
    /// Manages screen viewable region and offset.
    /// </summary>
    public sealed class Camera
    {
        public float OffsetX { get; set; }
        public float OffsetY { get; set; }

        public Camera(float x, float y)
        {
            OffsetX = x;
            OffsetY = y;
        }

        public void Pan(float xIncrement, float yIncrement)
        {
            OffsetX += xIncrement;
            OffsetY += yIncrement;
        }

        public void Set(float offsetX, float offsetY)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
        }
    }
}
