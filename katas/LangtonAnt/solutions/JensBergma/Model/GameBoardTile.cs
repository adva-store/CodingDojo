public class GameBoardTile
    {
        public char Color {get; set;} = 'w';
        public bool IsCurrentPos {get; set;} = false;
        public Compass Direction {get; set;} = Compass.None;
        public int Row {get; set;}
        public int Column {get; set;}
    }