using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CitasPlatform.Data;
using CitasPlatform.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Routing;

namespace CitasPlatform.Controllers
{
    public class Admin : Controller
    {

        private readonly CitasPlatformContext _context;

        public Admin(CitasPlatformContext context)
        {
            _context = context;
        }


        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date;

            List<Cita> citas = new List<Cita>();

            var citax = (from citaCtx in _context.Cita
                              join usuarioCtx in _context.Usuario on citaCtx.UsuarioId equals usuarioCtx.UsuarioId
                            where citaCtx.Fecha == date && citaCtx.Estatus == "Pendiente"
                            orderby citaCtx.Hora_Inicio
                            select new Cita
                            {
                                Fecha = citaCtx.Fecha,
                                UsuarioId = citaCtx.UsuarioId,
                                CitaId = citaCtx.CitaId,
                                Estatus = citaCtx.Estatus,
                                Tipo = citaCtx.Tipo,
                                Descripcion = citaCtx.Descripcion,
                                Hora_Inicio = citaCtx.Hora_Inicio,
                                Hora_Final = citaCtx.Hora_Final,
                                H_Final = citaCtx.Hora_Final.ToString().Replace('.', ':'),
                                H_Inicio = citaCtx.Hora_Inicio.ToString().Replace('.', ':'),
                                NombreUsuario = usuarioCtx.Nombre +" " +usuarioCtx.Apellidos
                            });

             await citax.ForEachAsync(cita =>
            {

                citas.Add(new Cita() {
                    Fecha = cita.Fecha,
                    UsuarioId = cita.UsuarioId,
                    CitaId = cita.CitaId,
                    Estatus = cita.Estatus,
                    Tipo = cita.Tipo,
                    Descripcion = cita.Descripcion,
                    Hora_Inicio = cita.Hora_Inicio,
                    Hora_Final = cita.Hora_Final,
                    H_Final = cita.H_Final,
                    H_Inicio = cita.H_Inicio,
                    NombreUsuario = cita.NombreUsuario
                });
            });

            return View(citas);
        }

        public async Task<string> GetUsersList(int citaId)
        {
            List<String> usuariosV = await _context.Cita_Usuario
                                    .Where(cu => cu.CitaId == citaId)
                                    .Select(cu => cu.UsuarioId.ToString())
                                    .ToListAsync();

            string usuariosVp = string.Join(',', usuariosV);
            return usuariosVp;
        }

        public async Task<IActionResult> Historial(string name,string date)
        {
            
            List<Cita> citas = new List<Cita>();

            IQueryable<Cita> citax;

            if (name == null)
            {
                name = "";
            }

            if(date != null)
            {
                 DateTime searchDate = DateTime.Parse(date);
                 citax = (from citaCtx in _context.Cita
                         join usuarioCtx in _context.Usuario on citaCtx.UsuarioId equals usuarioCtx.UsuarioId
                         where usuarioCtx.Nombre.Contains(name) || usuarioCtx.Apellidos.Contains(name)
                         where citaCtx.Fecha == searchDate
                         orderby citaCtx.Fecha
                         select new Cita
                             {
                                 Fecha = citaCtx.Fecha,
                                 UsuarioId = citaCtx.UsuarioId,
                                 CitaId = citaCtx.CitaId,
                                 Estatus = citaCtx.Estatus,
                                 Tipo = citaCtx.Tipo,
                                 Descripcion = citaCtx.Descripcion,
                                 Hora_Inicio = citaCtx.Hora_Inicio,
                                 Hora_Final = citaCtx.Hora_Final,
                                 H_Final = citaCtx.Hora_Final.ToString().Replace('.', ':'),
                                 H_Inicio = citaCtx.Hora_Inicio.ToString().Replace('.', ':'),
                                 NombreUsuario = usuarioCtx.Nombre + " " + usuarioCtx.Apellidos
                             });

            }
            else
            {
              citax = (from citaCtx in _context.Cita
                         join usuarioCtx in _context.Usuario on citaCtx.UsuarioId equals usuarioCtx.UsuarioId
                         where usuarioCtx.Nombre.Contains(name) || usuarioCtx.Apellidos.Contains(name)
                         orderby citaCtx.Fecha
                         select new Cita
                         {
                             Fecha = citaCtx.Fecha,
                             UsuarioId = citaCtx.UsuarioId,
                             CitaId = citaCtx.CitaId,
                             Estatus = citaCtx.Estatus,
                             Tipo = citaCtx.Tipo,
                             Descripcion = citaCtx.Descripcion,
                             Hora_Inicio = citaCtx.Hora_Inicio,
                             Hora_Final = citaCtx.Hora_Final,
                             H_Final = citaCtx.Hora_Final.ToString().Replace('.', ':'),
                             H_Inicio = citaCtx.Hora_Inicio.ToString().Replace('.', ':'),
                             NombreUsuario = usuarioCtx.Nombre + " " + usuarioCtx.Apellidos
                         });
            }

            await citax.ForEachAsync(cita =>
            {

                citas.Add(new Cita()
                {
                    Fecha = cita.Fecha,
                    UsuarioId = cita.UsuarioId,
                    CitaId = cita.CitaId,
                    Estatus = cita.Estatus,
                    Tipo = cita.Tipo,
                    Descripcion = cita.Descripcion,
                    Hora_Inicio = cita.Hora_Inicio,
                    Hora_Final = cita.Hora_Final,
                    H_Final = cita.H_Final,
                    H_Inicio = cita.H_Inicio,
                    NombreUsuario = cita.NombreUsuario
                });
            });


            SearchModel searchInfo = new SearchModel();

            if(name != null && name != "")
            {
                searchInfo.searchNombre = name;
            }else 
            {
                searchInfo.searchNombre = null;
            }

            if(date != null)
            {
                searchInfo.Fecha = DateTime.Parse(date);
                searchInfo.searchFecha = date;
            }


            ViewBag.Message = searchInfo;
            return View(citas);
        }

      
        public async Task<IActionResult> EditStateAtendido(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return await stateModify((int)id, "Atendido");
        }
        public async Task<IActionResult> EditStateCancelado(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return await stateModify((int)id, "Cancelado");
        }

        public async Task<IActionResult> stateModify(int id ,string estado)
        {
            var redirect = RedirectToAction();

            var cita = await _context.Cita.FindAsync(id);

            if (cita == null)
            {
                return NotFound();
            }
            Cita updateCita = cita;
            updateCita.Estatus = estado;

            _context.Entry(updateCita).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                Console.WriteLine("error");
            }
            redirect.ActionName = ""; // or can use nameof("") like  nameof(YourAction);
            redirect.ControllerName = "Admin"; // or can use nameof("") like  nameof(YourCtrl);
            return redirect;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = (from citaCtx in _context.Cita
                         join usuarioCtx in _context.Usuario on citaCtx.UsuarioId equals usuarioCtx.UsuarioId
                         where citaCtx.CitaId == id
                         select new Cita
                         {
                             Fecha = citaCtx.Fecha,
                             UsuarioId = citaCtx.UsuarioId,
                             CitaId = citaCtx.CitaId,
                             Estatus = citaCtx.Estatus,
                             Tipo = citaCtx.Tipo,
                             Descripcion = citaCtx.Descripcion,
                             Hora_Inicio = citaCtx.Hora_Inicio,
                             Hora_Final = citaCtx.Hora_Final,
                             H_Final = citaCtx.Hora_Final.ToString().Replace('.', ':'),
                             H_Inicio = citaCtx.Hora_Inicio.ToString().Replace('.', ':'),
                             NombreUsuario = usuarioCtx.Nombre + " " + usuarioCtx.Apellidos
                         });

            if (cita == null)
            {
                return NotFound();
            }
            Cita citaDetails = new Cita();
            await cita.ForEachAsync(cita =>
            {

                citaDetails.Fecha = cita.Fecha;
                citaDetails.UsuarioId = cita.UsuarioId;
                citaDetails.CitaId = cita.CitaId;
                citaDetails.Estatus = cita.Estatus;
                citaDetails.Tipo = cita.Tipo;
                citaDetails.Descripcion = cita.Descripcion;
                citaDetails.H_Final = cita.H_Final;
                citaDetails.H_Inicio = cita.H_Inicio;
                citaDetails.NombreUsuario = cita.NombreUsuario;
                
            });

            return View(citaDetails);
        }

        public IActionResult Search(SearchModel model)
        {
            string nameSearch = "";

            if (model.searchNombre != null)
            {
                nameSearch = model.searchNombre;
            }
            
            if(model.searchFecha != null)
            {
                string dateSearch = model.searchFecha.ToString();
                return RedirectToAction("Historial", new RouteValueDictionary(
                    new { controller = "Admin", action = "get", name = nameSearch, date = dateSearch }));
            }

            return RedirectToAction("Historial", new RouteValueDictionary(
                    new { controller = "Admin", action = "get", name=nameSearch }));
        }

    }
}
