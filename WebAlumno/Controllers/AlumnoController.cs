using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAlumno.Controllers
{
    public class AlumnoController : Controller
    {
        //
        // GET: /Alumno/
        public ActionResult Index()
        {       

            return View();
        }
        [HttpPost]
        public ActionResult Index(int id)
        {
            using (WSAlumno.WSPersonaClient cliente = new WSAlumno.WSPersonaClient())
            {
                var alumno=cliente.BuscarAlumno(id);
                ViewBag.Dni= alumno.Dni;
                ViewBag.Nombre = alumno.Nombre;
                ViewBag.Error = alumno.Error;
            }

            return View();
        }
	}
}