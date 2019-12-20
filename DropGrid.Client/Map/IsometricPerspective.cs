using System;
using Microsoft.Xna.Framework;

namespace DropGrid.Client.Map
{
    public class IsometricPerspective : IPerspective
    {
        
        public Vector2 toInternalCoordinates(Vector2 point)
        {
            throw new NotImplementedException();
        }

        public Vector3 toInternalCoordinates(Vector3 point)
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

        public Vector3 toViewCoordinates(Vector3 point)
        {
            Vector2 result = toViewCoordinates(new Vector2(point.X, point.Y));
            result.X /= point.Z;
            result.Y /= point.Z;
            return new Vector3(result.X, result.Y, point.Z);
        }
    }
}
