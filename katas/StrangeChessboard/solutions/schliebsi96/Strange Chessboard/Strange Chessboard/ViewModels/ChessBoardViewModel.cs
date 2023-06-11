using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace Strange_Chessboard.ViewModels
{
    public class ChessBoardViewModel : ViewModelBase
    {
        public ICommand CalculateWoodCommand { get; set; }

        private string _cs = "test";

        public string CS
        {
            get => _cs;
            set
            {
                _cs = value;
                OnPropertyChanged(nameof(CS));
            }
        }

        private string _rs;

        public string RS
        {
            get => _rs;
            set
            {
                _rs = value;
                OnPropertyChanged(nameof(RS));
            }
        }

        private string _output;

        public string Output
        {
            get => _output;
            set 
            { 
                _output = value;
                OnPropertyChanged(nameof(Output));
            }
        }

        public ChessBoardViewModel()
        {
            CalculateWoodCommand = new Command(() => CalculateWood());
        }

        public void CalculateWood()
        {
            List<int> CSNumbers = new List<int>();
            List<int> RSNumbers = new List<int>();

            if (CS == null || RS == null)
            {
                Output = "At least enter something";
            }
            else
            {

                string[] csNumbers = Regex.Split(CS, @"\D+");
                foreach (string value in csNumbers)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        int i = int.Parse(value);
                        CSNumbers.Add(i);
                    }
                }

                string[] rsNumbers = Regex.Split(RS, @"\D+");
                foreach (string value in rsNumbers)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        int i = int.Parse(value);
                        RSNumbers.Add(i);
                    }
                }

                if (CSNumbers.Count != RSNumbers.Count)
                {
                    Output = "Please enter the same amount of rows and columns";
                }

                var lastRowWasBlack = true;
                bool isBlack;
                var totalBlack = 0;
                var totalWhite = 0;
                foreach (var csNumber in CSNumbers)
                {
                    isBlack = lastRowWasBlack;
                    foreach (var rsNumber in RSNumbers)
                    {
                        isBlack = !isBlack;
                        if (isBlack)
                        {
                            totalBlack = totalBlack + csNumber * rsNumber;
                        }
                        else
                        {
                            totalWhite = totalWhite + csNumber * rsNumber;
                        }
                    }
                    lastRowWasBlack = !lastRowWasBlack;
                }
                Output = "(" + totalWhite + "," + totalBlack + ")";
            }
        }
    }
}
