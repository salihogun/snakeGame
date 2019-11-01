using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Znake.Desktop.Converters;
using Znake.Desktop.Factoriers;
using Znake.Desktop.Models;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace Znake.Desktop.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly LocationToCanvasConverter converter;
        private readonly IDialogCoordinator dialogCoordinator;
        private readonly DispatcherTimer timer;
        private DelegateCommand delegateCommand;
        private Random random = new Random();
        private int points;
        private ObservableCollection<string> scoreList;
        private ObservableCollection<string> name;

        public ShellViewModel(IDialogCoordinator dialogCoordinator)
        {
            // Application.Current.Dispatcher.Invoke(
            //     async () =>
            //     {
            //         await (Application.Current.MainWindow as MetroWindow).ShowMetroDialogAsync(startMenuDialog);
            //     });
            converter = new LocationToCanvasConverter();
            ScoreList = new ObservableCollection<string>();
            MapSize = new Size(60, 60);

            this.dialogCoordinator = dialogCoordinator;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000.0F / Fps);
            timer.Tick += delegate
            {
                CleanCanvas();
                Update();
                DrawCanvas();
            };

        }

        public DelegateCommand StartCommand => delegateCommand ?? (delegateCommand = new DelegateCommand(OnStart));

        public Size MapSize { get; set; }

        public Snake Snake { get; set; }

        public Food Food { get; set; }

        public Canvas Canvas { get; set; }



        public ushort Fps
        {
            get;
            set;
        } = 30;

        public int Points
        {
            get { return points; }
            set { SetProperty(ref points, value); }
        }


        public ObservableCollection<string> ScoreList
        {
            get { return scoreList; }
            set { SetProperty(ref scoreList, value); }
        }



        private void DrawCanvas()
        {
            DrawRectangle(converter.Convert(Food.Location, Canvas, MapSize), Brushes.Red);
            DrawSnake();
        }

        private void CleanCanvas()
        {
            Canvas.Children.Clear();
        }

        public void OnStart()
        {
            Points = 0;
            Snake = new SnakeFactory().Create(new Point(MapSize.Height / 2 + 1, MapSize.Width / 2 + 1));
            DrawSnake();

            #region Create and Draw Food

            Food = new Food
            {
                Location = new Point(random.Next((int)MapSize.Height), random.Next((int)MapSize.Height))
            };

            Rectangle rec = converter.Convert(
                Food.Location
              , Canvas
              , MapSize);
            DrawRectangle(rec, Brushes.Red);

            #endregion

            timer.Start();
        }

        private void DrawRectangle(Rectangle rec, Brush color)
        {
            var element = new Label
            {
                Background = color
              ,
                Height = rec.Height
              ,
                Width = rec.Width
            };
            Canvas.Children.Add(element);
            Canvas.SetLeft(element, rec.Left);
            Canvas.SetTop(element, rec.Top);
        }

        private void DrawSnake()
        {
            foreach (SnakePart part in Snake.Body)
            {
                DrawSnakePart(part);
            }
        }

        public void addlist()
        {
            string Info = string.Format("Name: {0} | IP: {1} , | Score: {2}", GetName(), GetIp(), Points.ToString());
            ScoreList.Add(Info);
        }

        private string GetName()
        {
            return System.Environment.MachineName;
        }

        public static IPAddress GetIp()
        {
            return Dns.GetHostAddresses(Dns.GetHostName())[1];

        }

        private void DrawSnakePart(SnakePart part)
        {
            Rectangle rectangle = converter.Convert(part.Coordinates, Canvas, MapSize);
            DrawRectangle(rectangle, Brushes.BlueViolet);
        }


        private void OnWake()
        {
            //snake initial position

            //food initial position

            if (IsEatFood())
            {
                Food = new Food
                {
                    Location = new Point(random.Next((int)MapSize.Height), random.Next((int)MapSize.Height))
                };

                Rectangle rec = converter.Convert(
                    Food.Location
                    , Canvas
                    , MapSize);
                DrawRectangle(rec, Brushes.Red);
            }
        }

        private bool IsEatFood()
        {

            if (Snake.Body.Any(part => part.Coordinates == Food.Location))
            {
                Points++;
                Snake.Grow();
                return true;
            }

            return false;
        }

        private Point DontPassWall(SnakePart part)
        {//if Snake crushs to wall use this 
            Point partCoordinates = part.Coordinates;
            if (Snake.Body.First().Coordinates.Y < 0 || Snake.Body.First().Coordinates.Y > MapSize.Height || Snake.Body.First().Coordinates.X < 0 || Snake.Body.First().Coordinates.X > MapSize.Height)
            {
                CloseGame();
            }
            return partCoordinates;
        }

        private void CloseGame()
        {
            Application.Current.Shutdown();
        }

        public void DidSnakeDie()
        {
            for (int i = 1; i < Snake.Body.Count; i++)
            {
                if (Snake.Body[0].Coordinates.X == Snake.Body[i].Coordinates.X && Snake.Body[0].Coordinates.Y == Snake.Body[i].Coordinates.Y)
                {
                    MessageBoxResult result1 = MessageBox.Show("Would You like New Game ?", " You Die :(", MessageBoxButton.YesNo);
                    addlist();
                    if (result1 == MessageBoxResult.Yes)
                    {
                        OnStart();
                    }
                    else
                    {
                        CloseGame();
                    }

                }
            }
        }

        private void Update()
        {
            Snake.Move(MapSize);
            OnWake();
            DidSnakeDie();




        }
    }
}
