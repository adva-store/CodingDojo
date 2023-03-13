using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StrangeChessboard
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public event PropertyChangedEventHandler PropertyChanged;


        public int TotalWhiteArea
        {
            get => totalWhiteArea;
            set
            {
                if (totalWhiteArea == value)
                    return;

                totalWhiteArea = value;
                OnPropertyChanged();
            }
        }
        private int totalWhiteArea = 0;

        public int TotalBlackArea
        {
            get => totalBlackArea;
            set
            {
                if (totalBlackArea == value)
                    return;

                totalBlackArea = value;
                OnPropertyChanged();
            }
        }
        private int totalBlackArea = 0;


        public int TotalArea
        {
            get => totalArea;
            set
            {
                if (totalArea == value)
                    return;

                totalArea = value;
                OnPropertyChanged();
            }
        }
        private int totalArea = 0;



        public string InputCs
        {
            get => intPutCs;
            set
            {
                if (intPutCs == value)
                    return;

                intPutCs = value;
                OnPropertyChanged();
            }
        }
        private string intPutCs;


        public string InputRs
        {
            get => intPutRs;
            set
            {
                if (intPutRs == value)
                    return;

                intPutRs = value;
                OnPropertyChanged();
            }
        }
        private string intPutRs;

        public MainPageViewModel()
        {
            var cs = new int[] { 3, 1, 2, 7, 1 };
            var rs = new int[] { 1, 8, 4, 5, 2 };

            InputCs = string.Join(", ", cs);
            InputRs = string.Join(", ", rs);

            var result = CalculateArea(cs: cs, rs: rs);
            TotalWhiteArea = result.Item1;
            TotalBlackArea = result.Item2;
            TotalArea = TotalWhiteArea + TotalBlackArea;
        }

        public Tuple<int, int> CalculateArea(int[] cs, int[] rs)
        {
            if (cs.Length != rs.Length)
                throw new InvalidOperationException($"{cs.Length} != {rs.Length}");

            var isWhite = true;

            var totalWhiteArea = 0;
            var totalBlackArea = 0;

            for (var indexRow = 0; indexRow < rs.Length; indexRow++)
            {
                for (var indexColumn = 0; indexColumn < cs.Length; indexColumn++)
                {
                    var rsLoop = rs[indexRow];
                    if (rsLoop <= 0)
                        throw new InvalidOperationException($"{rsLoop} <= 0");

                    var csLoop = cs[indexColumn];
                    if (csLoop <= 0)
                        throw new InvalidOperationException($"{csLoop} <= 0");

                    var area = rsLoop * csLoop;

                    if (isWhite)
                        totalWhiteArea += area;
                    else
                        totalBlackArea += area;

                    isWhite = !isWhite;
                }
            }

            var result = Tuple.Create(totalWhiteArea, totalBlackArea);
            return result;
        }

        public int GetSum(int[] arrValue)
        {
            int returnValue = 0;
            foreach (var valueLoop in arrValue)
            {
                if (valueLoop <= 0)
                    throw new InvalidOperationException($"{valueLoop} <= 0");
                returnValue += valueLoop;
            }
            return returnValue;
        }
    }
}
