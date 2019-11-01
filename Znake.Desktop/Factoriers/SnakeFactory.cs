using System.Windows;
using Znake.Desktop.Models;

namespace Znake.Desktop.Factoriers
{
    public class SnakeFactory
    {
        public Snake Create(Point headPoint)
        {
            var snake = new Snake();
            snake.Body.Add(new SnakePart(headPoint));
            snake.Body.Add(new SnakePart(new Point(headPoint.X - 1, headPoint.Y)));
            snake.Body.Add(new SnakePart(new Point(headPoint.X - 2, headPoint.Y)));


            return snake;
        }
    }
}
