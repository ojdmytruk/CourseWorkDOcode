using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWorkDO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWorkDO.Controllers
{
    public class UserProblemController : Controller
    {
        // GET: UserProblem
        public ActionResult DataUser()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateMatrixes()
        {
            DataMatrix userDataMatrix = new DataMatrix();
            
            return View(userDataMatrix);
        }

        [HttpPost]
        public ActionResult CreateMatrixes(DataMatrix userDataMatrix)
        {
            if (userDataMatrix.Dimension != 0)
            {
                userDataMatrix.Distances = new double[userDataMatrix.Dimension][];
                for (int i = 0; i < userDataMatrix.Dimension; i++)
                    userDataMatrix.Distances[i] = new double[userDataMatrix.Dimension];
                userDataMatrix.Flows = new double[userDataMatrix.Dimension][];
                for (int i = 0; i < userDataMatrix.Dimension; i++)
                    userDataMatrix.Flows[i] = new double[userDataMatrix.Dimension];
            }
            return View(userDataMatrix);
        }


        // GET: UserProblem/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserProblem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProblem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserProblem/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserProblem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserProblem/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserProblem/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}