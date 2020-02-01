using System;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.Graphics
{
    /// <summary>
    /// Since we are using cartesian co-ordinates internally, the perspective
    /// part is trivial.
    /// </summary>
    public class CartesianPerspective : IViewPerspective
    {
        public Vector2 ToInternal(Vector2 point)
        {
            return point;
        }

        public Vector2 ToInternal(Vector3 point)
        {
            return new Vector2(point.X, point.Y);
        }

        public Vector2 ToProjected(Vector2 point)
        {
            return point;
        }

        public Vector2 ToProjected(Vector3 point)
        {
            return new Vector2(point.X, point.Y);
        }
    }
}
