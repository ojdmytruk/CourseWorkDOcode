using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWorkDO.Models;

namespace CourseWorkDO.Algorithm
{
    public class BetterSolutiou
    {
        public ulong RateSolution(int[] sol, DataMatrix data)
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
  
        public int RateInsert(int[] sol, DataMatrix data, int facility, int location)
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

    }
}
