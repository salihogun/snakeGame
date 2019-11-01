using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Znake.Desktop.Models
{
    public class Snake : ISnake
    {
        public Snake()
        {
            CurrentDirection = Direction.Up;
        }

        public ObservableCollection<SnakePart> Body { get; } = new ObservableCollection<SnakePart>();

        public Direction CurrentDirection { get; set; }

        public void ChangeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.None:
                    break;
                case Direction.Left when CurrentDirection != Direction.Right:
                    {
                        CurrentDirection = direction;
                        break;
                    }
                case Direction.Up when CurrentDirection != Direction.Down:
                    {
                        CurrentDirection = direction;
                        break;
                    }
                case Direction.Right when CurrentDirection != Direction.Left:
                    {
                        CurrentDirection = direction;
                        break;
                    }
                case Direction.Down when CurrentDirection != Direction.Up:
                    {
                        CurrentDirection = direction;
                        break;
                    }
            }
        }

        public void Move(Size map)
        {
            if (CurrentDirection == Direction.None)
            {
                return;
            }

            SnakePart last = Body.Last();
            SnakePart first = Body.First();

            switch (CurrentDirection)
            {
                case Direction.Left:
                    last.Coordinates = new Point(((first.Coordinates.X - 1) + map.Width) % map.Width, (first.Coordinates.Y));
                    break;
                case Direction.Up:
                    last.Coordinates = new Point((first.Coordinates.X), ((first.Coordinates.Y - 1) + map.Height) % map.Height);
                    break;
                case Direction.Right:
                    last.Coordinates = new Point((first.Coordinates.X + 1) % map.Width, (first.Coordinates.Y));
                    break;
                case Direction.Down:
                    last.Coordinates = new Point((first.Coordinates.X), (first.Coordinates.Y + 1) % map.Height);
                    break;
            }

            Body.Remove(last);
            Body.Insert(0, last);
        }

        public void Grow()
        {
            //Add a new snake part
            SnakePart first = Body.Last();
            Body.Add(new SnakePart(Body.Last().Coordinates));





        }

    }

    public enum Direction
    {
        None = 0
      , Left = 4
      , Up = 8
      , Right = 6
      , Down = 2
    }

    public interface ISnake
    {
        void ChangeDirection(Direction direction);

        void Grow();

        void Move(Size map);
    }
}
