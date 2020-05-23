using System;
using CourseWorkDO.Models;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkDO.Algorithm
{
    public class SteepestSolver: QapSolver
    {        
        public SteepestSolver(DataMatrix data) : base(data)
        {
        }

        public override SolutionMatrix GetSolution()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AnaliticsContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

            AnaliticsContext db = new AnaliticsContext(options);

            Stopwatch stopwatch = new Stopwatch();
            SolutionMatrix solution = new SolutionMatrix
            {
                Dimension = Data.Distances.Length,
                Solution = this.GetList(this.GetRandomInitSolution())
            };

            stopwatch.Start();

            FirstSolution = solution.Solution.ToArray();
            Delta betterSolution = new Delta(Data, solution);
            CheckedElems = 0;
            bool isLocalMinimum = false;
            while (!isLocalMinimum)
            {
                if (!CheckBestNeighbor(betterSolution))
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
            analitics.Dimenssion = Data.Dimension;
            long ticks = stopwatch.ElapsedTicks;
            TimeSpan interval = TimeSpan.FromTicks(ticks);
            analitics.Method = "Метод вектора спаду";
            analitics.WorkTime = interval.ToString();
            analitics.Score = score;
            db.AnaliticsTable.Add(analitics);
            db.SaveChangesAsync();

            solution.Score = score;
            solution.SolutionArray = betterSolution.ActualBestSolution.Solution.ToArray();
            return solution;
        }

        public override int GetSwapCounter()
        {
            throw new NotImplementedException();
        }

        private bool CheckBestNeighbor(Delta betterSolution)
        {
            int bestI = -1;
            int bestJ = -1;
            int bestScore = betterSolution.ActualBestSolution.Score;

            for (int i = 0; i < betterSolution.ActualBestSolution.Dimension - 1; i++)
            {
                for (int j = i + 1; j < betterSolution.ActualBestSolution.Dimension; j++)
                {
                    int neighborScore = betterSolution.RateSolutionChange(i, j);
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
                betterSolution.ChangeSolution(bestI, bestJ);
                return true;
            }
            
        }
    }
}
