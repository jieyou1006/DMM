using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMM.Controllers
{
    public class MOOController : Controller
    {
        // GET: MOO
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Actresses()
        {
            return View();
        }

        public ActionResult Genre()
        {
            return View();
        }
    }
}