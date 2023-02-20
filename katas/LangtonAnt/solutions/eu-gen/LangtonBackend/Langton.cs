namespace LangtonBackend
{
    internal class Langton
    {
        //Definition: Höhe = width und Breite = width
        private readonly int _width;
        private readonly int _height;

        private Position _antCurrentPosition;
        private Char _antDirection;
        private static Field _field { get; set; }

        public Position AntCurrentPosition { get { return _antCurrentPosition; } set { _antCurrentPosition = AntCurrentPosition; } }
        public Char AntDirection{ get {return _antDirection;} }

        /// <summary>
        /// Hauptklasse für Langton Ant Simulator. 
        /// Initialisiert auch Feld und Startposition.
        /// </summary>
        /// <param name="width">Kantenlänge des Feldes</param>
        /// <param name="startX">Startkoordinate waagrecht</param>
        /// <param name="startY">Startkoordinate senkrecht</param>
        /// <param name="direction">Blickrichtung zum Start</param>
        public Langton(int width, int startX, int startY, char direction )
        {
            _width = width;
            _height = width;
            _antDirection = direction;  
            _antCurrentPosition = new(startX, startY);
            _field = new Field(width, width);
        }

        /// <summary>
        /// Ant bewegen und Feld färben, Direction ermitteln und setzen
        /// </summary>
        internal Position Step()
        {

            //Zustand vor Step
            Position positionNow = _field.GetPosition(_antCurrentPosition.X , _antCurrentPosition.Y );
            bool turnLeft = positionNow.IsBlack;
            Char currentDirection = _antDirection;

            //Step Actions
            Char newDirection = GetNewDirection(currentDirection, turnLeft);
            positionNow.IsBlack = !positionNow.IsBlack;
            Move(newDirection); //_antCurrentPosition und _antDirection werden auch gesetzt

            //neue Position returnen
            return _antCurrentPosition;
        }

        /// <summary>
        /// Je nach Richtung die X- oder Y-Koordinate ändern, 
        /// Blickrichtung für Ant setzen.
        /// </summary>
        /// <param name="direction"></param>
        private void Move(Char direction)
        {
            switch (direction)
            {
                case 'n':
                    _antCurrentPosition.Y += 1;
                     break;
                case 's':
                    _antCurrentPosition.Y += -1;
                    break;
                case 'o':
                    _antCurrentPosition.X += 1;
                    break;
                case 'w':
                    _antCurrentPosition.X += -1;
                    break;
            }

            _antDirection= direction;
        }

        /// <summary>
        /// Bekomme aktuelle Richtung und Drehrichtung, returne neue Richtung
        /// Richtungen: n,o,s,w
        /// </summary>
        /// <param name="currentDirection">Die aktuelle Richtung</param>
        /// <param name="turnLeft">true = Rotation nach links</param>
        /// <returns></returns>
        private char GetNewDirection(char currentDirection, bool turnLeft)
        {
            if (turnLeft) 
            {
                switch (currentDirection)
                {
                    case 'n':
                        return 'w';
                    case 's':
                        return 'o';
                    case 'o':
                        return 'n';
                    case 'w':
                        return 's';
                }
            }
            else 
            {
                switch (currentDirection)
                {
                    case 'n':
                        return 'o';
                    case 'o':
                        return 's';
                    case 's':
                        return 'w';
                    case 'w':
                        return 'n';
                }
            }

            return ' '; //egtl. unreachable
        }

        /// <summary>
        /// Gesamtes Feld in Console ausgeben.
        /// Dauert bei größeren Werten ganz schön lange, daher optional gestaltet.
        /// </summary>
        internal void PrintField()
        {
            Console.Write("(");
            for (int iRow = 0; iRow < _field.Height; iRow++)
            {
                for (int iCol = 0; iCol < _field.Width; iCol++)
                {
                    Position pos = _field.GetPosition(iCol, iRow);
                    if (pos.X == _antCurrentPosition.X && pos.Y == _antCurrentPosition.Y)
                    {
                        Console.Write(_antDirection);
                    }
                    Console.Write(_field.GetPosition(iCol, iRow).IsBlack ? "s" : "w");
                    Console.Write(",");
                }
                Console.WriteLine();
            }
            Console.WriteLine(")");
        }

        /// <summary>
        /// Aktuellen Feldzustand in Datei schreiben (append)
        /// Datei im Verzeichnis der Binaries mit Name Lanton[GUID].txt
        /// Klammern als Begrenzungen des Feldes.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        internal async Task SaveFieldAsync(Guid guid)
        {
            string[] lines = new string[_field.Height]; 
            
            for (int iRow = 0; iRow < _field.Height; iRow++)
            {
                string row = string.Empty;

                if (iRow == 0) { row = "("; }

                for (int iCol = 0; iCol < _field.Width; iCol++)
                {
                    Position pos = _field.GetPosition(iCol, iRow);
                    if (pos.X == _antCurrentPosition.X && pos.Y == _antCurrentPosition.Y)
                    {
                        row += _antDirection;
                    }
                    row += _field.GetPosition(iCol, iRow).IsBlack ? "s" : "w";
                    row += ",";
                }

                if (iRow == _field.Height -1) { row += ")"; }
                lines[iRow] = row;
            }

            using StreamWriter file = new("Langton" + guid.ToString() +  ".txt", append: true);
            foreach (string line in lines)
            {
                await file.WriteLineAsync(line);  
            }

        }

        /// <summary>
        /// Prüfen, ob der nächste Step die Feldgrenzen überschreiten würde
        /// </summary>
        /// <param name="antCurrentPosition">aktuelle Position</param>
        /// <returns></returns>
        public bool CanStep()
        {
            if (_antCurrentPosition.X < 0
                || (_antCurrentPosition.X == 0 && _antDirection == 'w')
                || _antCurrentPosition.Y < 0
                || (_antCurrentPosition.Y == this._height && _antDirection == 's')
                ) 
            {
                return false;
            }

            return true;
        }
    }
}
