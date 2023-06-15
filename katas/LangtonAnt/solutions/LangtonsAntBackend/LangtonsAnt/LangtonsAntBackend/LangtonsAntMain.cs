using System.ComponentModel.DataAnnotations;
using static LangtonsAntAPI.LangtonsAntBackend.Statics;

namespace LangtonsAntAPI.LangtonsAntBackend
{
    public class LangtonsAntMain
    {
        #region Properties & Fields
        #region StartParams
        public int EdgeLength { get; set; }

        public int NumberOfSteps { get; set; }

        public int StartX { get; set; }

        public int StartY { get; set; }

        public string StartDirection { get; set; }

        #endregion StartParams

        private bool[,]? _field; //true = black, false = white

        private Ant? _ant;
        public int StepCounter { get; set; }

        public string ResultText { get; set; }

        public string ErrMessage { get; set; }
        #endregion Properties

        #region Methods



        public void Initialize()
        {
            //Todo: Check Parameters first 
            _field = new bool[EdgeLength, EdgeLength];
            _ant = new Ant(iPositionX: StartX, iPositionY: StartY, iDirection: StartDirection);
            StepCounter = 0;
            GenerateErrorText();
            GenerateResultString();
        }


        public void NextStep()
        {
            if (_ant == null || _field == null)
                return;

            GenerateErrorText();
            if (!String.IsNullOrEmpty(ErrMessage))
                return;

            bool isBlack = _field[_ant.PositionX, _ant.PositionY];

            //Invert Square Color
            _field[_ant.PositionX, _ant.PositionY] = !_field[_ant.PositionX, _ant.PositionY];

            //Move Ant
            if (isBlack)
            {
                _ant.Move(ETurn.Left);
            }
            else
            {
                _ant.Move(ETurn.Right);
            }

            StepCounter++;
            GenerateResultString();
        }

        public void GenerateResultString()
        {
            if (_ant == null || _field == null)
                return;
            ResultText = String.Empty;
            for (int y = 0; y < EdgeLength; y++)
            {
                for (int x = 0; x < EdgeLength; x++)
                {
                    //Write Ant
                    if (_ant.PositionX == x && _ant.PositionY == y)
                    {
                        ResultText += _ant.Direction;
                    }
                    //Write Field Color
                    if (_field[x, y])
                    {
                        ResultText += "s,";
                    }
                    else
                    {
                        ResultText += "w,";
                    }

                }
            }
        }


        private void GenerateErrorText()
        {
            ErrMessage = String.Empty;
            if (IsOutOfBounds())
            {
                ErrMessage = "Der Rand wurde erreicht!";
            }

            if (IsOver())
            {
                ErrMessage += "Die finale Anzahl der Schritte wurde erreicht!";
            }

        }

        public bool IsOutOfBounds()
        {
            if (_ant == null)
                return false;
            if (_ant.PositionX >= EdgeLength || _ant.PositionY >= EdgeLength || _ant.PositionX < 0 || _ant.PositionY < 0)
                return true;
            return false;
        }
        private bool IsOver()
        {
            if (StepCounter >= NumberOfSteps)
                return true;
            return false;
        }



        #endregion Methods

    }
}
