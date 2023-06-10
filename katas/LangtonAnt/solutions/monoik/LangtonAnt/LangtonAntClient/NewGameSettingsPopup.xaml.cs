using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LangtonAntClient
{
	/// <summary>
	/// Interaction logic for NewGameSettingsPopup.xaml
	/// </summary>
	public partial class NewGameSettingsPopup : Window
	{
		public NewGameSettingsPopup()
		{
			InitializeComponent();
		}

		private void StartGameButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
