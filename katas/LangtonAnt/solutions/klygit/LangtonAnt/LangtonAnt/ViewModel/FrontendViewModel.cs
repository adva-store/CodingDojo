using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LangtonAnt
{
    public class FrontendViewModel : INotifyPropertyChanged
    {
        private static int INIT__MOVE_SPEED_IN_MS => 1;

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
        private bool isLoading = false;


        public string MoveSpeedDisplay => $"{MoveSpeedInMs} ms";

        public int MoveSpeedInMs
        {
            get => moveSpeedInMs;
            set
            {
                if (moveSpeedInMs == value)
                    return;

                moveSpeedInMs = value;
                OnPropertyChanged(nameof(MoveSpeedDisplay));
            }
        }
        private int moveSpeedInMs = INIT__MOVE_SPEED_IN_MS;

        public string DisplayIsAutoPrevStarted => IsAutoPrevStarted ? "Stop" : "<<";
        public string DisplayIsAutoNextStarted => IsAutoNextStarted ? "Stop" : ">>";

        private bool IsAutoNextStarted
        {
            get => isAutoNextStarted;
            set
            {
                if (isAutoNextStarted == value)
                    return;

                isAutoNextStarted = value;
                OnPropertyChanged(nameof(DisplayIsAutoNextStarted));
            }
        }
        private bool isAutoNextStarted = false;


        private bool IsAutoPrevStarted
        {
            get => isAutoPrevStarted;
            set
            {
                if (isAutoPrevStarted == value)
                    return;

                isAutoPrevStarted = value;
                OnPropertyChanged(nameof(DisplayIsAutoPrevStarted));
            }
        }
        private bool isAutoPrevStarted = false;

        public ICommand StartAutoPrevCmd { get; set; }

        public ICommand StartAutoNextCmd { get; set; }

        public ICommand LoadCmd { get; set; }


        public ICommand PrevStepCmd { get; set; }
        public ICommand NextStepCmd { get; set; }

        public string DisplayCurrentStep => string.Join("/", IndexCurrentStep + 1, gameData?.CountSteps);
        public string DisplayAntStartPos => $"G:{gameData?.GridSize} X:{gameData?.AntStartPosX} Y:{gameData?.AntStartPosY} D:{gameData?.AntStartDirection.Code}";

        public int IndexCurrentStep
        {
            get => indexCurrentStep;
            set
            {
                if (indexCurrentStep == value)
                    return;

                indexCurrentStep = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayCurrentStep));
            }
        }
        private int indexCurrentStep = 0;


        internal Action<int> OnIndexCurrentChangedAction { get; set; }

        private readonly GameData gameData;

        public FrontendViewModel(IGameDataManager gameDataManager, IFileManager fileManager)
        {
            gameData = gameDataManager.GameData;

            PrevStepCmd = new Command(() =>
            {
                prevStep(true);
            });

            NextStepCmd = new Command(() =>
            {
                nextStep(true);
            });


            StartAutoPrevCmd = new Command(async () =>
            {
                if (IsAutoPrevStarted)
                {
                    IsAutoPrevStarted = false;
                    return;
                }

                IsAutoNextStarted = false;
                IsAutoPrevStarted = true;
                while (IsAutoPrevStarted)
                {
                    if (IsAutoNextStarted || !prevStep(MoveSpeedInMs > 0))
                    {
                        IsAutoPrevStarted = false;
                        return;
                    }

                    if (MoveSpeedInMs > 0)
                        await Task.Delay(MoveSpeedInMs);
                }
            });

            StartAutoNextCmd = new Command(async () =>
            {
                if (IsAutoNextStarted)
                {
                    IsAutoNextStarted = false;
                    return;
                }

                IsAutoPrevStarted = false;
                IsAutoNextStarted = true;
                while (IsAutoNextStarted)
                {
                    if (IsAutoPrevStarted || !nextStep(MoveSpeedInMs > 0))
                    {
                        IsAutoNextStarted = false;
                        return;
                    }

                    if (MoveSpeedInMs > 0)
                        await Task.Delay(MoveSpeedInMs);
                }
            });


            bool prevStep(bool notifyOnIndexCurrentchanged)
            {
                if (IndexCurrentStep <= 0)
                {
                    OnIndexCurrentChangedAction?.Invoke(IndexCurrentStep);
                    return false;
                }

                IndexCurrentStep--;
                if (notifyOnIndexCurrentchanged)
                    OnIndexCurrentChangedAction?.Invoke(IndexCurrentStep);
                return true;
            }

            bool nextStep(bool notifyOnIndexCurrentchanged)
            {
                if (IndexCurrentStep >= gameData.CountSteps - 1)
                {
                    OnIndexCurrentChangedAction?.Invoke(IndexCurrentStep);
                    return false;
                }

                IndexCurrentStep++;
                if (notifyOnIndexCurrentchanged)
                    OnIndexCurrentChangedAction?.Invoke(IndexCurrentStep);
                return true;
            }


            LoadCmd = new Command(async () =>
            {
                IsLoading = true;
                await load();
                IsLoading = false;
                async Task load()
                {
                    var code = await fileManager.LoadGameCode();
                    if (string.IsNullOrEmpty(code))
                        return;

                    gameData.LoadCode(code);

                    IndexCurrentStep = 0;
                    OnIndexCurrentChangedAction?.Invoke(IndexCurrentStep);
                }
            });
        }
    }
}
