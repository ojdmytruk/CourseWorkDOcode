using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using CourseWorkDO.Models;
using CourseWorkDO.Algorithm;

namespace CourseWorkDO.Controllers
{
    public class FileProblemController : Controller
    {
        private IWebHostEnvironment Environment;
        public FileProblemController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        [HttpGet]
        public ActionResult DataFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DataFile(IFormFile postedFile, [FromQuery] string myMethod = null)
        {           
            _ = this.Environment.WebRootPath;
            _ = this.Environment.ContentRootPath;

            string path = Path.Combine(this.Environment.WebRootPath, "UploadedFiles");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Path.GetFileName(postedFile.FileName);
                        
            
            var problem = new DataMatrix();
            using (var sr = new StreamReader(System.IO.File.OpenRead(Path.Combine(path, fileName))))
            {
                string data = sr.ReadToEnd();
                var splitted = data.Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
                    .Select(x => Convert.ToInt32(x))
                    .ToList();

                int matrixSize = splitted[0];
                problem.Dimension = matrixSize;
                if (problem.Dimension != 0)
                {
                    problem.Distances = new int[problem.Dimension][];
                    for (int i = 0; i < problem.Dimension; i++)
                        problem.Distances[i] = new int[problem.Dimension];
                    problem.Flows = new int[problem.Dimension][];
                    for (int i = 0; i < problem.Dimension; i++)
                        problem.Flows[i] = new int[problem.Dimension];
                }
                var qapDataFlow = new int[matrixSize][];
                var qapDataDistance = new int[matrixSize][];

                var chunked = splitted.Skip(1).Chunk(matrixSize).ToList();

                for (int i = 0; i < matrixSize; ++i)
                {
                    problem.Distances[i] = chunked[i].ToArray();
                }

                for (int i = matrixSize; i < 2 * matrixSize; ++i)
                {
                    problem.Flows[i - matrixSize] = chunked[i].ToArray();
                }

            }

            var problemResult = new DataMatrix()
            {
                Dimension = problem.Dimension,
                Distances = problem.Distances,
                Flows = problem.Flows
            };

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
            else return View();

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