using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWorkDO.Models;
using System.Diagnostics;

namespace CourseWorkDO.Algorithm
{
    public class SteepestSolver : QapSolver
    {
        AnaliticsContext db = new AnaliticsContext();
        public SteepestSolver(DataMatrix data) : base(data)
        {
        }

        public override SolutionMatrix/*[]*/ GetSolution(/*int score*/)
        {
            Stopwatch stopwatch = new Stopwatch();
            SolutionMatrix solution = new SolutionMatrix
            {
                Dimension = Data.Distances.Length,
                Solution = this.GetList(this.GetRandomInitSolution())
            };

            stopwatch.Start();

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
            int score = 0;
            for (int i = 1; i < solution.Solution.ToArray().Length; i++)
            {
                score += Data.Distances[solution.Solution.ToArray()[i - 1]][solution.Solution.ToArray()[i]] * Data.Flows[i - 1][i];
            }

            stopwatch.Stop();

            var analitics = new Analitics();
            analitics.Dimenssion = solution.Dimension;
            analitics.WorkTime = stopwatch.ElapsedMilliseconds;
            db.AnaliticsTable.Add(analitics);

            solution.Score = score;
            solution.SolutionArray = benchmark.ActualBestSolution.Solution.ToArray();
            return solution;
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
