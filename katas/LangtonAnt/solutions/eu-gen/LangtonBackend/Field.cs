namespace LangtonBackend
{
    /// <summary>
    /// Das gesamte Feld, auf dem sich die Ameise bewegt.
    /// </summary>
    internal class Field
    {
        Position[,] positions;

        private int _width;
        private int _height;

        public int Width { get { return _width; } }
        public int Height { get { return _height; } }

        public Field(int width, int height) 
        { 
            _width= width;
            _height = height;

            //Feld erstellen
            positions = new Position[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    positions[x, y] = new Position( x, y, this);
                }
            }
        }

        public Position GetPosition(int x, int y)
        {
            // Grenzen prüfen
            if (x > _width || x < 0 || y > _height || y < 0)
            {
                return null;
            }
            return positions[x, y];
        }
    }
}
