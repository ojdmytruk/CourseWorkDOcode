using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CourseWorkDO.Models;
using CourseWorkDO.Algorithm;

namespace CourseWorkDO.Controllers
{
    public class FileProblemController : Controller
    {
        [HttpGet]
        public ActionResult DataFile()
        {
            DataMatrix dataMatrix = new DataMatrix();
            return View(dataMatrix);
        }

        //[HttpPost]
        //public ActionResult DataFile(DataMatrix dataMatrix)
        //{
        //    DataMatrix matrix = new DataMatrix()
        //    { Dimension = dataMatrix.Dimension };

        //    return RedirectToAction("MatrixFile", matrix);
        //}

        [HttpGet]
        public ActionResult MatrixFile(DataMatrix problem)
        {
            //var problem = new DataMatrix();
            var dataReader = new DataReader();
            var filePath = Path.GetTempFileName().Trim();
            problem = dataReader.ReadData(filePath);
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

            return View("MatrixFile", problemResult);

        }

        [HttpPost]
        public ActionResult MatrixFile(DataMatrix problem2, [FromQuery] string myMethod = null)
        {
            var problem = problem2;

            if (myMethod == "Greedy")
            {
                SolutionMatrix solution = new SolutionMatrix();
                solution.SolutionArray = new int[problem.Flows.Count()];
                var greedySolver = new GreedySolver(problem);
                solution = greedySolver.GetSolution();
                return RedirectToAction("GreedySolutionFile", solution);
            }
            else if (myMethod == "Steepest")
            {
                SolutionMatrix solution = new SolutionMatrix();
                solution.SolutionArray = new int[problem.Flows.Count()];
                var steepestSolver = new SteepestSolver(problem);
                solution.SolutionArray = steepestSolver.GetSolution().SolutionArray;
                solution.Score = steepestSolver.GetSolution().Score;
                return RedirectToAction("SteepestSolutionFile", solution);
            }
            else
                return View(problem);
        }

        [HttpPost/*("FileUpload")*/]
        public async Task<IActionResult> DataFile(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            var data = new DataMatrix();
            var dataReader = new DataReader();
            data = dataReader.ReadData(filePaths.ToString().Trim());
            var problem = new DataMatrix()
            {
                Dimension = data.Dimension,
                Distances = data.Distances,
                Flows = data.Flows
            };

            return RedirectToAction("MatrixFile", problem);
        }

        [HttpGet]
        public ActionResult GreedySolutionFile(SolutionMatrix solution)
        {
            return View(solution);

        }

        [HttpGet]
        public ActionResult SteepestSolutionFile(SolutionMatrix solution)
        {
            return View(solution);
        }
    }
}