using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LangtonAnt
{
    public class BackendOutViewModel : INotifyPropertyChanged
    {
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public event PropertyChangedEventHandler PropertyChanged;


        public bool IsLoading
        {
            get => isLoading;
            private set
            {
                if (isLoading == value)
                    return;

                isLoading = value;
                OnPropertyChanged();
            }
        }
        private bool isLoading = true;

        public ICommand PrevStepCmd { get; set; }
        public ICommand NextStepCmd { get; set; }

        public ICommand DrawCmd { get; set; }
        public ICommand SaveCmd { get; set; }


        public int IndexCurrentStep
        {
            get => indexCurrentStep;
            set
            {
                if (indexCurrentStep == value)
                    return;

                indexCurrentStep = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentStepDisplay));
            }
        }
        private int indexCurrentStep = 0;


        public string CurrentCode
        {
            get => currentCode;
            set
            {
                if (currentCode == value)
                    return;

                currentCode = value;
                OnPropertyChanged();
            }
        }
        private string currentCode = null;


        public string CurrentStepDisplay => string.Join("/", IndexCurrentStep + 1, gameData?.CountSteps);

        private readonly GameData gameData;

        public BackendOutViewModel(IPageManager pageManager, IGameDataManager gameManager, IFileManager fileManager)
        {
            gameData = gameManager.GameData;
            PrevStepCmd = new Command(() =>
            {
                if (IndexCurrentStep <= 0)
                    return;
                IndexCurrentStep--;
                updateCurrentCode();
            });

            NextStepCmd = new Command(() =>
            {
                if (IndexCurrentStep >= gameData.CountSteps - 1)
                    return;
                IndexCurrentStep++;
                updateCurrentCode();
            });

            void updateCurrentCode()
            {
                CurrentCode = gameData.GetCodeByIndex(IndexCurrentStep);
            }


            DrawCmd = new Command(async () =>
            {
                await pageManager.PushPageAsync(new Frontend());
            });


            SaveCmd = new Command(async () =>
            {
                IsLoading = true;
                await fileManager.SaveGameData(CurrentCode);
                IsLoading = false;
                await pageManager.DisplayToastAsync("Saved.");
            });


            Task.Run(() =>
            {
                gameData.CalculateAllSteps();

                Device.BeginInvokeOnMainThread(() =>
                {
                    IsLoading = false;
                    updateCurrentCode();
                });
            });
        }
    }
}
