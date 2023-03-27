using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StrangeChessboard.ViewModels
{
    public class StrangeChessboardPageViewModel : BaseViewModel
    {
        private string columnNumbers;
        public string ColumnNumbers
        {
            get { return columnNumbers; }
            set { SetProperty(ref columnNumbers, value); }
        }

        private string rowNumbers;
        public string RowNumbers
        {
            get { return rowNumbers; }
            set { SetProperty(ref rowNumbers, value); }
        }

        private int totalWhiteArea;
        public int TotalWhiteArea
        {
            get { return totalWhiteArea; } 
            set { SetProperty(ref totalWhiteArea, value); }
        }

        private int totalBlackArea;
        public int TotalBlackArea
        {
            get { return totalBlackArea; }
            set { SetProperty(ref totalBlackArea, value); }
        }

        private int totalArea;
        public int TotalArea
        {
            get { return totalArea; }
            set { SetProperty(ref totalArea, value); }
        }

        public List<int> CS { get; set; }
        public List<int> RS { get; set; }

        public ICommand GenerateNewChessboardCommand { get; private set; }

        Random random;

        public StrangeChessboardPageViewModel()
        {
            random = new Random();
            GenerateNewChessboardCommand = new Command(_GenerateNewChessBoard);

        }

        private void _GenerateNewChessBoard(object sender)
        {
            var boardNumbers =  GetChessboard();
            CS = boardNumbers.Item1;
            RS = boardNumbers.Item2;
            ColumnNumbers = string.Join(",", CS);
            RowNumbers = string.Join(",", RS);
            var calculatedAreas = CalculateStrangeChess.CalculateChessboardColorAreas(CS, RS);
            TotalWhiteArea = calculatedAreas.Item1;
            TotalBlackArea = calculatedAreas.Item2;
            TotalArea = TotalWhiteArea + TotalBlackArea;
            MessagingCenter.Send(this, "GenerateBoard");

        }

        public Tuple<List<int>, List<int>> GetChessboard()
        {
            List<int> cs = new List<int>();
            List<int> rs = new List<int>();

            int numOfCells = random.Next(4, 7);
            for (int i = 0; i < numOfCells; i++)
            {
                cs.Add(random.Next(1, 9));
                rs.Add(random.Next(1, 9));
            }

            return Tuple.Create(cs, rs);

        }
    }
}
