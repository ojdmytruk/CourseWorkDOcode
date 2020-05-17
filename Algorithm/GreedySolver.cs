using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWorkDO.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseWorkDO.Algorithm
{
    public class GreedySolver : QapSolver
    {
        public GreedySolver(DataMatrix data) : base(data)
        {
        }

        public override SolutionMatrix GetSolution()
        {
            var solutionMatrix = new SolutionMatrix();
            int dimension_ = this.Data.Dimension;
            var distancesPotential_ = new List<int>();
            var flowPotential_ = new List<int>();

            for (int i = 0; i < dimension_; ++i)
            {
                int potential = 0;
                for (int j = 0; j < dimension_; ++j)
                {
                    if (i != j)
                    {
                        potential += this.Data.Distances[i][j];
                    }
                }
                distancesPotential_.Add(potential);
            }

            for (int i = 0; i < dimension_; ++i)
            {
                int potential = 0;
                for (int j = 0; j < dimension_; ++j)
                {
                    if (i != j)
                    {
                        potential += this.Data.Flows[i][j];
                    }
                }
                flowPotential_.Add(potential);
            }

            int[] solution_ = Enumerable.Repeat(-1, Data.Dimension).ToArray();
            for (int j = 0; j < dimension_; ++j)
            {
                int maxPos = 0;
                int minPos = 0;
                for (int i = 0; i < dimension_; ++i)
                {
                    if (distancesPotential_[i] > distancesPotential_[maxPos])
                    {
                        maxPos = i;
                    }
                    if (flowPotential_[i] < flowPotential_[minPos])
                    {
                        minPos = i;
                    }
                }
                solution_[maxPos] = minPos;
                distancesPotential_[maxPos] = -1;
                flowPotential_[minPos] = Int32.MaxValue;
            }
            solutionMatrix.SolutionArray = solution_;
            int score = 0;
            for (int i = 1; i < solution_.Length; i++)
            {
                score += Data.Distances[solution_[i - 1]][solution_[i]] * Data.Flows[i - 1][i];
            }
            solutionMatrix.Score = score;
            return solutionMatrix;
        }

        public override int GetSwapCounter()
        {
            throw new NotImplementedException();
        }
    }
}
