using System;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.Graphics
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
}
