using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWorkDO.Models;

namespace CourseWorkDO.Algorithm
{
    public class QapSteepestLocalSolver : QapSolver
    {
        public QapSteepestLocalSolver(DataMatrix data) : base(data)
        {
        }

        public override int[] GetSolution()
        {
            QapSolution solution = new QapSolution
            {
                Size = Data.Distances.Length,
                Solution = this.GetList(this.GetRandomInitSolution())
            };
            FirstSolution = solution.Solution.ToArray();
            DeltaSolutionBenchmark benchmark = new DeltaSolutionBenchmark(Data, solution);
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

        private bool CheckBestNeighbor(DeltaSolutionBenchmark benchmark)
        {
            int bestI = -1;
            int bestJ = -1;
            int bestScore = benchmark.ActualBestSolution.Score;

            for (int i = 0; i < benchmark.ActualBestSolution.Size - 1; i++)
            {
                for (int j = i + 1; j < benchmark.ActualBestSolution.Size; j++)
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
