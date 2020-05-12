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
        
        public ActionResult DataUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMatrixes(DataMatrix problem)
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
            return View(problem);
           
        }

        [HttpPost]
        public ActionResult GreedySolutionUser(DataMatrix problem)
        {
            SolutionMatrix solution = new SolutionMatrix();
            solution.SolutionArray = new int[problem.Dimension];
            var greedySolver = new GreedySolver(problem);
            solution.SolutionArray = greedySolver.GetSolution();
            return View(solution);

        }

    }
}