using System;
using System.Windows.Input;
using Znake.Desktop.Models;
using Znake.Desktop.ViewModels;

namespace Znake.Desktop.Views
{
    public partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
            ContentRendered += OnContentRendered;
        }

        private void OnContentRendered(object sender, EventArgs e)
        {
            if (!(DataContext is ShellViewModel vm))
            {
                return;
            }

            vm.Canvas = canvas;
            vm.OnStart();
        }

        private void Canvas_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!(DataContext is ShellViewModel vm))
            {
                return;
            }


            switch (e.Key)
            {
                case Key.A:
                case Key.Left:
                case Key.NumPad4:
                    vm.Snake.ChangeDirection(Direction.Left);
                    break;

                case Key.S:
                case Key.Down:
                    vm.Snake.ChangeDirection(Direction.Down);
                    break;

                case Key.W:
                case Key.Up:
                    vm.Snake.ChangeDirection(Direction.Up);
                    break;

                case Key.D:
                case Key.Right:
                    vm.Snake.ChangeDirection(Direction.Right);
                    break;
            }
        }
    }
}
