using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWorkDO.Models;
using CourseWorkDO.Algorithm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWorkDO.Controllers
{
    public class GeneratedProblemController : Controller
    {
        [HttpGet]
        public ActionResult DataGenerated()
        {
            DataMatrix dataMatrix = new DataMatrix();
            return View(dataMatrix);
        }

        [HttpPost]
        public ActionResult DataGenerated(DataMatrix dataMatrix)
        {
            DataMatrix matrix = new DataMatrix()
            { Dimension = dataMatrix.Dimension };

            return RedirectToAction("MatrixGenerated", matrix);


        }

        [HttpGet]
        public ActionResult MatrixGenerated(DataMatrix problem)
        {
            if (problem.Dimension != 0)
            {
                problem.Distances = new int[problem.Dimension][];
                for (int i = 0; i < problem.Dimension; i++)
                    problem.Distances[i] = new int[problem.Dimension];
                problem.Flows = new int[problem.Dimension][];
                for (int i = 0; i < problem.Dimension; i++)
                    problem.Flows[i] = new int[problem.Dimension];
                for (int i = 0; i < problem.Dimension; i++)
                    for (int j = 0; j < problem.Dimension; j++)
                    {
                        Random random = new Random();
                        int value = random.Next(1, 99);
                        if (i==j)
                        {
                            problem.Distances[i][j] = 0;
                        }
                        else
                        {
                            problem.Distances[i][j] = value;
                            problem.Distances[j][i] = value;
                        }

                    }
                for (int i = 0; i < problem.Dimension; i++)
                    for (int j = 0; j < problem.Dimension; j++)
                    {
                        Random random = new Random();
                        int value = random.Next(1, 99);
                        if (i == j)
                        {
                            problem.Flows[i][j] = 0;
                        }
                        else
                        {
                            problem.Flows[i][j] = value;
                            problem.Flows[j][i] = value;
                        }

                    }
            }

            var problemResult = new DataMatrix()
            {
                Dimension = problem.Dimension,
                Distances = problem.Distances,
                Flows = problem.Flows
            };

            return View("MatrixGenerated", problemResult);

        }

        [HttpPost]
        public ActionResult MatrixGenerated(DataMatrix problem2, [FromQuery] string myMethod = null)
        {
            var problem = problem2;

            if (myMethod == "Greedy")
            {
                int score = 0;
                SolutionMatrix solution = new SolutionMatrix();
                solution.SolutionArray = new int[problem.Flows.Count()];
                var greedySolver = new GreedySolver(problem);
                solution.SolutionArray = greedySolver.GetSolution(score);
                solution.Score = score;
                return RedirectToAction("GreedySolutionGenerated", solution);
            }
            else if (myMethod == "Steepest")
            {
                int score = 0;
                SolutionMatrix solution = new SolutionMatrix();
                solution.SolutionArray = new int[problem.Flows.Count()];
                var greedySolver = new SteepestSolver(problem);
                solution.SolutionArray = greedySolver.GetSolution(score);
                solution.Score = score;
                return RedirectToAction("SteepestSolutionGenerated", solution);
            }
            else
                return View(problem);
        }


        [HttpGet]
        public ActionResult GreedySolutionGenerated(SolutionMatrix solution)
        {
            return View(solution);

        }

        [HttpGet]
        public ActionResult SteepestSolutionGenerated(SolutionMatrix solution)
        {
            return View(solution);
        }


    }
}