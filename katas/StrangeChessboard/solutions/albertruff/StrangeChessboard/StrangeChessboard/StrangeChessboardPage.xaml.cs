using StrangeChessboard.ViewModels;

namespace StrangeChessboard;

public partial class StrangeChessboardPage : ContentPage
{
	private StrangeChessboardPageViewModel pageVM;
	public StrangeChessboardPage()
	{
		InitializeComponent();
		pageVM = new StrangeChessboardPageViewModel();
		BindingContext = pageVM;
		

	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		MessagingCenter.Instance.Subscribe<StrangeChessboardPageViewModel>(this, "GenerateBoard", _GenerateBoard);
        pageVM.GenerateNewChessboardCommand?.Execute(pageVM);
    }

	protected override void OnDisappearing() 
	{  
		base.OnDisappearing();
		MessagingCenter.Instance.Unsubscribe< StrangeChessboardPageViewModel>(this, "GenerateBoard");
	}

	private void _GenerateBoard(StrangeChessboardPageViewModel sender)
	{
		if (!_BoardFitInArea(chessBorder, pageVM.CS, pageVM.RS)) { 
			chessBorder.Content = new Label()
			{
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				Text = "Das generierte Board, passt nicht aufs UI-Feld",
				FontAttributes = FontAttributes.Bold,
				FontSize = 20
			};
		}
		else
		{
			chessBorder.Content = _GenerateUIBoard(pageVM.CS, pageVM.RS);
		}
	}

	private VerticalStackLayout _GenerateUIBoard(List<int> cs, List<int> rs)
	{
		var sb = new VerticalStackLayout() { Spacing = 1};
		//int cellCount = 0;
		//Color backColor = Colors.White;
		//foreach (int row in rs) 
		//{
  //          var rowStack = new HorizontalStackLayout() { Spacing = 1};

  //          foreach (int col in cs)
		//	{
  //              if (cellCount % 2 == 0)
  //              {
		//			backColor = Colors.White;
		//		}
		//		else
		//		{
		//			backColor = Colors.Black;
		//		}
		//		cellCount++;

  //              rowStack.Add(new BoxView
  //              {
  //                  HeightRequest = row * 10 ,
  //                  WidthRequest = col * 10,
		//			Color = backColor,

  //              });
		//	}
		//	sb.Add(rowStack);
		//}
		return sb;
	}

	private bool _BoardFitInArea(Border area, List<int> cs, List<int> rs)
	{
		int generatedWidth = cs.Sum(x => x*10);
		int generatedHeight = rs.Sum(x => x*10);
		return generatedWidth <= area.WidthRequest && generatedHeight <= area.HeightRequest;
	}
}