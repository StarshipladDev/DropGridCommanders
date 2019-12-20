﻿using System;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.Map
{
    /// <summary>
    /// Since we are using cartesian co-ordinates internally, the perspective
    /// part is trivial.
    /// </summary>
    public class CartesianPerspective : IPerspective
    {
        public Vector2 toInternalCoordinates(Vector2 point)
        {
            return point;
        }

        public Vector3 toInternalCoordinates(Vector3 point)
        {
            return point;
        }

        public Vector2 toViewCoordinates(Vector2 point)
        {
            return point;
        }

        public Vector3 toViewCoordinates(Vector3 point)
        {
            return point;
        }
    }
}