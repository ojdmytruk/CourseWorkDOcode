using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWorkDO.Algorithm
{
    public class QapSolutionBenchmark
    {
        public ulong RateSolution(int[] sol, QapData data)
        {
            
            ulong fitness = 0;
            for (int i = 0; i < sol.Count(); ++i)
            {
                for (int j = 0; j < sol.Count(); ++j)
                {

                    int xi = sol[i];
                    int xj = sol[j];
                    fitness += Convert.ToUInt64(data.Distances[i][j]) * Convert.ToUInt64(data.Flows[xi][xj]);

                }
            }
            return fitness;
        }

        public int RateSolutionInt(int[] sol, QapData data)
        {
            
            int fitness = 0;
            for (int i = 0; i < sol.Count(); ++i)
            {
                for (int j = 0; j < sol.Count(); ++j)
                {

                    int xi = sol[i];
                    int xj = sol[j];
                    fitness += (data.Distances[i][j]) * (data.Flows[xi][xj]);

                }
            }
            return fitness;
        }

        public int RateSolutionIndexedFromZero(int[] sol, QapData data)
        {
         
            int fitness = 0;
            for (int i = 0; i < sol.Count(); ++i)
            {
                for (int j = 0; j < sol.Count(); ++j)
                {
                    //if (i != j)
                    {
                        int xi = sol[i];
                        int xj = sol[j];
                        fitness += data.Distances[i][j] * data.Flows[xi - 1][xj - 1];
                    }

                }
            }
            return fitness;
        }



        public int RateInsert(int[] sol, QapData data, int facility, int location)
        {
            int cost = 0;
            for (int i = 0; i < sol.Length; ++i)
            {
                if (sol[i] != -1)
                {
                    cost += data.Distances[i][location] * data.Flows[sol[i]][facility];
                    cost += data.Distances[location][i] * data.Flows[facility][sol[i]];
                }
            }
            return cost;
        }

        public double RateSimilarity(int[] sol, int[] orig)
        {
            double x = 0;
            for (int i = 0; i < sol.Count(); ++i)
            {
                if (sol[i] == orig[i]) x++;
            }
            return x / Convert.ToDouble(sol.Length);
        }
    }
}
