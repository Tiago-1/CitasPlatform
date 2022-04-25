using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CitasPlatform.Controllers
{
    public class CitasAlumno : Controller
    {
        // GET: CitasAlumno
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Alumnos()
        {
            return View();
        }


        // POST: CitasAlumno/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CitasAlumno/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CitasAlumno/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
