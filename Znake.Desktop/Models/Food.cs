using Prism.Mvvm;
using System.Windows;

namespace Znake.Desktop.Models
{
    public class Food : BindableBase
    {
        private Point location;

        public Point Location
        {
            get => location;
            set => SetProperty(ref location, value);
        }
    }
}
