using System;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.Graphics
{
    /// <summary>
    /// Provides functionalities to convert internal view co-ordiantes to
    /// isometric view co-ordinates.
    /// </summary>
    public class IsometricPerspective : IViewPerspective
    {
        public Vector2 ToInternal(Vector2 point)
        {
            Vector2 result = new Vector2
            {
                X = (2.0f * point.Y + point.X) * 0.5f,
                Y = (2.0f * point.Y - point.X) * 0.5f
            };
            return result;
        }

        public Vector2 ToInternal(Vector3 point)
        {
            Vector2 result = ToInternal(new Vector2(point.X, point.Y));
            return result;
        }

        public Vector2 ToProjected(Vector2 point)
        {
            Vector2 result = new Vector2
            {
                X = point.X - point.Y,
                Y = (point.X + point.Y) / 2
            };
            return result;
        }

        public Vector2 ToProjected(Vector3 point)
        {
            Vector2 result = ToProjected(new Vector2(point.X, point.Y));
            result.X /= point.Z;
            result.Y /= point.Z;
            return new Vector2(result.X, result.Y);
        }
    }
}
