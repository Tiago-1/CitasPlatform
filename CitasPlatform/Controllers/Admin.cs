﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CitasPlatform.Data;
using CitasPlatform.Models;

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
            List<Cita> cita = await _context.Cita
                               .Where(b => b.Fecha == date)
                               .OrderBy(o => o.Hora_Inicio)
                               .Select(b => new Cita
                               {
                                   Fecha = b.Fecha,
                                   CitaId = b.CitaId,
                                   UsuarioId = b.UsuarioId,
                                   Tipo = b.Tipo,
                                   Descripcion = b.Descripcion,
                                   Hora_Inicio = b.Hora_Inicio,
                                   Hora_Final = b.Hora_Final
                               }).ToListAsync();

            return View(cita);
        }

    }
}
