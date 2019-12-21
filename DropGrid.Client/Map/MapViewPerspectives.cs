using System;
namespace DropGrid.Client.Map
{
    /// <summary>
    /// The collection of all usable perspectives.
    /// </summary>
    public class MapViewPerspectives
    {
        public static readonly CartesianPerspective CARTESIAN = new CartesianPerspective();
        public static readonly IsometricPerspective ISOMETRIC = new IsometricPerspective();
    }
}
