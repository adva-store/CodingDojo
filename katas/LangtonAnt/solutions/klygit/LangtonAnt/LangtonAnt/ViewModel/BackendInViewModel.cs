using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LangtonAnt.Res.Str;

namespace LangtonAnt
{
    public class BackendInViewModel : INotifyPropertyChanged
    {
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public event PropertyChangedEventHandler PropertyChanged;

        private static int INIT__GRID_SIZE => 70;
        private static int INIT__COUNT_STEP => 10800;

        private static int INIT__ANT_POS_X => INIT__GRID_SIZE / 2;
        private static int INIT__ANT_POS_Y => INIT__GRID_SIZE / 2;
        private static GameData.AntDirectionValue INIT__ANT_DIRECTION => GameData.AntDirectionValue.West;


        public string PageTitle => StrResBackendIn.PageTitle;

        public string DisplayGridSize => StrResBackendIn.DisplayGridSize;
        public string DisplayCountSteps => StrResBackendIn.DisplayCountSteps;
        public string DisplayStartPosAnt => StrResBackendIn.DisplayStartPosAnt;
        public string DisplayStartPosXAnt => StrResBackendIn.DisplayStartPosXAnt;
        public string DisplayStartPosYAnt => StrResBackendIn.DisplayStartPosYAnt;
        public string DisplayStartDirectionAnt => StrResBackendIn.DisplayStartDirectionAnt;
        public string DisplayStartBtn => StrResBackendIn.DisplayStartBtn;


        private string ErrMsgInvalidGridSize => StrResBackendIn.ErrMsgInvalidGridSize;
        private string ErrMsgInvalidCountSteps => StrResBackendIn.ErrMsgInvalidCountSteps;
        private string ErrMsgInvalidAntStartPosX => StrResBackendIn.ErrMsgInvalidAntStartPosX;
        private string ErrMsgInvalidAntStartPosY => StrResBackendIn.ErrMsgInvalidAntStartPosY;


        public int GridSize { get; set; } = INIT__GRID_SIZE;


        public int CountSteps { get; set; } = INIT__COUNT_STEP;

        public int AntPosX { get; set; } = INIT__ANT_POS_X;
        public int AntPosY { get; set; } = INIT__ANT_POS_Y;

        private GameData.AntDirectionValue AntDirection
        {
            get => antDirection;
            set
            {
                if (antDirection == value)
                    return;

                antDirection = value;
                OnPropertyChanged(nameof(IsAntDirNorthSelected));
                OnPropertyChanged(nameof(IsAntDirEastSelected));
                OnPropertyChanged(nameof(IsAntDirSouthSelected));
                OnPropertyChanged(nameof(IsAntDirWestSelected));
            }
        }
        private GameData.AntDirectionValue antDirection = INIT__ANT_DIRECTION;

        public ICommand SetAntDirectionNorthCmd { get; private set; }
        public ICommand SetAntDirectionEastCmd { get; private set; }
        public ICommand SetAntDirectionSouthCmd { get; private set; }
        public ICommand SetAntDirectionWestCmd { get; private set; }

        public bool IsAntDirNorthSelected => AntDirection == GameData.AntDirectionValue.North;
        public bool IsAntDirEastSelected => AntDirection == GameData.AntDirectionValue.East;
        public bool IsAntDirSouthSelected => AntDirection == GameData.AntDirectionValue.South;
        public bool IsAntDirWestSelected => AntDirection == GameData.AntDirectionValue.West;

        public ICommand StartCmd { get; private set; }

        public BackendInViewModel(IPageManager pageManager, IGameDataManager gameManager)
        {
            SetAntDirectionNorthCmd = new Command(() => AntDirection = GameData.AntDirectionValue.North);
            SetAntDirectionEastCmd = new Command(() => AntDirection = GameData.AntDirectionValue.East);
            SetAntDirectionSouthCmd = new Command(() => AntDirection = GameData.AntDirectionValue.South);
            SetAntDirectionWestCmd = new Command(() => AntDirection = GameData.AntDirectionValue.West);


            StartCmd = new Command(async () =>
            {
                if (GridSize <= 0)
                {
                    await pageManager.DisplayToastAsync(ErrMsgInvalidGridSize);
                    return;
                }

                if (CountSteps <= 0)
                {
                    await pageManager.DisplayToastAsync(ErrMsgInvalidCountSteps);
                    return;
                }

                if (AntPosX < 0 || AntPosX >= GridSize)
                {
                    await pageManager.DisplayToastAsync(ErrMsgInvalidAntStartPosX);
                    return;
                }

                if (AntPosY < 0 || AntPosY >= GridSize)
                {
                    await pageManager.DisplayToastAsync(ErrMsgInvalidAntStartPosY);
                    return;
                }

                gameManager.GameData = new GameData(gridSize: GridSize, countSteps: CountSteps, antPosX: AntPosX, antPosY: AntPosY, antDirection: AntDirection);
                await pageManager.PushPageAsync(new BackendOut());
            });
        }
    }
}
