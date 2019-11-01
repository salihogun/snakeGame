using System;

namespace Znake.Desktop.Models
{
    public interface IRandom
    {
        int Next(int max);
        int Next(int min, int max);
    }

    public class MyRandom : Random, IRandom
    {
        public MyRandom(int seed) : base(seed)
        {

        }

        public MyRandom()
        {

        }

    }

}
