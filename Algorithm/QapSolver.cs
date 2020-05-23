using System;
using System.Collections.Generic;
using System.Linq;
using CourseWorkDO.Models;

namespace CourseWorkDO.Algorithm
{
    public abstract class QapSolver
    {
        public DataMatrix Data { get; set; }

        public RandomGenerator Rnd { get; set; } = new RandomGenerator();

        public int CheckedElems { get; set; }

        public int[] FirstSolution { get; set; }

        public int Steps { get; set; }

        public QapSolver(DataMatrix data)
        {
            this.Data = data;
        }

        public abstract SolutionMatrix GetSolution();

        public abstract int GetSwapCounter();

        public int[] GetRandomInitSolution()
        {
            var init = Enumerable.Range(0, Data.Dimension).ToArray();
            Rnd.Shuffle(init);
            return init;
        }

        public List<int> GetList(int[] array)
        {
            List<int> list = new List<int>();
            foreach (int elem in array)
            {
                list.Add(elem);
            }
            return list;
        }

    }
}
