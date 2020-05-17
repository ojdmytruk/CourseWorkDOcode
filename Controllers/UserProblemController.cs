using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWorkDO.Models;
using CourseWorkDO.Algorithm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace CourseWorkDO.Controllers
{
    public class UserProblemController : Controller
    {
        
        [HttpGet]
        public ActionResult DataUser()
        {
            DataMatrix dataMatrix = new DataMatrix();
            return View(dataMatrix);
        }

        [HttpPost]
        public ActionResult DataUser(DataMatrix dataMatrix)
        {
            DataMatrix matrix = new DataMatrix()
            { Dimension = dataMatrix.Dimension };

            return RedirectToAction("MatrixUser", matrix);
        }

        [HttpGet]
        public ActionResult MatrixUser(DataMatrix problem)
        {
            if (problem.Dimension != 0)
            {
                problem.Distances = new int[problem.Dimension][];
                for (int i = 0; i < problem.Dimension; i++)
                    problem.Distances[i] = new int[problem.Dimension];
                problem.Flows = new int[problem.Dimension][];
                for (int i = 0; i < problem.Dimension; i++)
                    problem.Flows[i] = new int[problem.Dimension];
            }

            var problemResult = new DataMatrix()
            {
                Dimension = problem.Dimension,
                Distances = problem.Distances,
                Flows = problem.Flows
            };

            return View("MatrixUser", problemResult);

        }

        [HttpPost]
        public ActionResult MatrixUser(DataMatrix problem2, [FromQuery] string myMethod = null)
        {
            var problem = problem2;

            if (myMethod == "Greedy")
            {
                SolutionMatrix solution = new SolutionMatrix();
                solution.SolutionArray = new int[problem.Flows.Count()];
                var greedySolver = new GreedySolver(problem);
                solution = greedySolver.GetSolution();
                return RedirectToAction("GreedySolutionUser", solution);
            }
            else if (myMethod == "Steepest")
            {
                SolutionMatrix solution = new SolutionMatrix();
                solution.SolutionArray = new int[problem.Flows.Count()];
                var steepestSolver = new SteepestSolver(problem);
                solution.SolutionArray = steepestSolver.GetSolution().SolutionArray;
                solution.Score = steepestSolver.GetSolution().Score;
                return RedirectToAction("SteepestSolutionUser", solution);
            }
            else
                return View(problem);
        }

        [HttpGet]
        public ActionResult GreedySolutionUser(SolutionMatrix solution)
        {
            return View(solution);
        }

        [HttpGet]
        public ActionResult SteepestSolutionUser(SolutionMatrix solution)
        {
            return View(solution);
        }

    }
}