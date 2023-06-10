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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using static LangtonAntServerClass.LangtonAntServerRemoteClass;
using LangtonAntServerClass;
using System.Timers;
using Microsoft.Win32;
using System.IO;

namespace LangtonAntClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private int playFieldSize;
		private int numberOfMoves;
		private string myGameId;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void NewGameButton_Click(object sender, RoutedEventArgs e)
		{ 
			NewGameSettingsPopup popup = new NewGameSettingsPopup();
			popup.ShowDialog();


			Type type = typeof(LangtonAntServerRemoteClass);
            remoteServerObj = (LangtonAntServerRemoteClass)Activator.GetObject(type,
            "tcp://localhost:8085/LangtonAntServer");
            if (remoteServerObj == null)
            {
                Console.WriteLine("Could not locate server");
                return;
			}
			playFieldSize = int.Parse(popup.playFieldSizeTextBox.Text);
			numberOfMoves = int.Parse(popup.movesToDoTextBox.Text);
			int antX = int.Parse(popup.antXTextBox.Text);
			int antY = int.Parse(popup.antYTextBox.Text);
			AntOrientation selectedOrientation = GetAntOrientation(((ComboBoxItem)popup.antOrientationComboBox.SelectedItem).Content.ToString());

			myGameId = remoteServerObj.StartNewGame(playFieldSize, antX, antY, selectedOrientation, numberOfMoves);
			currentMove = 1;

			ShowState(currentMove, remoteServerObj.GetGameState(myGameId));
			nextMoveButton.IsEnabled = true;
			saveOutputButton.IsEnabled = true;
			autoAdvanceCheckbox.IsEnabled = true;
			antSteps.Clear();
		}

		private void ShowState(int moveNo, string stateStr)
		{
			if (stateStr == "")
			{
				if (autoAdvanceTimer != null)
				{
					autoAdvanceTimer.Stop();
					autoAdvanceTimer = null;
				}

				return;
			}
			antSteps.Add(stateStr);
			List<string> fields = stateStr.Split(',').ToList();
			string[,] fieldsArr = new string[playFieldSize,playFieldSize];
			(int X, int Y) antPos = (0, 0);
			AntOrientation antOr = AntOrientation.North;
			bool antFound = false;

			// Fill the array of the fields from the "flat" list provided by the server
			for (int x = 0; x < playFieldSize; x++)
			{
				for (int y = 0; y < playFieldSize; y++)
				{
					string field = fields.First();
					fields.RemoveAt(0);
					if (field.Length == 2)
					{
						// Field contains ant position as well
						antFound = true;
						antPos = (x, y);
						antOr = AntOrientationFromChar(field[0]);
						field = field.Remove(0, 1);
					}
					fieldsArr[x,y] = field;
				}
			}

			mainCanvas.Dispatcher.Invoke(new Action(() =>
			{
				moveNumberLabel.Content = "Move number: " + moveNo;

				mainCanvas.Children.Clear();

				DrawField();
				DrawBlackFields(fieldsArr);
				if (antFound)
				{
					DrawAnt(antPos, antOr);
				}
			}));
		}
		private void DrawField()
		{
			double canvasWidth = mainCanvas.ActualWidth;
			double fieldSizePx = canvasWidth / playFieldSize;
			for (int i = 0; i < playFieldSize - 1; i++)
			{
				_ = mainCanvas.Children.Add(new Line
				{
					X1 = (i + 1) * fieldSizePx,
					Y1 = 0,
					X2 = (i + 1) * fieldSizePx,
					Y2 = canvasWidth,
					Stroke = Brushes.Black,
					StrokeThickness = 1
				}
				);
				_ = mainCanvas.Children.Add(new Line
				{
					X1 = 0,
					Y1 = (i + 1) * fieldSizePx,
					X2 = canvasWidth,
					Y2 = (i + 1) * fieldSizePx,
					Stroke = Brushes.Black,
					StrokeThickness = 1
				}
				);
			}
		}

		private readonly ImageBrush AntImageBrush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/LangtonAntClient;component/ant.png")));
		private LangtonAntServerRemoteClass remoteServerObj;
		private bool autoAdvanceEnabled;
		private Timer autoAdvanceTimer;
		private int currentMove;
		private readonly List<string> antSteps = new List<string>();

		private void DrawAnt((int X, int Y) antPos, AntOrientation antOr)
		{
			Console.WriteLine($"Drawing ant @ {antPos.X}, {antPos.Y}");
			double canvasWidth = mainCanvas.ActualWidth;
			double fieldSizePx = canvasWidth / playFieldSize;

			RotateTransform trans = new RotateTransform(GetRotation(antOr))
			{
				CenterX = fieldSizePx / 2,
				CenterY = fieldSizePx / 2
			};

			Canvas myCanvas = new Canvas
			{
				Width = fieldSizePx,
				Height = fieldSizePx,
				Background = AntImageBrush,
			};

			AntImageBrush.Transform = trans;

			mainCanvas.Children.Add(myCanvas);
			Canvas.SetLeft(myCanvas, antPos.Y * fieldSizePx);
			Canvas.SetTop(myCanvas, antPos.X * fieldSizePx);
		}

		private double GetRotation(AntOrientation antOr)
		{
			switch (antOr)
			{
				case AntOrientation.North:
					return 0;
				case AntOrientation.East:
					return 90;
				case AntOrientation.South:
					return 180;
				case AntOrientation.West:
					return 270;
				case AntOrientation.Unknown:
					break;
			}
			return 0;
		}
		private AntOrientation GetAntOrientation(string text)
		{
			switch (text)
			{
				case "North":
					return AntOrientation.North;
				case "East":
					return AntOrientation.East;
				case "South":
					return AntOrientation.South;
				case "West":
					return AntOrientation.West;
				default:
					return AntOrientation.Unknown;
			}
		}


		private void DrawBlackFields(string[,] fieldArr)
		{
			double canvasWidth = mainCanvas.ActualWidth;
			double fieldSizePx = canvasWidth / playFieldSize;
			for (int x = 0; x < playFieldSize; x++)
			{
				for (int y = 0; y < playFieldSize; y++)
				{
					if (fieldArr[x,y] == "s")
					{
						Rectangle r = new Rectangle
						{
							Fill = new SolidColorBrush(Colors.DarkGray),
							Width = fieldSizePx - 2,
							Height = fieldSizePx - 2
						};

						_ = mainCanvas.Children.Add(r);
						Canvas.SetTop(r, x * fieldSizePx + 1);
						Canvas.SetLeft(r, y * fieldSizePx + 1);
					}
				}
			}
		}

		private AntOrientation AntOrientationFromChar(char orChar)
		{
			switch (orChar)
			{
				case 'n':
					return AntOrientation.North;
				case 's':
					return AntOrientation.South;
				case 'o':
					return AntOrientation.East;
				case 'w':
					return AntOrientation.West;
			}
			return AntOrientation.Unknown;
		}

		private void NextMoveButton_Click(object sender, RoutedEventArgs e)
		{
			currentMove++;
			ShowState(currentMove, remoteServerObj.GetNextGameState(myGameId));
		}

		private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (autoAdvanceTimer != null)
			{
				autoAdvanceTimer.Interval = autoAdvanceSpeedSlider.Value * 1000;
			}

			if (autoAdvanceLabel2 != null)
			{
				autoAdvanceLabel2.Content = autoAdvanceSpeedSlider.Value + "s";
			}
		}

		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			autoAdvanceEnabled = autoAdvanceCheckbox.IsChecked ?? false;

			nextMoveButton.IsEnabled = !autoAdvanceEnabled;
			autoAdvanceSpeedSlider.IsEnabled = autoAdvanceEnabled;
			autoAdvanceLabel1.IsEnabled = autoAdvanceEnabled;
			autoAdvanceLabel2.IsEnabled = autoAdvanceEnabled;

			if (autoAdvanceEnabled)
			{
				autoAdvanceTimer = new Timer(autoAdvanceSpeedSlider.Value * 1000)
				{
					AutoReset = true
				};
				autoAdvanceTimer.Elapsed += AutoAdvanceTimer_Elapsed;
				autoAdvanceTimer.Start();
			}
			else
			{
				if (autoAdvanceTimer != null)
				{
					autoAdvanceTimer.Stop();
					autoAdvanceTimer = null;
				}
			}
		}

		private void AutoAdvanceTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (numberOfMoves != 0 && currentMove >= numberOfMoves)
			{
				autoAdvanceTimer.Stop();
				autoAdvanceTimer = null;
				return;
			}
			currentMove++;
			ShowState(currentMove, remoteServerObj.GetNextGameState(myGameId));
		}

		private void SaveOutputButton_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			if (saveFileDialog.ShowDialog() == true)
			{
				File.WriteAllText(saveFileDialog.FileName, String.Join("\r\n", antSteps));
			}
		}
	}
}
