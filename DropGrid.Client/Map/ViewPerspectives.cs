using System;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.Map
{
    /// <summary>
    /// We work with cartesian co-ordinates within the game code. Only when objects
    /// are rendered on the screen will they be transformed to view coordinate.
    /// </summary>
    public interface IMapViewPerspective
    {
        /// <summary>
        /// Transforms a given point to rendered co-ordinate on screen.
        /// </summary>
        /// <param name="point">A vector2f that consists of (x, y).</param>
        /// <returns></returns>
        Vector2 toViewCoordinates(Vector2 point);

        /// <summary>
        /// Transforms a given point to rendered co-ordinate on screen.
        /// </summary>
        /// <param name="point">A vector3f that consists of (x, y, height).</param>
        /// <returns></returns>
        Vector2 toViewCoordinates(Vector3 point);

        /// <summary>
        /// Converts a rendered co-coordinate back to internal (cartesian) coordinates.
        /// </summary>
        /// <param name="point">A vector2f that consists of (x, y).</param>
        /// <returns></returns>
        Vector2 toInternalCoordinates(Vector2 point);

        /// <summary>
        /// Converts a rendered co-coordinate back to internal (cartesian) coordinates.
        /// </summary>
        /// <param name="point">A vector3f that consists of (x, y, height).</param>
        /// <returns></returns>
        Vector2 toInternalCoordinates(Vector3 point);
    }

    /// <summary>
    /// The collection of all usable perspectives.
    /// </summary>
    public class MapViewPerspectives
    {
        public static readonly CartesianPerspective CARTESIAN = new CartesianPerspective();
        public static readonly IsometricPerspective ISOMETRIC = new IsometricPerspective();
    }

    /// <summary>
    /// The chosen perspective of the game.
    /// </summary>
    public class IsometricPerspective : IMapViewPerspective
    {
        public Vector2 toInternalCoordinates(Vector2 point)
        {
            throw new NotImplementedException();
        }

        public Vector2 toInternalCoordinates(Vector3 point)
        {
            throw new NotImplementedException();
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

    /// <summary>
    /// Since we are using cartesian co-ordinates internally, the perspective
    /// part is trivial.
    /// </summary>
    public class CartesianPerspective : IMapViewPerspective
    {
        public Vector2 toInternalCoordinates(Vector2 point)
        {
            return point;
        }

        public Vector2 toInternalCoordinates(Vector3 point)
        {
            return new Vector2(point.X, point.Y);
        }

        public Vector2 toViewCoordinates(Vector2 point)
        {
            return point;
        }

        public Vector2 toViewCoordinates(Vector3 point)
        {
            return new Vector2(point.X, point.Y);
        }
    }
}
