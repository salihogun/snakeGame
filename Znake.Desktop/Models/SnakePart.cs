using Prism.Mvvm;
using System.Windows;

namespace Znake.Desktop.Models
{
    public class SnakePart : BindableBase
    {
        private Point coordinates;
        private string image;

        public SnakePart(Point coordinates)
        {
            Coordinates = coordinates;
        }

        public Point Coordinates
        {
            get => coordinates;
            set => SetProperty(ref coordinates, value);
        }

        public string Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }
    }
}
