using LangtonAntFrontend.Enums;

namespace LangtonAntFrontend.Classes
{
    public class Ant
    {
        public int X;
        public int Y;
        public int Max;
        public Direction LookDirection;

        public Ant(int maximum, int x, int y, Direction dir)
        {
            X = x;
            Y = y;
            Max = maximum;
            LookDirection = dir;
        }
    }
}