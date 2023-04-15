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
	public int Moves => _antField.Moves;
	public string Direction => _antField.Direction.GetAsString();

	private async Task RunAnt()
	{
		if (_cts.IsCancellationRequested)
			return;

		await Task.Delay(_speed);

		try
		{
			if (!_antField.Move())
			{
				// wall reached
				Status = "Ant stopped";
				return;
			}
		}
		finally
		{
			Refresh();
		}

		_ = Task.Run(RunAnt);
	}

	private void Refresh()
	{
		MainThread.BeginInvokeOnMainThread(() =>
		{
			OnPropertyChanged(nameof(Drawable));
			OnPropertyChanged(nameof(Moves));
			OnPropertyChanged(nameof(Direction));
		});
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

	async void OnSaveButtonClicked(System.Object sender, System.EventArgs e)
	{
		var path = Path.Combine(FileSystem.CacheDirectory, "ant.txt");
		_antField.SaveToFile(path);
		await Share.RequestAsync(new ShareFileRequest(new ShareFile(path, "text/plain")));
	}
}
