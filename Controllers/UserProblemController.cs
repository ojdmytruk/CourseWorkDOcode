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
                int score = 0;
                SolutionMatrix solution = new SolutionMatrix();
                solution.SolutionArray = new int[problem.Flows.Count()];
                var greedySolver = new GreedySolver(problem);
                solution.SolutionArray = greedySolver.GetSolution(score);
                solution.Score = score;
                return RedirectToAction("GreedySolutionUser", solution);
            }
            else if (myMethod == "Steepest")
            {
                int score = 0;
                SolutionMatrix solution = new SolutionMatrix();
                solution.SolutionArray = new int[problem.Flows.Count()];
                var greedySolver = new SteepestSolver(problem);
                solution.SolutionArray = greedySolver.GetSolution(score);
                solution.Score = score;
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