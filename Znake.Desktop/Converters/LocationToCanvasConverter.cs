using System.Drawing;
using System.Windows.Controls;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace Znake.Desktop.Converters
{
    public class LocationToCanvasConverter
    {
        public Rectangle Convert(Point foodLocation, Canvas canvas, Size map)
        {
            double heightRatio = canvas.ActualHeight / map.Height;
            double widthRatio = canvas.ActualWidth / map.Width;

            var rectangle = new Rectangle
            {
                X = (int)(widthRatio * foodLocation.X)
              ,
                Y = (int)(heightRatio * foodLocation.Y)
              ,
                Height = (int)heightRatio - 1
              ,
                Width = (int)widthRatio - 1
            };


            return rectangle;
        }
    }
}
