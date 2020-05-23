using System;
using System.Linq;
using CourseWorkDO.Models;

namespace CourseWorkDO.Algorithm
{
    public class Delta
    {
        public int[,] DeltaTable { get; set; }

        public DataMatrix Data { get; set; }

        public SolutionMatrix ActualBestSolution { get; set; }

        public int SwapCounter { get; set; }

        public Delta(DataMatrix data, SolutionMatrix solution)
        {
            ActualBestSolution = solution;
            Data = data;
            SwapCounter = 0;
            ActualBestSolution.Score = Convert.ToInt32(RateSolution(solution.Solution.ToArray(), data));
            CalculateDeltaTable();
        }

        public void ChangeSolution(int p, int q)
        {
            SwapCounter++;

            int piP = ActualBestSolution.Solution[p];
            int piQ = ActualBestSolution.Solution[q];



            int tableSize = Data.Distances.Count();

            ActualBestSolution.Score += DeltaTable[p, q];

            SwapValuesInSolution(p, q);
            for (int i = 0; i < tableSize; i++)
            {
                int piI = ActualBestSolution.Solution[i];
                for (int j = 0; j < tableSize; j++)
                {
                    int piJ = ActualBestSolution.Solution[j];
                    CalculateDelta(tableSize, i, j, piJ, piI);
                }
            }
        }


        public int RateSolutionChange(int swapX, int swapY)
        {
            return ActualBestSolution.Score + DeltaTable[swapX, swapY];
        }

        private void CalculateDelta(int dimension, int i, int j, int piJ, int piI)
        {
            int partSum = 0;
            for (int g = 0; g < dimension; g++)
            {
                if (g == i || g == j) continue;
                int piG = ActualBestSolution.Solution[g];
                partSum +=
                    (Data.Distances[g][i] - Data.Distances[g][j]) * (Data.Flows[piG][piJ] - Data.Flows[piG][piI]) +
                    (Data.Distances[i][g] - Data.Distances[j][g]) * (Data.Flows[piJ][piG] - Data.Flows[piI][piG]);
            }
            DeltaTable[i, j] =
                (Data.Distances[i][i] - Data.Distances[j][j]) * (Data.Flows[piJ][piJ] - Data.Flows[piI][piI]) +
                (Data.Distances[i][j] - Data.Distances[j][i]) * (Data.Flows[piJ][piI] - Data.Flows[piI][piJ]) +
                partSum;
        }

        private void CalculateDeltaTable()
        {
            int dimension = Data.Distances.Count();

            DeltaTable = new int[dimension, dimension];
            for (int i = 0; i < dimension; i++)
            {
                int piI = ActualBestSolution.Solution[i];
                for (int j = 0; j < dimension; j++)
                {
                    int piJ = ActualBestSolution.Solution[j];
                    CalculateDelta(dimension, i, j, piJ, piI);
                }
            }
        }

        private void SwapValuesInSolution(int swapX, int swapY)
        {
            int temp = ActualBestSolution.Solution[swapY];
            ActualBestSolution.Solution[swapY] = ActualBestSolution.Solution[swapX];
            ActualBestSolution.Solution[swapX] = temp;
        }

        public ulong RateSolution(int[] sol, DataMatrix data)
        {
            ulong cf = 0;
            for (int i = 0; i < sol.Count(); ++i)
            {
                for (int j = 0; j < sol.Count(); ++j)
                {

                    int xi = sol[i];
                    int xj = sol[j];
                    cf += Convert.ToUInt64(data.Distances[i][j]) * Convert.ToUInt64(data.Flows[xi][xj]);

                }
            }
            return cf;
        }
    }
}