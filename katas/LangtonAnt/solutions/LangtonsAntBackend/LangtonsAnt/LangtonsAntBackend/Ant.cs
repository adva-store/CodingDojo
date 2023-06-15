using static LangtonsAntAPI.LangtonsAntBackend.Statics;

namespace LangtonsAntAPI.LangtonsAntBackend
{
    public class Ant
    {
        public Ant(int iPositionX, int iPositionY, string iDirection)
        {
            PositionX = iPositionX;
            PositionY = iPositionY;
            Direction = (EDirection)Enum.Parse(typeof(EDirection), iDirection);

        }

        public int PositionX { get; private set; }

        public int PositionY { get; private set; }

        public EDirection Direction { get; private set; }


        public void Move(ETurn iTurn)
        {
            //set new direction
            if (iTurn == ETurn.Left)
            {
                if (Direction == EDirection.n)
                {
                    Direction = EDirection.w;
                }
                else
                {
                    Direction--;

                }

            }
            else if (iTurn == ETurn.Right)
            {
                if (Direction == EDirection.w)
                {
                    Direction = EDirection.n;
                }
                else
                {
                    Direction++;
                }
            }

            //set new Position
            switch (Direction)
            {
                case EDirection.n:
                    PositionY--;
                    break;
                case EDirection.o:
                    PositionX++;
                    break;
                case EDirection.s:
                    PositionY++;
                    break;
                case EDirection.w:
                    PositionX--;
                    break;
            }

        }


    }
}
