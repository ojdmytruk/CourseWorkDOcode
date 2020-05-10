﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWorkDO.Models;

namespace CourseWorkDO.Algorithm
{
    public class QapSimpleGreedySolver : QapSolver
    {
        public QapSimpleGreedySolver(DataMatrix data) : base(data)
        {
        }

        public override int[] GetSolution()
        {
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
                    if (distancesPotential_[i] < distancesPotential_[maxPos])
                    {
                        maxPos = i;
                    }
                    if (flowPotential_[i] > flowPotential_[minPos])
                    {
                        minPos = i;
                    }
                }
                solution_[maxPos] = minPos;
                distancesPotential_[maxPos] = -1;
                flowPotential_[minPos] = Int32.MaxValue;
            }
            return solution_;
        }

        public override int GetSwapCounter()
        {
            throw new NotImplementedException();
        }
    }
}
