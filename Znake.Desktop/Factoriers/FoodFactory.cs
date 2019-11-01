using System.Linq;
using System.Windows;
using Znake.Desktop.Models;

namespace Znake.Desktop.Factoriers
{
    public class FoodFactory
    {
        public Food Create(Size mapSize, Snake snake, IRandom random)
        {
            var food = new Food();
            Point foodLocation;
            do
            {
                foodLocation = new Point(
                    random.Next((int)mapSize.Width)
                  , random.Next((int)mapSize.Height));
            }
            while (InSnakeBody(foodLocation, snake));
            food.Location = foodLocation;
            return food;
        }

        public static bool InSnakeBody(Point foodCoordinates, Snake snake)
        {
            return snake.Body.Any(part => part.Coordinates == foodCoordinates);
        }
    }
}
