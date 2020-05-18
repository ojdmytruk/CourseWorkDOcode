using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CourseWorkDO.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkDO.Controllers
{
    public class AnaliticsController : Controller
    {
        private AnaliticsContext db;

        public AnaliticsController(AnaliticsContext analiticsContext)
        {
            db = analiticsContext;
        }

        public async Task<IActionResult> AnaliticsData()
        {
            return View(await db.AnaliticsTable.ToListAsync());
        }
    }
}