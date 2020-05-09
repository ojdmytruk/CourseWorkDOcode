using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
namespace CourseWorkDO.Algorithm
{
    public class RandomGenerator
    {
        public Random Rnd { get; set; } = new Random();

        public void Shuffle<T>(IList<T> toShuffle)
        {
            for (int i = toShuffle.Count - 1; i >= 0; --i)
            {
                var j = Rnd.Next(0, i);
                toShuffle.Swap(i, j);
            }
        }

        public void Shuffle<T>(T[] toShuffle)
        {
            for (int i = toShuffle.Length - 1; i >= 0; --i)
            {
                var j = Rnd.Next(0, i);
                T temp = toShuffle[i];
                toShuffle[i] = toShuffle[j];
                toShuffle[j] = temp;
            }
        }

        public T RandomElement<T>(IList<T> elems)
        {
            return elems[Rnd.Next(0, elems.Count)];
        }

        public int RandomNumber(int begin, int end)
        {
            return Rnd.Next(begin, end);
        }
    }
}
