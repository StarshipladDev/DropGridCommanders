namespace DropGrid.Client.Graphics
{
    /// <summary>
    /// The collection of all usable perspectives.
    /// </summary>
    public static class ViewPerspectives
    {
        public static readonly CartesianPerspective CARTESIAN = new CartesianPerspective();
        public static readonly IsometricPerspective ISOMETRIC = new IsometricPerspective();
    }
}
