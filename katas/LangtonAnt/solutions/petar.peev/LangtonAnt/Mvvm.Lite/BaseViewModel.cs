using System.Windows.Input;
namespace LangtonAnt.Mvvm.Lite;
/// <summary>
/// Abstarct base-klasse für MVVM-ViewModels
/// </summary>
public abstract class BaseViewModel : NotificationBase
{
    private bool _isRefreshing = false;
    private bool _isBusy = false;
    private string _title;

    public bool IsRefreshing
    {
        get => _isRefreshing;
        protected set
        {
            if (_isRefreshing == value)
                return;

            _isRefreshing = value;
            RaisePropertyChanged(nameof(IsRefreshing));
        }
    }
    public bool IsBusy
    {
        get => _isBusy;
        protected set
        {
            if (_isBusy == value)
                return;

            _isBusy = value;

            RaisePropertyChanged(nameof(IsBusy));
        }
    }
    public string Title
    {
        get => _title;
        set {
            if(_title != value)
            {
                _title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }
    }
    public ICommand LoadDataCommand { get; protected set; }
}
