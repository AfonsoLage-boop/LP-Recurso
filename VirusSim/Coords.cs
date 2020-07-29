namespace VirusSim
{
    /// <summary>
    /// <c>Coords</c> Class.
    /// Creates grid positions.
    /// </summary>
    public class Coords
    {
        /// <summary>
        /// Self implemented property that stores a grid Row value.
        /// </summary>
        /// <value>Grid Row.</value>
        public int X {get ; set;}

        /// <summary>
        /// Self implemented property that stores a grid Column value.
        /// </summary>
        /// <value>Grid Column.</value>
        public int Y {get ; set;}

        /// <summary>
        /// Constructor, creates a new position.
        /// </summary>
        /// <param name="x">Grid Row.</param>
        /// <param name="y">Grid Column.</param>
        public Coords(int x, int y)
        {
            // Saves the receiving coordinates.
            X = x;
            Y = y;
        }

        /// <summary>
        /// Shows position in a (x, y) format.
        /// </summary>
        /// <returns>String with compact position info.</returns>
        public override string ToString()
        {
            return $"({X,2},{Y,2})";
        }
    }
}