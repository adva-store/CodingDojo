using StrangeChessboard.StrangeChessboard;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace StrangeChessboard;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		var test = new Solution();
		test.Initialize();
		var result = test.Calculate();
		ShowResult(test, result);
	}

    private void BoardBtn_Generic_Clicked(object sender, EventArgs e)
    {
		int.TryParse(this.Range.Text, out var range);
		if(range > 0) {
			try
			{
                var test = new Solution();
                test.Initialize(range);
                var result = test.Calculate();
                ShowResult(test, result);
            }
			catch (Exception ex)
			{
                labelResult.Text = ex.Message;
                labelCheck.Text = "";
                labelColumns.Text = "";
                labelRows.Text = "";
            }
		}
    }

	private void ShowResult(Solution test, (int,int) result)
	{
		labelResult.Text = $"{result.Item1.ToString()} + {result.Item2.ToString()} = {(result.Item1 + result.Item2).ToString()}";
		labelCheck.Text = (test.ColumnItems.ToArray().Sum() * test.RowItems.ToArray().Sum()).ToString();
		labelColumns.Text = $"[{string.Join(",", test.ColumnItems)}]";
        labelRows.Text = $"[{string.Join(",", test.RowItems)}]";
    }
}

