using LangtonAnt.Library;

namespace LangtonAnt.MAUI;

public partial class AntPage : ContentPage
{
	private CancellationTokenSource _cts;
	private AntField _antField;
	private int _speed;

	public AntPage(int speed, AntField antField)
	{
		_cts = new();
		_speed = speed;
		_antField = antField;
		InitializeComponent();
		Status = "Ant is working";
		Task.Run(RunAnt);
	}

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		_cts.Cancel();
	}

	public Ant.AntDrawable Drawable => new(_antField.Size, _antField.ToString());
	public string GridString => _antField.ToString();

	private async Task RunAnt()
	{
		if (_cts.IsCancellationRequested)
			return;

		await Task.Delay(_speed);

		if (!_antField.Move())
		{
			// wall reached
			Status = "Ant stopped";
			return;
		}

		MainThread.BeginInvokeOnMainThread(() =>
		{
			OnPropertyChanged(nameof(Drawable));
			OnPropertyChanged(nameof(GridString));
		});

		_ = Task.Run(RunAnt);
	}

	private string _Status;
	public string Status
	{
		get => _Status;
		set
		{
			_Status = value;
			OnPropertyChanged(nameof(Status));
		}
	}
}
