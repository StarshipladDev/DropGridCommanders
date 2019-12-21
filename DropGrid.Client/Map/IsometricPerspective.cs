using System;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.Map
{
    /// <summary>
    /// The chosen perspective of the game.
    /// </summary>
    public class IsometricPerspective : IMapViewPerspective
    {
        public Vector2 toInternalCoordinates(Vector2 point)
        {
            Vector2 result = new Vector2();
            result.X = (2.0f * point.Y + point.X) * 0.5f;
            result.Y = (2.0f * point.Y - point.X) * 0.5f;
            return result;
        }

        public Vector2 toInternalCoordinates(Vector3 point)
        {
            Vector2 result = toInternalCoordinates(new Vector2(point.X, point.Y));
            return result;
        }

        public Vector2 toViewCoordinates(Vector2 point)
        {
            Vector2 result = new Vector2();
            result.X = point.X - point.Y;
            result.Y = (point.X + point.Y) / 2;
            return result;
        }

        public Vector2 toViewCoordinates(Vector3 point)
        {
            Vector2 result = toViewCoordinates(new Vector2(point.X, point.Y));
            result.X /= point.Z;
            result.Y /= point.Z;
            return new Vector2(result.X, result.Y);
        }
    }
}
