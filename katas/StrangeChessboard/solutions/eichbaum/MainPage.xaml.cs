namespace StrangeChessboard;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		CreateChessboard();
	}

	private Chessboard.Chessboard Chessboard;
	private void CreateChessboard()
	{
		Chessboard = new(Columns, Rows);
		OnPropertyChanged(nameof(ChessboardHeight));
		OnPropertyChanged(nameof(ChessboardDrawable));
		OnPropertyChanged(nameof(ChessboardTotalArea));
	}

	public int ChessboardWidth => Chessboard.Width;
	public int ChessboardHeight => Chessboard.Height;
	public Chessboard.Drawable ChessboardDrawable => Chessboard.Drawable;
	public Tuple<int, int> ChessboardTotalArea => Chessboard.TotalArea;

	private string _Columns = "3, 1, 2, 7, 1";
	public string Columns
	{
		get => _Columns;
		set
		{
			_Columns = value;
			OnPropertyChanged(nameof(Columns));
			CreateChessboard();
		}
	}

	private string _Rows = "1, 8, 4, 5, 2";
	public string Rows
	{
		get => _Rows;
		set
		{
			_Rows = value;
			OnPropertyChanged(nameof(Rows));
			CreateChessboard();
		}
	}
}