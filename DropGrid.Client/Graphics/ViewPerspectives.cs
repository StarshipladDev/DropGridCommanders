using System;

namespace DropGrid.Client.Graphics
{
    /// <summary>
    /// The collection of all usable perspectives.
    /// </summary>
    public class ViewPerspectives
    {
        private ViewPerspectives() { }

        public static readonly CartesianPerspective CARTESIAN = new CartesianPerspective();
        public static readonly IsometricPerspective ISOMETRIC = new IsometricPerspective();
    }
}
