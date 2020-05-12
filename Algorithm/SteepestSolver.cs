using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWorkDO.Models;

namespace CourseWorkDO.Algorithm
{
    public class SteepestSolver : QapSolver
    {
        public SteepestSolver(DataMatrix data) : base(data)
        {
        }

        public override int[] GetSolution()
        {
            SolutionMatrix solution = new SolutionMatrix
            {
                Dimension = Data.Distances.Length,
                Solution = this.GetList(this.GetRandomInitSolution())
            };
            FirstSolution = solution.Solution.ToArray();
            Delta benchmark = new Delta(Data, solution);
            CheckedElems = 0;
            bool isLocalMinimum = false;
            while (!isLocalMinimum)
            {
                if (!CheckBestNeighbor(benchmark))
                {
                    isLocalMinimum = true;
                }
            }
            CheckedElems = Steps * Data.Dimension * (Data.Dimension - 1);
            
            return benchmark.ActualBestSolution.Solution.ToArray();
        }

        public override int GetSwapCounter()
        {
            throw new NotImplementedException();
        }

        private bool CheckBestNeighbor(Delta benchmark)
        {
            int bestI = -1;
            int bestJ = -1;
            int bestScore = benchmark.ActualBestSolution.Score;

            for (int i = 0; i < benchmark.ActualBestSolution.Dimension - 1; i++)
            {
                for (int j = i + 1; j < benchmark.ActualBestSolution.Dimension; j++)
                {
                    int neighborScore = benchmark.RateSolutionChange(i, j);
                    if (neighborScore < bestScore)
                    {
                        bestScore = neighborScore;
                        bestI = i;
                        bestJ = j;
                    }
                }
            }
            Steps++;
            if (bestJ == -1 || bestI == -1)
            {
                return false;
            }
            else
            {
                benchmark.ChangeSolution(bestI, bestJ);
                return true;
            }
        }
    }
}
