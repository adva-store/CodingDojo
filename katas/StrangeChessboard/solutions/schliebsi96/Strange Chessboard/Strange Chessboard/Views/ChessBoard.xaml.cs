
using Xamarin.Forms.Xaml;
using Strange_Chessboard.ViewModels;

namespace Strange_Chessboard.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChessBoard
    {
        public ChessBoard()
        {
            InitializeComponent();

            BindingContext = new ChessBoardViewModel();

        }
    }
}