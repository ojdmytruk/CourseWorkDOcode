using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseWorkDO.Controllers
{
    public class GeneratedProblemController : Controller
    {
        // GET: Generated
        public ActionResult DataGenerated()
        {
            return View();
        }

        // GET: Generated/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Generated/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Generated/Create
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

        // GET: Generated/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Generated/Edit/5
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

        // GET: Generated/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Generated/Delete/5
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