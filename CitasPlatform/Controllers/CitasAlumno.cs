using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using CitasPlatform.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;

namespace CitasPlatform.Controllers
{
    public class CitasAlumno : Controller
    {
        private readonly Data.CitasPlatformContext _context;

        public CitasAlumno(Data.CitasPlatformContext context)
        {
            _context = context;
        }

        public int generalUsuarioId;

        // GET: CitasAlumno

        public async Task<IActionResult> Index(int? id)
        {

            if (id == null)
            {
               return ReturnToLogin();
            }

            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date;

            try
            {
                List<Cita> citas = await _context.Cita
                               .Where(b => b.UsuarioId == id && b.Fecha >= date)
                               .OrderBy(o => o.Fecha)
                               .Select(b => new Cita
                               {
                                   Fecha = b.Fecha,
                                   CitaId = b.CitaId,
                                   Estatus = b.Estatus,
                                   Tipo = b.Tipo,
                                   Descripcion = b.Descripcion,
                                   Hora_Inicio = b.Hora_Inicio,
                                   Hora_Final = b.Hora_Final,
                                   H_Final = b.Hora_Final.ToString().Replace('.', ':'),
                                   H_Inicio = b.Hora_Inicio.ToString().Replace('.', ':')
                               }).ToListAsync();
               
                Usuario user = new Usuario();
                user.UsuarioId = (int)id;

                ViewBag.Message = user;

                return View(citas);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return View();
        }
        public ActionResult Cita(int? id)
        {

            if(id == null)
            {
               return ReturnToLogin();
            }

            Usuario user = new Usuario();
            user.UsuarioId = (int)id;

            ViewBag.Message = user;
            return View();
        }
        public ActionResult Contact(int ?id)
        {
            if (id == null)
            {
                return ReturnToLogin();
            }

            Usuario user = new Usuario();
            user.UsuarioId = (int)id;

            ViewBag.Message = user;
            return View();
        }
        public ActionResult createCita(int? id)
        {
            return  id != null ? View() : ReturnToLogin();
        }

        // post -> CitasAlumno/createCita
        [HttpPost]
        public async Task<ActionResult> createCita(Cita model)
        {
            var dateAndTime = model.citaDateTime;
            var date = dateAndTime.ToString("yyyy-MM-dd");
            string response = dateAndTime.ToString("HH,mm");
            decimal horainicio = Convert.ToDecimal(response);

            Cita cita = new Cita();

            cita.Fecha = Convert.ToDateTime(date);
            cita.Hora_Inicio = horainicio;
            cita.Hora_Final = horainicio + 1;
            cita.Tipo = model.Tipo;
            cita.Estatus = "Pendiente";
            cita.Descripcion = model.Descripcion;
            cita.UsuarioId = model.UsuarioId;

            _context.Add(cita);
            await _context.SaveChangesAsync();


            Usuario user = new Usuario();
            user.UsuarioId = (int) model.UsuarioId;

            ViewBag.Message = user;
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


        public ActionResult ReturnToLogin()
        {
            return RedirectToAction("", new RouteValueDictionary(
                    new { controller = "Home", action = "get" }));
        }

    }
}
