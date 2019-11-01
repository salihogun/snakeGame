using Prism.Mvvm;
using System.Windows;

namespace Znake.Desktop.Models
{
    public class Assets : BindableBase
    {
        private Food food;
        private Snake snake;

        public Snake Snake
        {
            get => snake;
            set => SetProperty(ref snake, value);
        }

        public Food Food
        {
            get => food;
            set => SetProperty(ref food, value);
        }

        public Size MapSize { get; set; }


    }
}
