using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Interop;
using System.Windows.Threading;
using System.Timers;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace LangtonAntProject
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int gridSize;
        private string path;
        const int cellSize = 30;
        public MainWindow()
        {
            InitializeComponent();
        }

 
        private void InitGrid(object sender, RoutedEventArgs e)
        {
            var startPosition_x = 0;
            var startPosition_y = 0;
            var iterationCount = 0;
            var direction = LangtonAntProject.Direction.n;

            int.TryParse(this.GridSize.Text, out gridSize);
            int.TryParse(this.StartPosition_x.Text, out startPosition_x);
            int.TryParse(this.StartPosition_y.Text, out startPosition_y);
            int.TryParse(this.IterationCount.Text, out iterationCount);

            switch (this.Direction.Text.ToLower().Trim())
            {
                case "n":
                    direction = LangtonAntProject.Direction.n; break;
                case "s":
                    direction = LangtonAntProject.Direction.s; break;
                case "o":
                    direction = LangtonAntProject.Direction.o; break;
                case "w":
                    direction = LangtonAntProject.Direction.w; break;
            }

            if (gridSize <= 0)
            {
                MessageBox.Show("Kantenlänge muss großer als 0 sein.");
                return;
            }
            if ( startPosition_x >= gridSize || startPosition_y >= gridSize || startPosition_x < 0 || startPosition_y < 0)
            {
                MessageBox.Show($"Startposition x und Startposition y müssen zwischen 0 und {gridSize-1} sein.");
                return;
            }
            
            InitGridCells();
            InitAntPosition(this.Direction.Text.ToLower().Trim(), startPosition_x, startPosition_y);

            var langtonAnt = new LangtonAnt(gridSize, direction, startPosition_x, startPosition_y, iterationCount);
            path = langtonAnt.Start();

            MessageBox.Show("Spielfeld ist initialisiert");
        }
        //initialize the grid with white cells
        private void InitGridCells()
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    var rectangle = new System.Windows.Shapes.Rectangle
                    {
                        Width = cellSize,
                        Height = cellSize,
                        Fill = System.Windows.Media.Brushes.White,
                        Stroke = System.Windows.Media.Brushes.Black,
                        StrokeThickness = 1,
                        Name = $"Rect_{i}_{j}"
                    };
                    Canvas.SetLeft(rectangle, i * cellSize);
                    Canvas.SetTop(rectangle, j * cellSize);
                    Canvas.Children.Add(rectangle);
                }
            }
        }
        private void InitAntPosition(string direction,int x,int y)
        {
            var directionText = "";
            if (direction == "n") directionText = "^";
            else if (direction == "o") directionText = "->";
            else if (direction == "s") directionText = char.ConvertFromUtf32(8595);
            else if (direction == "w") directionText = "<-";
            else directionText = "";

            var rect = new System.Windows.Shapes.Rectangle
            {
                Width = cellSize,
                Height = cellSize,
                Fill = System.Windows.Media.Brushes.White,
                Stroke = System.Windows.Media.Brushes.Black,
                StrokeThickness = 1,
                Name = $"Rect_{x}{y}"
            };
            Canvas.SetLeft(rect, x * cellSize);
            Canvas.SetTop(rect, y * cellSize);
            ;
            //draw the new one
            Canvas.Children.Add(rect);

            //draw the ant
            if (!string.IsNullOrEmpty(directionText))
                DrawTextOnRectange(rect, directionText);
        }

        private async void StartDrawing(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                MessageBox.Show("Klicken Sie bitte auf dem initialisieren button.");

                return;
            }

            int speed = 10;
            int.TryParse(this.Speed.Text, out speed);
            if(speed<0 || speed > 100)
            {
                MessageBox.Show("Geschwindigkeit muss zwischen 0 und 100 sein.");
                return;
            }

            using (StreamReader stream = new StreamReader(path))
            {
                Dispatcher d = Application.Current.Dispatcher;

                while (true)
                {
                    var line = stream.ReadLine();
                    await Application.Current.Dispatcher.InvokeAsync(new Action(() =>
                    {
                        DrawGrid(line);

                    }), DispatcherPriority.Background);

                    Thread.Sleep(1000-(speed*10));

                    if (string.IsNullOrEmpty(line)) break;
                }
               
                MessageBox.Show("Fertig");

            }
        }
        
        //draw an iteration 
        private void DrawGrid(string line)
        {
            if (string.IsNullOrEmpty(line)) return;

            var matchField = line.Split(',');
            var direction = "";
            var directionText = "";
            int x = 0;
            int y = 0;
            foreach (var field in matchField)
            {

                direction = field.Length == 2 ? field[0].ToString() : "";

                if (direction == "n") directionText = "^";
                else if (direction == "o") directionText = "->";
                else if (direction == "s") directionText = char.ConvertFromUtf32(8595);
                else if (direction == "w") directionText = "<-";
                else directionText = "";

                var rectName = $"Rect_{x}_{y}";

                //find the rectange using its name
                var currentRect = Canvas.Children.OfType<System.Windows.Shapes.Rectangle>().FirstOrDefault(r => r.Name == rectName) as System.Windows.Shapes.Rectangle;

                if (currentRect != null)
                {
                    //copy the current rectangle info
                    var rect = new System.Windows.Shapes.Rectangle
                    {
                        Width = currentRect.Width,
                        Height = currentRect.Height,
                        Fill = currentRect.Fill,
                        Stroke = currentRect.Stroke,
                        StrokeThickness = currentRect.StrokeThickness,
                        Name = currentRect.Name
                    };
                    Canvas.SetLeft(rect, x * cellSize);
                    Canvas.SetTop(rect, y * cellSize);

                    //remove the old rectangle
                    Canvas.Children.Remove(currentRect);
                    //draw the new one
                    Canvas.Children.Add(rect);
                    //change color
                    switch (field.Length == 2 ? field[1].ToString() : field)
                    {
                        case "s":
                            rect.Fill = System.Windows.Media.Brushes.Black;
                            break;
                        case "w":
                            rect.Fill = System.Windows.Media.Brushes.White;
                            break;
                    }
                    //draw the ant
                    if (!string.IsNullOrEmpty(directionText))
                        DrawTextOnRectange(rect, directionText);


                }

                if (y < gridSize - 1)
                {
                    y++;
                }
                else
                {
                    y = 0;
                    x++;
                }
            }


        }
        //draw the ant
        private void DrawTextOnRectange(System.Windows.Shapes.Rectangle rect, string directionText)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using(DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                FormattedText ft = new FormattedText(
                    directionText,
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Arial"),
                    16,
                    System.Windows.Media.Brushes.Red);
                double text_x = (rect.Width - ft.Width) / 2;
                double text_y = (rect.Height - ft.Height) / 2;
                drawingContext.DrawRectangle(rect.Fill,null,new Rect(0,0,rect.Width,rect.Height));
                drawingContext.DrawText(ft,new System.Windows.Point(text_x,text_y));
            }
            DrawingBrush drawingBrush = new DrawingBrush(drawingVisual.Drawing);
            rect.Fill = drawingBrush;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
