using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CourseWorkDO.Models;

namespace CourseWorkDO.Controllers
{
    public class AnaliticsController : Controller
    {
       [HttpGet]
       public ActionResult AnaliticsData()
        {
           // AnaliticsContext db = new AnaliticsContext();
            return View(/*db*/);
        }
    }
}