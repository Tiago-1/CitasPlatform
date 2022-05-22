using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using CitasPlatform.Data;
using CitasPlatform.Models;
using System.Threading.Tasks;

namespace CitasPlatform.Controllers
{
    public class CitasAlumno : Controller
    {
        private readonly CitasPlatformContext _context;

        public CitasAlumno(CitasPlatformContext context)
        {
            _context = context;
        }

        // GET: CitasAlumno
        public ActionResult Index()
        {
            return View();
        }

        public Boolean createCita()
        {
            return true;
        }

        // post -> CitasAlumno/createCita
        [HttpPost]
        public async Task<int> createCita(Cita model)
        {
            Console.WriteLine(model.citaDateTime);


            var dateAndTime = model.citaDateTime;
            var date = dateAndTime.ToString("yyyy-MM-dd");
            string response = dateAndTime.ToString("HH,mm");

            Console.WriteLine(response);

            decimal horainicio = Convert.ToDecimal(response);

            Console.WriteLine("--Test");
            Console.WriteLine(horainicio);
            Console.WriteLine(horainicio + 1);

            Cita cita = new Cita();

            cita.Fecha = Convert.ToDateTime(date);
            cita.Hora_Inicio = horainicio;
            cita.Hora_Final = horainicio + 1;
            cita.Tipo = model.Tipo;
            cita.Estatus = "Pendiente";
            cita.Descripcion = model.Descripcion;



            _context.Add(cita);
            await _context.SaveChangesAsync();

            return 1;
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
